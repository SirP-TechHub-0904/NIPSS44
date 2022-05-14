using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Tour.Post
{
    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourPost> TourPost { get;set; }

        public async Task OnGetAsync()
        {
            TourPost = await _context.TourPosts
                .Include(t => t.TourPostType)
                .Include(t => t.TourSubCategory).ToListAsync();
        }
    }
}
