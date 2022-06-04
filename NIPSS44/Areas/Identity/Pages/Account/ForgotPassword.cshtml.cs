using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;


namespace NIPSS44.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public ForgotPasswordModel(NIPSS44.Data.NIPSSDbContext context, IHostingEnvironment hostingEnv, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public Message i { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userx = await _userManager.FindByEmailAsync(Input.Email);
                if (userx == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["er"] = "Invalid Email Address";
                    return Page();
                }
                var user = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userx.Id);

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(userx);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);


                StreamReader sr = new StreamReader(System.IO.Path.Combine(_hostingEnv.WebRootPath, "emailsec.html"));
                MailMessage mail = new MailMessage();
                string mi = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{NAME}", user.Title + " " + user.FullName);
                mailmsg = mailmsg.Replace("{TITLE}", "Reset Password");
                mailmsg = mailmsg.Replace("{BODY}", mi);
                mail.Body = mailmsg;
                sr.Close();

                Message ms = new Message();
                ms.Recipient = user.User.Email;
                ms.Title = "Reset Password";
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

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

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
                TempData["err"] = ex.Message.ToString();
                return false;
            }
        }

    }
}
