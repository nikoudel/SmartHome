using System.Linq;
using System.Web.Http;

public class ChangeDeviceStateData
{
    public long Id { get; set; }
    public DeviceState NewState { get; set; }
}

public class ChangeDeviceStateController : ApiController
{
    public ChangeDeviceStateData Post(ChangeDeviceStateData newDeviceState)
    {
        // initialize the response object
        var resultState = new ChangeDeviceStateData();

        // find the needed device
        var device = Program.Devices.Items.FirstOrDefault(d => d.Id == newDeviceState.Id);

        if (device == null)
        {
            // device not found
            resultState.Id = 0;
            resultState.NewState = DeviceState.Undefined;
        }
        else
        {
            // change device state
            device.DeviceState = newDeviceState.NewState;

            // set new values to the response object
            resultState.Id = device.Id;
            resultState.NewState = device.DeviceState;
        }

        return resultState;
    }
}