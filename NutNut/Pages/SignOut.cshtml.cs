using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
    public class SignOutModel : PageModel
    {
        public void OnGet()
        {
            movies.Clear();
            shows.Clear();
            user[0] = string.Empty;
            user[1] = string.Empty;
            MembershipChecked = false;
            ImgId = new Random().Next(1, 70);
            
            SignedIn.IsSignedIn = false;
            SignedIn.WrongPassword = false;
        }
    }
}
