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

        public IndexModel(ClaimsHandler.DataContext.InterviewContext context)
        {
            _context = context;
        }

        public IList<LossType> LossType { get;set; }

        public async Task OnGetAsync()
        {
            LossType = await _context.LossTypes.ToListAsync();
        }
    }
}
