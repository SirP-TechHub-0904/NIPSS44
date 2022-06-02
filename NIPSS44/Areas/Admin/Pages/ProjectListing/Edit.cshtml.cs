﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.ProjectListing
{
    public class EditModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public EditModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LegacyProject LegacyProject { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync(m => m.Id == id);

            if (LegacyProject == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LegacyProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegacyProjectExists(LegacyProject.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LegacyProjectExists(long id)
        {
            return _context.LegacyProjects.Any(e => e.Id == id);
        }
    }
}
