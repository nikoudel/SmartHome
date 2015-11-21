using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Sauna : Room
{
    private List<SaunaConfiguration> configurations;

    private const int NORMAL_TEMP = 22;
    private const int MAX_TEMP = 90;
    private const string DATE = "yyyy-MM-dd";
    private const string TIME = "HH:mm";

    public Sauna() : base()
    {
        configurations = new List<SaunaConfiguration>();
    }

    public SaunaConfiguration AddConfiguration(SaunaConfiguration conf)
    {
        if(conf.Sauna != this.Id)
        {
            throw new ApplicationException("Configuration vs sauna id mismatch!");
        }

        if(isDateTimeOverlapped(conf))
        {
            throw new ApplicationException("The sauna is already configured for the given time!");
        }

        configurations.Add(conf);

        return conf;
    }

    public SaunaState GetState(DateTime dt)
    {
        SaunaState state = new SaunaState();

        //the sauna is off
        state.Sauna = this.Id;
        state.DateTime = dt;
        state.Stove = "Off";
        state.Light = "Off";
        state.Temperature = NORMAL_TEMP;

        SaunaConfiguration conf = getMatchingConfiguration(dt);

        if (conf == null)
        {
            return state;
        }

        state.Temperature = getTemperature(conf, dt);

        //turn the light on when it's hot enough
        state.Light = state.Temperature > 60 ? "On" : "Off";

        //turn the stove on / off if it's too cold / hot
        state.Stove = state.Temperature < MAX_TEMP ? "On" : "Off";

        DateTime offTime = parseTime(conf.Off, dt.ToString(DATE));

        if (dt >= offTime)
        {
            state.Light = "Off";
            state.Stove = "Off";
        }

        return state;
    }

    private bool isDateTimeOverlapped(SaunaConfiguration conf)
    {
        foreach(SaunaConfiguration existingConf in configurations.Where(c => c.DayOfWeek == conf.DayOfWeek))
        {
            if(isTimeOverlapped(conf.On, conf.Off, existingConf.On, existingConf.Off))
            {
                return true;
            }
        }
        return false;
    }

    private bool isTimeOverlapped(string t1On, string t1Off, string t2On, string t2Off)
    {
        DateTime dt1On = parseTime(t1On);
        DateTime dt1Off = parseTime(t1Off);
        DateTime dt2On = parseTime(t2On);
        DateTime dt2Off = parseTime(t2Off);

        if(dt1On >= dt2On && dt1On <= dt2Off)
        {
            return true;
        }

        if (dt2On >= dt1On && dt2On <= dt1Off)
        {
            return true;
        }

        return false;
    }

    private DateTime parseTime(string time, string date = "2001-01-01")
    {
        try
        {
            return DateTime.ParseExact(date + " " + time.Trim(), DATE + " " + TIME, CultureInfo.InvariantCulture); ;
        }catch(Exception)
        {
            throw new ApplicationException("Failed parsing time; time is expected to be in " + TIME + " format.");
        }
    }  

    private int getTemperature(SaunaConfiguration conf, DateTime dt)
    {
        DateTime on = parseTime(conf.On, dt.ToString(DATE));
        DateTime off = parseTime(conf.Off, dt.ToString(DATE));
        double temp;

        if(dt < off)
        {
            //warming up
            temp = NORMAL_TEMP + 2.3 * (dt - on).TotalMinutes;
        }
        else
        {
            //cooling down
            temp = MAX_TEMP - 0.5 * (dt - off).TotalMinutes;
        }

        if(temp > MAX_TEMP)
        {
            temp = MAX_TEMP;
        }

        if(temp < NORMAL_TEMP)
        {
            temp = NORMAL_TEMP;
        }

        return (int)temp;
    }

    private SaunaConfiguration getMatchingConfiguration(DateTime dt)
    {
        string time = dt.ToString(TIME);

        foreach(SaunaConfiguration conf in configurations.Where(c => c.DayOfWeek == dt.DayOfWeek))
        {
            if(isTimeOverlapped(time, time, conf.On, conf.Off))
            {
                return conf;
            }
        }
        return null;
    }
}
