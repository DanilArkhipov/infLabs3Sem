using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Prac4.Pages
{
    public class Chat : PageModel
    {


        public void OnPostEnter(string name)
        {
            var session = HttpContext.Session;
            session.SetString("name",name);
        }
        public void OnPostLogout()
        {
            var session = HttpContext.Session;
            session.Remove("name");
        }
    }
}