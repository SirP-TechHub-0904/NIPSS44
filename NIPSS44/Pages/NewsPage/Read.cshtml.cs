using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.NewsPage
{
    public class ReadModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public ReadModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public News News { get; set; }
        public IList<News> NewsList { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            News = await _context.News.FirstOrDefaultAsync(m => m.Id == id);
            NewsList = await _context.News.OrderByDescending(x=>x.Date).Take(10).ToListAsync();

            if (News == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
