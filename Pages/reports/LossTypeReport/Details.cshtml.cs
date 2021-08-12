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
    public class DetailsModel : PageModel
    {
        private readonly ClaimsHandler.DataContext.InterviewContext _context;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _HttpContextAccessor;

        public DetailsModel(ClaimsHandler.DataContext.InterviewContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        public LossType LossType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("../../Account/AccessDenied");
            }

            LossType = await _context.LossTypes.FirstOrDefaultAsync(m => m.LossTypeId == id);

            if (LossType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
