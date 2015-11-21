using System.Web.Http;

public class DeviceData : IRepositoryItem
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceState DeviceState { get; set; }
}

public class CreateDeviceController : ApiController
{
    /// <summary>
    /// Add a new DeviceData into the repository.
    /// </summary>
    public DeviceData Post(DeviceData item)
    {
        return Program.Devices.Store(item);
    }
}
