﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.QuestionnerPage.QPage
{
    public class DeleteModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DeleteModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions
                .Include(q => q.Questionner).FirstOrDefaultAsync(m => m.Id == id);

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FindAsync(id);

            if (Question != null)
            {
                _context.Questions.Remove(Question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
