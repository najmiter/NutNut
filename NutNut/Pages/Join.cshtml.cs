using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using System.Data.SqlClient;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
	public class JoinModel : PageModel
	{
		[BindProperty] public string Username { get; set; } = user[0];
		[BindProperty] public string FullName { get; set; } = string.Empty;
		[BindProperty] public string CardNumber { get; set; } = string.Empty;
		[BindProperty] public string ExpiryDate { get; set; } = string.Empty;
		[BindProperty] public string CVC { get; set; } = string.Empty;


		public void OnPost()
		{
			try
			{
				bool already_exists = false;
				string this_date = DateTime.Today.ToString("MM-dd-yyyy");
				string next_date = DateTime.Now.AddMonths(1).ToString("MM-dd-yyyy");

				string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

				using SqlConnection connection = new(connectionString);

				connection.Open();

				var query = $"SELECT COUNT(*) FROM Members WHERE username = '{Username}'";

				using SqlCommand command = new(query, connection);

				using SqlDataReader reader = command.ExecuteReader();
				reader.Read();
				if (reader.GetInt32(0) != 0)
				{
					already_exists = true;
					try
					{
						reader.Close();
						query = $"UPDATE Members SET last_payment_date = cast(getdate() as date), next_payment_date = cast(dateadd(month, 1, getdate()) as date) WHERE username = '{Username}'";
						command.CommandText = query;

						command.ExecuteNonQuery();
					}
					catch (Exception e)
					{
                        Console.WriteLine("update member: {0}", e.Message);
                    }
				}
				else
				{
					reader.Close();
					try
					{
						// Insert into Members table
						query = "INSERT INTO Members (join_date, last_payment_date, next_payment_date, username) VALUES (@JoinDate, @LastPaymentDate, @NextPaymentDate, @Username)";
						command.CommandText = query;

						command.Parameters.Clear();
						command.Parameters.AddWithValue("@JoinDate", this_date);
						command.Parameters.AddWithValue("@LastPaymentDate", this_date);
						command.Parameters.AddWithValue("@NextPaymentDate", next_date);
						command.Parameters.AddWithValue("@Username", Username);
						
						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex.Message);
					}
				}

				// Retrieve membership_id
				query = "SELECT membership_id FROM Members WHERE username = @Username";
				command.CommandText = query;
				command.Parameters.Clear();

				command.Parameters.AddWithValue("@Username", Username);
				var result = command.ExecuteScalar();
				var member_id = result != null ? Convert.ToInt64(result) : -1;

				query = already_exists ? "UPDATE Payments SET cardholder_name = @CardholderName, card_number = @CardNumber, expiration_date = @ExpirationDate, cvc = @CVC WHERE membership_id = @MembershipId"
									   : "INSERT INTO Payments (membership_id, cardholder_name, card_number, expiration_date, cvc) VALUES (@MembershipId, @CardholderName, @CardNumber, @ExpirationDate, @CVC)";


				command.CommandText = query;

				command.Parameters.Clear();
				command.Parameters.AddWithValue("@CardholderName", FullName);
				command.Parameters.AddWithValue("@CardNumber", CardNumber);
				command.Parameters.AddWithValue("@ExpirationDate", ExpiryDate);
				command.Parameters.AddWithValue("@CVC", CVC);
				command.Parameters.AddWithValue("@MembershipId", member_id);
				command.ExecuteNonQuery();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
