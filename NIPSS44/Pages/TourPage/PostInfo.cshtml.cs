using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.TourPage
{
       public class PostInfoModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public PostInfoModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourPost> TourPost { get; set; }
        public IList<TourSubCategory> TourSubCategories { get; set; }
        public string Title { get; set; }

        public async Task OnGetAsync(long id)
        {
            TourPost = await _context.TourPosts
                .Include(t => t.TourPostType)
                .Include(t => t.TourSubCategory).Where(x => x.TourSubCategoryId == id).ToListAsync();
            var title = await _context.TourSubCategories.FirstOrDefaultAsync(x => x.Id == id);
            Title = title.Title;

            TourSubCategories = await _context.TourSubCategories.ToListAsync();
        }
    }

}
