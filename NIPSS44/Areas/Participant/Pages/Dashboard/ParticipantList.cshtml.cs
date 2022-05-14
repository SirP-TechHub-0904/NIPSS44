using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
    public class ParticipantListModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public ParticipantListModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }
        public string Gender { get; set; }
        public IList<Profile> Profile { get;set; }

        public async Task OnGetAsync(string gender = null)
        {
            Profile = await _context.Profiles
                .Include(p => p.Alumni)
                .Include(p => p.User).Where(x => x.AccountRole == "Participant").ToListAsync();
            if(gender != null)
            {
                Gender = gender;
                Profile = Profile.Where(x => x.Gender != null && x.Gender.ToLower() == gender).ToList();
            }
        }
    }
}
