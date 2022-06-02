using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.EventManagement
{
    public class IsLectureModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public IsLectureModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);
          
                if (Event == null)
            {
                return NotFound();
            }
            if (Event.IsLecture == true)
            {
                Event.IsLecture = false;
            }
            else
            {
                Event.IsLecture = true;
            }
            _context.Attach(Event).State = EntityState.Modified;

            
                await _context.SaveChangesAsync();
                return Page();
        }

      
    }
}
