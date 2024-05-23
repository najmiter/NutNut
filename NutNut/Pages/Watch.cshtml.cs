using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
	public class WatchModel : PageModel
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
