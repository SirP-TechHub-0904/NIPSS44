using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.Web
{
    public class TimeTableModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public TimeTableModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> Event { get;set; }
        public IQueryable<string> Events { get;set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public async Task OnGetAsync(string date = null)
        {

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
              .Select(x=>x.Date.Date.ToString())
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
        }
    }
}
