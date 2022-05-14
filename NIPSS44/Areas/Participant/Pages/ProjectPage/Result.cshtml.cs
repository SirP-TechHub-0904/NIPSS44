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

namespace NIPSS44.Areas.Participant.Pages.ProjectPage
{
    [Authorize]
        public class ResultModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ResultModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public LegacyProject LegacyProject { get; set; }

        public IList<LegacyProjectAnswer> LegacyProjectAnswers { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }
        public int P10 { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            LegacyProjectAnswers = await _context.LegacyProjectAnswers.ToListAsync();
            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync();

            P1 = LegacyProjectAnswers.Where(x => x.Answer == "P1").Count();
            P2 = LegacyProjectAnswers.Where(x => x.Answer == "P2").Count();
            P3 = LegacyProjectAnswers.Where(x => x.Answer == "P3").Count();
            P4 = LegacyProjectAnswers.Where(x => x.Answer == "P4").Count();
            P5 = LegacyProjectAnswers.Where(x => x.Answer == "P5").Count();
            P6 = LegacyProjectAnswers.Where(x => x.Answer == "P6").Count();
            P7 = LegacyProjectAnswers.Where(x => x.Answer == "P7").Count();
            P8 = LegacyProjectAnswers.Where(x => x.Answer == "P8").Count();
            P9 = LegacyProjectAnswers.Where(x => x.Answer == "P9").Count();
            P10 = LegacyProjectAnswers.Where(x => x.Answer == "P10").Count();



            return Page();
        }
       
    }

}
