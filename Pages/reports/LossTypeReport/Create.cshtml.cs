using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClaimsHandler.DataContext;
using ClaimsHandler.Models;

namespace ClaimsHandler.Pages.reports.LossTypeReport
{
    public class CreateModel : PageModel
    {
        private readonly ClaimsHandler.DataContext.InterviewContext _context;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _HttpContextAccessor;

        public CreateModel(ClaimsHandler.DataContext.InterviewContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        public IActionResult OnGet()
        {
            if (!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("../../Account/AccessDenied");
            }

            return Page();
        }

        [BindProperty]
        public LossType LossType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        // this code is a placeholder in case we want to permit creating LossTypes
        public async Task<IActionResult> OnPostAsync()
        {
/*            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LossTypes.Add(LossType);
            await _context.SaveChangesAsync();*/

            return RedirectToPage("./Index");
        }
    }
}
