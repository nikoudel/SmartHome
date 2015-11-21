using System;
using System.Linq;
using System.Web.Http;

public class GetSaunaStateController : ApiController
{
    public SaunaState Post(SaunaState item)
    {
        // find the room by a given room ID (SaunaStateData.Sauna)
        Room sauna = Program.Rooms.Items.Where(s => s.Id == item.Sauna).FirstOrDefault();

        if (sauna == null)
        {
            throw new ApplicationException("Room not found: " + item.Sauna);
        }

        // return the room state on the given DateTime
        return ((Sauna)sauna).GetState(item.DateTime);
    }
}
