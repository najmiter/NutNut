using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
    static public class SignedIn
    {
        public static bool IsSignedIn { get; set; } = false;
        public static bool WrongPassword { get; set; } = false;
    }

    public class IndexModel : PageModel
	{
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string Username { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
            //SignedIn.IsSignedIn = false;
        }

        public void OnPost()
		{
            try
            {
                string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

                using SqlConnection connection = new(connectionString);

                connection.Open();

                var query = $@"select *
								from Users
								where username = '{Username}'";

                using SqlCommand command = new(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (Username.Equals(reader.GetString(0)) && Password.Equals(reader.GetString(1)))
                    {
                        SignedIn.IsSignedIn = true;
                        SignedIn.WrongPassword = false;
                        user[0] = Username;
                        user[1] = Password;
                    }
                    else
                    {
                        SignedIn.WrongPassword = true;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
