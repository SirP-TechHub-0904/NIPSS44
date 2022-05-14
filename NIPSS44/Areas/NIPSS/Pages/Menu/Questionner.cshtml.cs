using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.NIPSS.Pages.Menu
{
    public class QuestionnerModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public QuestionnerModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Alumni> Alumni { get;set; }

        public async Task OnGetAsync()
        {
            Alumni = await _context.Alumnis.ToListAsync();
        }
    }
}
