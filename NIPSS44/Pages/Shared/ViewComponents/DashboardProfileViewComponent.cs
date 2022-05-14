﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using NIPSS44.Data.Model;
using NIPSS44.Data;

namespace Pestcontrol.Pages.Shared.ViewComponents
{
    public class DashboardProfileViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;


        public DashboardProfileViewComponent(
            UserManager<IdentityUser> userManager,
            NIPSSDbContext context
           
            /*IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _context = context;
        }

        public string UserInfo{ get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);


            if (profile != null)
                {
                    TempData["data"] = profile.FullName;
                    TempData["sponsor"] = profile.Sponsor;
                    TempData["img"] = profile.AboutProfile;
                }
            
          
            return View();
        }
    }
}
