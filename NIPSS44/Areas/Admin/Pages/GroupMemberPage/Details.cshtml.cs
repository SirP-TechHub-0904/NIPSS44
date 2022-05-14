﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.GroupMemberPage
{
    public class DetailsModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DetailsModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public StudyGroupMemeber StudyGroupMemeber { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudyGroupMemeber = await _context.StudyGroupMemebers
                .Include(s => s.Profile)
                .Include(s => s.StudyGroup).FirstOrDefaultAsync(m => m.Id == id);

            if (StudyGroupMemeber == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
