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

        public DetailsModel(ClaimsHandler.DataContext.InterviewContext context)
        {
            _context = context;
        }

        public LossType LossType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
