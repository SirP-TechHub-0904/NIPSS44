using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIPSS44.Data.Model;
using NIPSS44.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIPSS44.Pages
{
    public class IndexModel : PageModel 
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly UserManager<IdentityUser> _userManager;


        public IndexModel(ILogger<IndexModel> logger, Data.NIPSSDbContext context, INotificationService notificationService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public IList<Executive> Executive { get; set; }
        public IList<Profile> NipssStaffList { get; set; }
        public IList<News> News { get; set; }
        public IList<CurrentAffair> CurrentAffair { get; set; }

        public async Task OnGetAsync()
        {
            Executive = await _context.Executive.Include(x=>x.Profile).Include(x => x.Alumni).OrderBy(x => x.SortOrder).Where(x=>x.Alumni.Active == true).Take(3).ToListAsync();
            NipssStaffList = await _context.Profiles.Include(x => x.User).Where(x => x.AccountRole == "ManagingStaff").OrderBy(x => x.SortOrder).Take(3).ToListAsync();
            News = await _context.News.Include(x=>x.Comments).OrderBy(x => x.Date).Take(3).ToListAsync();
            CurrentAffair = await _context.CurrentAffairs.OrderBy(x => x.Date).Take(8).ToListAsync();

            var LegacyProjectAnswers = await _context.LegacyProjectAnswers.ToListAsync();
            decimal p1 = LegacyProjectAnswers.Where(x => x.Answer == "P1").Count();
            decimal p2 = LegacyProjectAnswers.Where(x => x.Answer == "P2").Count();
            decimal p3 = LegacyProjectAnswers.Where(x => x.Answer == "P3").Count();
            decimal p4 = LegacyProjectAnswers.Where(x => x.Answer == "P4").Count();
            decimal p5 = LegacyProjectAnswers.Where(x => x.Answer == "P5").Count();
            decimal p6 = LegacyProjectAnswers.Where(x => x.Answer == "P6").Count();
            decimal p7 = LegacyProjectAnswers.Where(x => x.Answer == "P7").Count();
            decimal p8 = LegacyProjectAnswers.Where(x => x.Answer == "P8").Count();
            decimal p9 = LegacyProjectAnswers.Where(x => x.Answer == "P9").Count();
            decimal p10 = LegacyProjectAnswers.Where(x => x.Answer == "P10").Count();


            //decimal ikf = (lpp / 90);
            //decimal jfh = (ikf *100);
            //int os = (p1 / 90);
            //int osj = (p1 / Convert.ToInt32(90));
            //int osjj = (p1 / Convert.ToInt32(90) * Convert.ToInt32(100));
            //int kos = (p1 / 90)*100;
            int ols = Convert.ToInt32((p1 / 90) * 100);
            P1 = Convert.ToInt32((p1 / 90) * 100);
            P2 = Convert.ToInt32((p2 / 90) * 100);
            P3 = Convert.ToInt32((p3 / 90) * 100);
            P4 = Convert.ToInt32((p4 / 90) * 100);
            P5 = Convert.ToInt32((p5 / 90) * 100);
            P6 = Convert.ToInt32((p6 / 90) * 100);
            P7 = Convert.ToInt32((p7 / 90) * 100);
            P8 = Convert.ToInt32((p8 / 90) * 100);
            P9 = Convert.ToInt32((p9 / 90) * 100);
            P10 = Convert.ToInt32((p10 / 90) * 100);


            
        }

        [BindProperty]
        public Contact Contact { get; set; }


        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }
        public int P10 { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.


        public async Task<JsonResult> OnGetRunAccount(string devicex, string tokenid)
        {
            try
            {
                long id = 0;
                var user = await _userManager.GetUserAsync(User);
                if(user != null)
                {
                    var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    id = profile.Id;
                }
                var getnotifystatus = await _context.UserToNotifys.Include(x=>x.Profile).FirstOrDefaultAsync(x => x.TokenId == tokenid);
                if (getnotifystatus != null)
                {
                    if (getnotifystatus.ProfileId == null)
                    {
                        getnotifystatus.ProfileId = id;
                        _context.Attach(getnotifystatus).State = EntityState.Modified;

                        await _context.SaveChangesAsync();


                        Notification nf = new Notification();
                        nf.DatetTime = DateTime.UtcNow.AddHours(1).AddMinutes(4);
                        nf.Message = "WELCOME "+getnotifystatus.Profile.FullName+". KINDLY ENABLE ALL YOUR NOTIFICATIONS TO ENABLE US NOTIFY YOU ON IMPORTANT MATTERS.";
                        nf.Title = "WELCOME TO SEC 44, 2022";
                        nf.UserToNotifyId = getnotifystatus.Id;
                        _context.Notifications.Add(nf);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    UserToNotify n = new UserToNotify();
                    n.IsAndriod = true;
                    n.TokenId = tokenid;
                    if (id == 0)
                    {
                        n.ProfileId = null;
                    }
                    else
                    {
                        n.ProfileId = id;
                    }
                    _context.UserToNotifys.Add(n);
                    await _context.SaveChangesAsync();

                    Notification nf = new Notification();
                    nf.DatetTime = DateTime.UtcNow.AddHours(1).AddMinutes(4);
                    nf.Message = "WELCOME TO SEC 44. KINDLY ENABLE ALL YOUR NOTIFICATIONS TO ENABLE US NOTIFY YOU ON IMPORTANT MATTERS.";
                    nf.Title = "WELCOME TO SEC 44, 2022";
                    nf.UserToNotifyId = n.Id;
                    _context.Notifications.Add(nf);
                    await _context.SaveChangesAsync();

                }

                return null;
            }
            catch (Exception k)
            {
                return new JsonResult("not found");
            }
        }

    }
}
