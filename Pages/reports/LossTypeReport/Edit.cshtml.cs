using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClaimsHandler.DataContext;
using ClaimsHandler.Models;

namespace ClaimsHandler.Pages.reports.LossTypeReport
{
    public class EditModel : PageModel
    {
        private readonly ClaimsHandler.DataContext.InterviewContext _context;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _HttpContextAccessor;

        public EditModel(ClaimsHandler.DataContext.InterviewContext context, Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _HttpContextAccessor = HttpContextAccessor;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        // this code is a placeholder in case we want to permit editing LossTypes
        public async Task<IActionResult> OnPostAsync()
        {
        /*    if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LossType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LossTypeExists(LossType.LossTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/

            return RedirectToPage("./Index");
        }

        private bool LossTypeExists(int id)
        {
            return _context.LossTypes.Any(e => e.LossTypeId == id);
        }
    }
}
