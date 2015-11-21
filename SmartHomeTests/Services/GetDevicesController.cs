using System.Linq;
using System.Collections.Generic;
using System.Web.Http;

public class GetDevicesController : ApiController
{
    /// <summary>
    /// Get a list of devices based on a given criteria.
    /// </summary>
    public IEnumerable<DeviceData> Get([FromUri]DeviceData crit)
    {
        var query = from device in Program.Devices.Items select device;

        if(crit.Id > 0)
        {
            query = query.Where(device => device.Id == crit.Id);
        }

        if(crit.RoomId > 0)
        {
            query = query.Where(device => device.RoomId == crit.RoomId);
        }
        
        if(crit.DeviceType != DeviceType.Undefined)
        {
            query = query.Where(device => device.DeviceType == crit.DeviceType);
        }

        if (crit.DeviceState != DeviceState.Undefined)
        {
            query = query.Where(device => device.DeviceState == crit.DeviceState);
        }

        return query.ToList<DeviceData>();
    }
}
