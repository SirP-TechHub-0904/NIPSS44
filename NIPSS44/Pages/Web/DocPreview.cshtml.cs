﻿using System;
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
    public class DocPreviewModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DocPreviewModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Document Document { get; set; }
        public IList<DocumentCategory> DocumentCategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Document = await _context.Documents
                .Include(d => d.DocumentCategory)
                .Include(d => d.Event)
                .Include(d => d.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Document == null)
            {
                return NotFound();
            }
            DocumentCategoryList = await _context.DocumentCategories.Include(x => x.Documents).ToListAsync();

            return Page();
        }
    }
}
