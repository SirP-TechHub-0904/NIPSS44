using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Participant.Pages.SEC
{
    public partial class IndexModel : PageModel
    {

        private readonly Data.NIPSSDbContext _context;

        public IndexModel(
            Data.NIPSSDbContext context)
        {
            _context = context;
        }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string GroupPosition { get; set; }

        [BindProperty]
        public long GroupId { get; set; }

        [BindProperty]
        public long GroupMemberId { get; set; }
        [BindProperty]
        public string GroupName { get; set; }
        public StudyGroupMemeber StudyGroup { get; set; }


        public async Task<IActionResult> OnGetAsync(string id = null)
        {
            if (id == null)
            {
                return RedirectToPage("./xFound");
            }
            Profile = await _context.Profiles.Include(x=>x.User).Include(x=>x.StudyGroupMemeber).ThenInclude(x=>x.StudyGroup).FirstOrDefaultAsync(x => x.ProfileHandler == id);
            
           
            return Page();
        }
      

    }

}
