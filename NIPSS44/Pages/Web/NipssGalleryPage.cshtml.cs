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
    public class NipssGalleryPageModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public NipssGalleryPageModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Gallery> Gallery { get;set; }

        public async Task OnGetAsync()
        {
            Gallery = await _context.Galleries.Where(x=>x.DontShow == false).OrderByDescending(x=>x.Date).ToListAsync();
        }
    }
}
