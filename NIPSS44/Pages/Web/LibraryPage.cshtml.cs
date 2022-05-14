using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.Web
{
    public class LibraryPageModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public LibraryPageModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<DocumentCategory> DocumentCategory { get; set; }

        public async Task OnGetAsync()
        {
            DocumentCategory = await _context.DocumentCategories.Include(x => x.Documents).ToListAsync();


        }
    }
}
