using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace NutNut.Pages
{
	public class SignUpModel : PageModel
	{
		public static bool id_unique { get; set; } = true;
		public static bool success { get; set; } = false;
		[BindProperty] public string Username { get; set; } = string.Empty;
		[BindProperty] public string Password { get; set; } = string.Empty;
		[BindProperty] public string FirstName { get; set; } = string.Empty;
		[BindProperty] public string LastName { get; set; } = string.Empty;
		[BindProperty] public string Email { get; set; } = string.Empty;
		[BindProperty] public string PhoneNumber { get; set; } = string.Empty;

		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

				using SqlConnection connection = new(connectionString);

				connection.Open();

				var query = $@"select *
								from Users
								where 
                                    username = '{Username}' or
                                    email    = '{Email}'
                                ";

				using SqlCommand command = new(query, connection);

				using SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					id_unique = false;
					success = false;
				}
				else
				{
					success = true;

					reader.Close();

					query = "INSERT INTO Users (username, password, first_name, last_name, email, phone_no) VALUES (@username, @password, @first_name, @last_name, @email, @phone_no);";
					command.CommandText = query;

					command.Parameters.AddWithValue("@username", Username);
					command.Parameters.AddWithValue("@password", Password);
					command.Parameters.AddWithValue("@first_name", FirstName);
					command.Parameters.AddWithValue("@last_name", LastName);
					command.Parameters.AddWithValue("@email", Email);
					command.Parameters.AddWithValue("@phone_no", PhoneNumber);

					command.ExecuteNonQuery();

				}

				connection.Close();

				return new JsonResult(new { success });
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return new JsonResult(new { success });
			}
		}
	}
}
