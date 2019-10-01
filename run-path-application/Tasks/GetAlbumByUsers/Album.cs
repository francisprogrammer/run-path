using System.Collections.Generic;

namespace RP.App.Tasks.GetAlbumByUsers
{
    public class Album
    {
        public int Id { get; }
        public int UserId { get; }
        public string Title { get; }
        
        public IEnumerable<Photo> Photos { get; }

        public Album(int id, int userId, string title, IEnumerable<Photo> photos)
        {
            Id = id;
            Title = title;
            Photos = photos;
            UserId = userId;
        }
    }
}