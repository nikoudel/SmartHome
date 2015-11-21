using System;
using System.Web.Http;
using System.Web.Http.SelfHost;            

/// <summary>
/// Entry point of the program.
/// </summary>
public class Program
{
    public static readonly Repository<Room> Rooms = new Repository<Room>();
    public static readonly Repository<DeviceData> Devices = new Repository<DeviceData>();

    static void Main(string[] args)
    {
        // run as administrator: netsh http add urlacl url=http://+:56473/ user=machine\username
        // http://www.asp.net/web-api/overview/older-versions/self-host-a-web-api

        var config = new HttpSelfHostConfiguration("http://localhost:56473");

        config.Routes.MapHttpRoute("Default", "{controller}.json");

        config.Formatters.Remove(config.Formatters.XmlFormatter);

        config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
            new Newtonsoft.Json.Converters.StringEnumConverter());

        config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
            new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm" });

        using (HttpSelfHostServer server = new HttpSelfHostServer(config))
        {
            server.OpenAsync().Wait();

            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
        }
    }
}
