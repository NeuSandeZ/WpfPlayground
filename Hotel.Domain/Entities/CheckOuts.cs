using System.ComponentModel.DataAnnotations;

namespace Hotel.Domain.Entities;

public class CheckOuts
{
    [Key] public int Id { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public CheckIns CheckIns { get; set; }
    public int CheckInsId { get; set; }
}