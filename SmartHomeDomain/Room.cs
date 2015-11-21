using System;

public abstract class Room : IRepositoryItem
{
    private long id;

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public static Room createInstance(RoomType type)
    {
        switch (type)
        {
            case RoomType.Kitchen:
                return new Kitchen();

            case RoomType.Sauna:
                return new Sauna();

            case RoomType.LivingRoom:
                return new LivingRoom();

            default:
                throw new ApplicationException("Unsupported room type: " + type.ToString());
        }
    }
}
