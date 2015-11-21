using System;
using System.Linq;
using System.Web.Http;

public class ConfigureSaunaController : ApiController
{
    public SaunaConfiguration Post(SaunaConfiguration conf)
    {
        // find the needed sauna
        Sauna sauna = (Sauna)Program.Rooms.Items.FirstOrDefault(room => room.Id == conf.Sauna);

        if (sauna == null)
        {
            throw new ApplicationException("Room not found: " + conf.Sauna);
        }

        // configure the sauna with given parameters
        return sauna.AddConfiguration(conf);
    }
}