namespace RP.App.Tasks.GetPhotos
{
    public class PhotoDto
    {
        public int AlbumId { get; }
        public int Id { get; }
        public string Title { get; }
        public string Url { get; }
        public string ThumbnailUrl { get; }

        public PhotoDto(int albumId, int id, string title, string url, string thumbnailUrl)
        {
            AlbumId = albumId;
            Id = id;
            Title = title;
            Url = url;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}