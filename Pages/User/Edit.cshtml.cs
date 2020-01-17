using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API
{
    public class EditModel : PageModel
    {
        private readonly WEB_API.Data.WEB_APIContext _context;

        public EditModel(WEB_API.Data.WEB_APIContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                string userJson = JsonSerializer.Serialize(User);
                var request = WebRequest.CreateHttp("https://cds-firebase.firebaseio.com/" + User.id + ".json");
                request.Method = "PUT";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(userJson);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                request.GetResponse();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        

            return RedirectToPage("./Index");
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}
