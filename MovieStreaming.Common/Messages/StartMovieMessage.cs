namespace MovieStreaming.Common.Messages
{
    public class StartMovieMessage
    {
        public string MovieTitle { get; private set; }
        public int UserId { get; private set; }
        public StartMovieMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }

    }
}
