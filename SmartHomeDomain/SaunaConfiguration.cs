using System;

public class SaunaConfiguration
{
    public long Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string On { get; set; }
    public string Off { get; set; }
    public long Sauna { get; set; }
}