using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Participant.Pages.Forms
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Questionner> QuestionnerList { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            ProfileId = profile.Id;
            QuestionnerList = await _context.Questionners.Where(x=>x.ProfileId == profile.Id).ToListAsync();

        }
        [BindProperty]
        public Questionner Questionner { get; set; }

        [BindProperty]
        public long ProfileId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Questionner.ShortLink = CreateD();
            string code = Guid.NewGuid().ToString();
            string code1 = Guid.NewGuid().ToString();
            string code2 = Guid.NewGuid().ToString();
            Questionner.LongLink = code + CreateD() + code1 + code2;
            Questionner.ProfileId = ProfileId;


            _context.Questionners.Add(Questionner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./FormMaker", new { o = Questionner.ShortLink, q = Questionner.LongLink });
        }


        private static string CreateA(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateB(int length = 3)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private static string CreateC(int length = 2)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private static string CreateD(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = CreateA() + CreateB() + CreateC();
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

    }
}
