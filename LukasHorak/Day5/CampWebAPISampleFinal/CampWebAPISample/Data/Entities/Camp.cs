﻿namespace CampWebAPISample.Data.Entities;

public class Camp
{
    public int CampId { get; set; }
    public string Name { get; set; }
    public string Moniker { get; set; }

    public Location Location { get; set; }

    public DateTime EventDate { get; set; }
    public int Length { get; set; } = 1;

    public ICollection<Talk> Talks { get; set; }
}