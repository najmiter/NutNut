namespace NutNut.Pages
{
    public class Movie
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public int ReleaseYear {  get; set; }
        public int Runtime {  get; set; }
        public string Genre { get; set; } = string.Empty;
        public decimal Ratings { get; set; }
        public string Director { get; set; } = string.Empty;
        public string[] Stars { get; set; } = new string[4];
        public int TotalVotes { get; set; }
    }
}
