namespace MoviesDatabase.Api.Models.Requests
{
    public class SearchMovieRequest
    {
        public string? MovieName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? DirectorName { get; set; } = string.Empty;
        public DateTime? RealiseDate { get; set; } = DateTime.MinValue;
        public bool Ordered { get; set; } = false;


    }
}
