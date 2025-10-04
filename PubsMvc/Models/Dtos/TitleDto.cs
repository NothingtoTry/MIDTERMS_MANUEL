using System.ComponentModel.DataAnnotations;

public class TitleDto
{
    [Required, StringLength(6)]
    public string TitleId { get; set; } = null!;

    [Required, StringLength(80)]
    public string Title { get; set; } = null!;

    [Range(0, 1000)]
    public decimal? Price { get; set; }
}
