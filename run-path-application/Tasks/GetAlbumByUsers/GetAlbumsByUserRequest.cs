namespace RP.App.Tasks.GetAlbumByUsers
{
    public class GetAlbumsByUserRequest
    {
        public int UserId { get; }

        public GetAlbumsByUserRequest(int userId)
        {
            UserId = userId;
        }
    }
}