using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClaimsHandler.DataContext;
using ClaimsHandler.Models;

namespace ClaimsHandler.Pages.reports.LossTypeReport
{
    public class IndexModel : PageModel
    {
        private readonly ClaimsHandler.DataContext.InterviewContext _context;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _HttpContextAccessor;

        public IndexModel(ClaimsHandler.DataContext.InterviewContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        public IList<LossType> LossType { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if(!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("../../Account/AccessDenied");
            }
            LossType = await _context.LossTypes.ToListAsync();
            return Page();
        }
    }
}
