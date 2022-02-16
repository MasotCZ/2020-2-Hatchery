using System.ComponentModel.DataAnnotations;

namespace CampWebAPISample.Models;

public class TalkModel
{
    public int TalkId { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Abstract { get; set; }

    public SpeakerModel Speaker { get; set; }
}