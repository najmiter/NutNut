using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace NutNut.Pages
{
	public static class MemoryDB
	{
		public static List<Movie> movies = [];
		public static List<TVShow> shows = [];
		public static string[] user = new string[2];

		public static bool MembershipChecked { get; set; } = false;

		public static int ImgId { get; set; } = new Random().Next(1, 70);

		public static bool CheckMembership()
		{
			MembershipChecked = true;
			try
			{
				string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

				using SqlConnection connection = new(connectionString);

				connection.Open();

				var query = $@"SELECT *
								FROM Members
								WHERE username = '{user[0]}';
                                ";

				using SqlCommand command = new(query, connection);

				using SqlDataReader reader = command.ExecuteReader();
				if (reader.Read())
				{
                    DateTime npd = DateTime.Parse(((DateTime)reader["next_payment_date"]).ToString("MM-dd-yyyy"));
                    return reader["membership_id"] != DBNull.Value && 
						   npd >= DateTime.Today;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("membership: {0}", e.Message);
			}

			return false;
		}
	}
}
