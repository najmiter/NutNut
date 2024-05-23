using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static NutNut.Pages.MemoryDB;

namespace NutNut.Pages
{
	
	public class NutNut___Home_PageModel : PageModel
	{
		int MoviesPerPage { get; } = 20;

		public void OnGet()
		{
			if (shows.Count > MoviesPerPage)
			{
				shows = shows.GetRange(0, MoviesPerPage);
			}

			if (!movies.Count.Equals(0)) return;

			ReadMovies();
		}

		public IActionResult OnPost()
		{
			ReadMovies();
            return new JsonResult(new { movies });
		}

		public void ReadMovies()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-AAUJ0I7\\KNOCTAL;Initial Catalog=NutNut;Integrated Security=True;TrustServerCertificate=True;";

				using SqlConnection connection = new(connectionString);

				connection.Open();
				var query = $"select * from Movies order by Id OFFSET {movies.Count} ROWS FETCH FIRST {MoviesPerPage} ROWS ONLY";
				//query = "select * from Movies order by Id OFFSET 80 ROWS FETCH FIRST 20 ROWS ONLY";

				using SqlCommand command = new(query, connection);

				using SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
                    Movie movie = new()
					{
						Id = reader.GetInt64(0),
						Name = reader.GetString(1),
						Poster = reader.GetString(2),
						ReleaseYear = reader.GetInt32(3),
						Runtime = reader.GetInt32(4),
						Genre = reader.GetString(5),
						Ratings = reader.GetDecimal(6),
						Director = reader.GetString(7),
						TotalVotes = reader.GetInt32(12)
					};

                    //Console.WriteLine(MoviesRead);
					movie.Stars[0] = reader.GetString(8);
					movie.Stars[1] = reader.GetString(9);
					movie.Stars[2] = reader.GetString(10);
					movie.Stars[3] = reader.GetString(11);

					movies.Add(movie);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
