using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
    public class DocumentListModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DocumentListModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Document> Document { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }
        public DocumentCategory DocumentCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            Document = await _context.Documents.Include(x => x.DocumentCategory).Include(x => x.Event).Include(x => x.Profile).Where(x => x.DocumentCategoryId == id).Where(x => x.DontShow == false).OrderByDescending(x => x.Event.Date).ToListAsync();

            DocumentCategory = await _context.DocumentCategories.FirstOrDefaultAsync(x => x.Id == id);

            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).ToListAsync();

        }
    }
}
