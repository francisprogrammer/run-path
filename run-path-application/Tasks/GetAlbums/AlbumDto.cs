namespace RP.App.Tasks.GetAlbums
{
    public class AlbumDto
    {
        public int UserId { get; }
        public int Id { get; }
        public string Title { get; }

        public AlbumDto(int userId, int id, string title)
        {
            UserId = userId;
            Id = id;
            Title = title;
        }
    }
}