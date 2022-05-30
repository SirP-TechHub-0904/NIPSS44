
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NIPSS44.Data;
using NIPSS44.Data.Model;
using NIPSS44.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NIPSS44.Background
{

    public class RequestInfo
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Retries { get; set; }
    }

    public class BackgroundSender : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly INotificationService _notificationService;
        public BackgroundSender(ILogger<BackgroundSender> logger, IServiceScopeFactory scopeFactory, INotificationService notificationService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _notificationService = notificationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<NIPSSDbContext>();
                IQueryable<Notification> notif = from s in _context.Notifications
                                                 .Include(x => x.UserToNotify)
                                                   .Where(x => x.Sent == false).AsNoTracking()
                                                   //.Where(x => x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(-5) && x.DatetTime <= DateTime.UtcNow.AddHours(1).AddMinutes(+5))
                                                   .Take(7)
                                                 select s;

                foreach (var nm in notif)
                {
                    NotificationModel notificationModel = new NotificationModel();
                    notificationModel.Body = nm.Message;
                    notificationModel.Title = nm.Title;
                    notificationModel.IsAndroiodDevice = true;
                    notificationModel.DeviceId = nm.UserToNotify.TokenId;
                    await _notificationService.SendNotification(notificationModel);
                    var xiod = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == nm.Id);
                    string ixd = xiod.Id.ToString();

                    try
                    {

                        var entry = await _context.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(ixd));

                        entry.Sent = true;
                        _context.Attach(entry).State = EntityState.Modified;
                        //_context.Entry(entry).CurrentValues.SetValues(xiod);
                        

                    }
                    catch (Exception e)
                    {
                        var x = "";
                    }

                }
                _context.SaveChanges();
                //Do your stuff with your Dbcontext
                IQueryable<Message> msgk = from s in _context.Messages
                                                    .Where(x => x.NotificationStatus == NotificationStatus.NotSent || x.NotificationStatus == NotificationStatus.NotDefind)
                                                    .Take(7)
                                           select s;

                var msg = msgk.Where(x => x.Retries < 5);

                var c = msg.Count();
                var cf = msg.ToList();

                foreach (var i in msg)
                {
                    if (i.NotificationType != NotificationType.SMS)
                    {
                        //
                        bool result = await SendEmail(i.Recipient, i.Mail, i.Title);
                        if (result == true)
                        {
                            i.NotificationStatus = NotificationStatus.Sent;
                        }
                        else
                        {
                            i.NotificationStatus = NotificationStatus.NotSent;
                            i.Retries = i.Retries + 1;
                        }
                    }

                    try
                    {

                        var iod = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.Id);
                        iod.NotificationStatus = i.NotificationStatus;
                        iod.Retries = i.Retries;
                        //_context.Entry(iod).State = EntityState.Detached;
                        _context.Attach(iod).State = EntityState.Modified;


                    }

                    catch (Exception webex)
                    {

                    }


                }
                await _context.SaveChangesAsync();
            }
        }
        //private async void DoWork(object state)
        //{
        //    _logger.LogInformation("Timed Background Service is working.");
        //    await _senderService.Notification();
        //}

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public async Task<bool> SendEmail(string recipient, string message, string title)
        {
            try
            {


                //create the mail message 
                MailMessage mail = new MailMessage();


                mail.Body = message;
                //set the addresses 
                mail.From = new MailAddress("noreply@sec44nipss.com", "SEC44 NIPSS"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(recipient);

                //set the content 
                mail.Subject = title.Replace("\r\n", "");

                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("mail.sec44nipss.com");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("noreply@sec44nipss.com", "Admin@123");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
