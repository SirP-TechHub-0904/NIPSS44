using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Rapid
{
    public class RapidListModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public RapidListModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<UserAnswer> UserAnswer { get;set; }

        public async Task OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                UserAnswer = await _context.UserAnswers.Include(x => x.Profile).ToListAsync();

            }
            else
            {
                UserAnswer = await _context.UserAnswers.Include(x => x.Profile).Where(x => x.Title == id).OrderByDescending(x=>x.Date).ToListAsync();

            }
        }
    }
}
