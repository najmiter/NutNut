using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
	public class TVShowsModel : PageModel
	{
		long ShowsPerPage { get; set; } = 20L;

		public void OnGet()
		{
			if (movies.Count > 20)
			{
				movies = movies.GetRange(0, 20);
			}

			if (!shows.Count.Equals(0)) return;

			ReadShows();
		}

		public IActionResult OnPost()
		{
			ReadShows();
			return new JsonResult(new { shows });
		}

		public void ReadShows()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

				using SqlConnection connection = new(connectionString);

				connection.Open();
				var query = $"select * from TVShows order by Id OFFSET {shows.Count} ROWS FETCH FIRST {ShowsPerPage} ROWS ONLY";
				//query = "select * from Movies order by Id OFFSET 80 ROWS FETCH FIRST 20 ROWS ONLY";

				using SqlCommand command = new(query, connection);

				using SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					TVShow show = new()
					{
						Id = reader.GetInt64(0),
						Name = reader.GetString(1),
						Director = reader.GetString(2),
						Stars = reader.GetString(3),
						ReleaseYear = reader.GetString(4),
						Rating = reader.GetString(5),
						Duration = reader.GetString(6),
						ListedIn = reader.GetString(7),
						Description = reader.GetString(8)
					};

					shows.Add(show);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
