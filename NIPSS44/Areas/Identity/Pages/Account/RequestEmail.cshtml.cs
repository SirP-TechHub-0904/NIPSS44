using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.IO;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RequestEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly NIPSS44.Data.NIPSSDbContext _context;


        public RequestEmailModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv, Data.NIPSSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _hostingEnv = hostingEnv;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public string Fullname { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

        }

        public async Task OnGetAsync()
        {
           
        }
        public Message i { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //  returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    var callbackUrl = Url.Page(
                       "/Account/Firstsignin",
                       pageHandler: null,
                       values: new { area = "Identity", userId = user.Email, code = user.SecurityStamp },
                       protocol: Request.Scheme);
                    StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                    MailMessage mail = new MailMessage();
                    string mi = $"Kindly Login via the link <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>click here</a>.<br>" +
                        $"<h5>OR Visit <a href='www.sec44nipss.com'>www.sec44nipss.com</a> and click on VLE on the menu.</h6><br>" +
                        $"<p>Email: " + profile.User.Email + "</p>" +
                        $"<p>Password: " + profile.PXI + "</p>";

                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{NAME}", profile.Title + " " + profile.FullName);
                    mailmsg = mailmsg.Replace("{TITLE}", "Account Setup");
                    mailmsg = mailmsg.Replace("{BODY}", mi);
                    mail.Body = mailmsg;
                    sr.Close();

                    Message ms = new Message();
                    ms.Recipient = profile.User.Email;
                    ms.Title = "Account Setup";
                    ms.Mail = mailmsg;
                    ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                    _context.Messages.Add(ms);
                    await _context.SaveChangesAsync();




                    i = await _context.Messages.FirstOrDefaultAsync(m => m.Id == ms.Id);

                    if (i == null)
                    {
                        return NotFound();
                    }
                    if (i.NotificationType != NotificationType.SMS)
                    {
                        //
                        bool result = await SendEmail(i.Recipient, i.Mail, i.Title);
                        if (result == true)
                        {
                            i.NotificationStatus = NotificationStatus.Sent;
                            TempData["sendt"] = "Sent";
                        }
                        else
                        {
                            i.NotificationStatus = NotificationStatus.NotSent;
                            i.Retries = i.Retries + 1;
                            TempData["sendt"] = "failed";
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



                    await _context.SaveChangesAsync();

                }
                else
                {
                    TempData["error"] = "Error";
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
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

        public async Task<IActionResult> OnPostSendForm()
        {
            //  returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
             
                    StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                    MailMessage mail = new MailMessage();
                    string mi =
                        $"<p>Email: " + Input.Email + "</p>" +
                        $"<p>phone: " + Phone + "</p>"+
                        $"<p>Name: " + Fullname + "</p>";
                        

                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{NAME}", Fullname);
                    mailmsg = mailmsg.Replace("{TITLE}", "Check Account");
                    mailmsg = mailmsg.Replace("{BODY}", mi);
                    mail.Body = mailmsg;
                    sr.Close();

                    Message ms = new Message();
                    ms.Recipient = "onwukaemeka41@gmail.com";
                    ms.Title = "Check Account";
                    ms.Mail = mailmsg;
                    ms.Retries = 0; ms.NotificationStatus = NotificationStatus.NotSent; ms.NotificationType = NotificationType.Email;
                    _context.Messages.Add(ms);
                    await _context.SaveChangesAsync();




                    i = await _context.Messages.FirstOrDefaultAsync(m => m.Id == ms.Id);

                    if (i == null)
                    {
                        return NotFound();
                    }
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



                    await _context.SaveChangesAsync();
                    TempData["request"] = "dfgf";
                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
