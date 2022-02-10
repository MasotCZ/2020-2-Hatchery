﻿namespace CampWebAPISample.Models
{
    public class TalkModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public CampModel Camp { get; set; }
        public SpeakerModel Speaker { get; set; }
    }
}