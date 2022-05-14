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

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
    [Authorize(Roles = "Participant")]

    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public IList<Event> Events { get; set; }
        public IList<Event> EventOnly { get; set; }
        public IQueryable<Event> Event { get; set; }
        public IQueryable<string> Events { get; set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public int TotalParticipant { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int ProjectAssigned { get; set; }
        public int ProjectAssigned_Submitted { get; set; }
        public int LecturesDocuments { get; set; }
        public int RESOURCEMATERIALS { get; set; }
        public int GroupResearch { get; set; }
        public int INDIVIDUALESSAYS_Submitted { get; set; }
        public int INDIVIDUALESSAYS { get; set; }
        public int OTHERRESOURCEMATERIALS { get; set; }
        public async Task<IActionResult> OnGetAsync(string date = null)
        {

            var participant = await _context.Profiles.Where(x => x.AccountRole == "Participant").ToListAsync();
            TotalParticipant = participant.Count();
            Male = participant.Where(x => x.Gender != null && x.Gender.ToLower() == "male").Count();
            Female = participant.Where(x => x.Gender != null && x.Gender.ToLower() == "female").Count();
            var doc = await _context.Documents.Include(x => x.DocumentCategory).ToListAsync();
            ProjectAssigned = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL RESEARCH PROJECTS")).Count();
            ProjectAssigned_Submitted = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL RESEARCH PROJECTS")).Where(x => x.FileName != null).Count();
            LecturesDocuments = doc.Where(x => x.DocumentCategory.Title.Contains("LECTURE NOTES")).Count();
            RESOURCEMATERIALS = doc.Where(x => x.DocumentCategory.Title.Contains("RESOURCE MATERIALS ON LOCAL GOVERNMENT")).Count();
            GroupResearch = doc.Where(x => x.DocumentCategory.Title.Contains("GROUP RESEARCH POLICY PAPERS")).Count();
            INDIVIDUALESSAYS = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL ESSAYS")).Count();
            INDIVIDUALESSAYS_Submitted = doc.Where(x => x.DocumentCategory.Title.Contains("INDIVIDUAL ESSAYS")).Where(x => x.FileName != null).Count();
            OTHERRESOURCEMATERIALS = doc.Where(x => x.DocumentCategory.Title.Contains("OTHER RESOURCE MATERIALS")).Count();


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (profile.PsChange == false)
            {
                return RedirectToPage("/Account/Manage/ChangePassword", new { area = "Identity" });
            }
            if (profile.ProfileUpdateLevel != Data.Model.ProfileUpdateLevel.THREE)
            {
                if (profile.ProfileUpdateFirstTime == false)
                {
                    TempData["alert"] = "Kindly Update Your Profile to access your dashboard";
                    return RedirectToPage("./Profile");
                }

                else if (String.IsNullOrEmpty(profile.Gender))
                {
                    TempData["alert"] = "Kindly Update Your Gender to access your dashboard";
                    return RedirectToPage("./Profile");
                }
            }
            IQueryable<Event> evct = from s in _context.Events.OrderByDescending(x => x.Date)
                                         //.Where(x=>x.Date.DayOfWeek == DateTime.UtcNow.DayOfWeek)
                                     select s;
            DateTime givenDate = DateTime.Today;
            if (date != null)
            {

                givenDate = DateTime.Parse(date).AddDays(1);
            }

            DateTime startOfWeek = givenDate.AddDays(-1 * Convert.ToInt32(givenDate.DayOfWeek)).AddDays(1);
            DateTime endOfWeek = startOfWeek.AddDays(5);

            var query = evct
              .Where(ob => startOfWeek <= ob.Date && ob.Date < endOfWeek)
              .Select(x => x.Date.Date.ToString())
              .Distinct().AsQueryable();

            Events = query;

            var xquery = query.FirstOrDefault();
            var eventinfo = await _context.Events.FirstOrDefaultAsync(x => x.Date.Date.ToString() == xquery);
            if (eventinfo != null)
            {
                if (eventinfo.Note != null)
                {
                    Desc = eventinfo.Note ?? "";
                }
            }
            DateTime mondayOfLastWeek = givenDate.AddDays(-(int)givenDate.DayOfWeek - 6);
            DateTime mondayOfNextWeek = givenDate.AddDays(-(int)givenDate.DayOfWeek + 8);
            PreviousWeek = mondayOfLastWeek.Date.ToString("dd MMMM yyyy");
            NextWeek = mondayOfNextWeek.Date.ToString("dd MMMM yyyy");
            PreviousWeekTitle = "Previous " + mondayOfLastWeek.Date.ToString("dd MMMM") + " to " + mondayOfLastWeek.Date.AddDays(4).ToString("dd MMMM");
            NextWeekTitle = "Next " + mondayOfNextWeek.Date.ToString("dd MMMM") + " to " + mondayOfNextWeek.Date.AddDays(4).ToString("dd MMMM");
            Title = startOfWeek.ToString("dd") + " - " + endOfWeek.ToString("dd MMMM yyyy");
            //if(profile )



            return Page();
        }
    }
}
