using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
	public class WatchShowModel : PageModel
	{
		public bool IsMember { get; set; } = false;
		public bool WasCheckedBefore { get; set; } = false;

		public void OnGet()
		{
			if (!WasCheckedBefore)
			{
				IsMember = CheckMembership();
				WasCheckedBefore = true;
			}
		}
	}
}
