using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API
{
    public class DeleteModel : PageModel
    {
        private readonly WEB_API.Data.WEB_APIContext _context;

        public DeleteModel(WEB_API.Data.WEB_APIContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FindAsync(id);

            if (User != null)
            {
                _context.User.Remove(User);
                await _context.SaveChangesAsync();
            }

            var request = WebRequest.CreateHttp("https://cds-firebase.firebaseio.com/" + id + ".json");
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.GetResponse();
            Console.WriteLine("Usuário Apagado com Sucesso.");

            return RedirectToPage("./Index");
        }
    }
}
