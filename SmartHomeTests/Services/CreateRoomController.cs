using System.Web.Http;

public class RoomData
{
    public long Id { get; set; }
    public RoomType RoomType { get; set; }
}

public class CreateRoomController : ApiController
{
    /// <summary>
    /// Create and add a new Room into the repository.
    /// </summary>
    public RoomData Post(RoomData data)
    {
        // Note: repository creates a Room, not RoomData.
        var room = Program.Rooms.Store(Room.createInstance(data.RoomType));

        // set room ID back to the data object
        data.Id = room.Id;

        return data;
    }
}
