using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API
{
    public class IndexModel : PageModel
    {
        private readonly WEB_API.Data.WEB_APIContext _context;

        public IndexModel(WEB_API.Data.WEB_APIContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
