using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Doc.DocList
{
    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Document> Document { get;set; }

        public async Task OnGetAsync()
        {
            Document = await _context.Documents
                .Include(d => d.DocumentCategory)
                .Include(d => d.Profile).ToListAsync();
        }
    }
}
