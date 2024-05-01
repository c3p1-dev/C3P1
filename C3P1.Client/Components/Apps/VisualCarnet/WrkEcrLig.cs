using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C3P1.Client.Components.Apps.VisualCarnet;

public partial class WrkEcrLig
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(6)]
    public string CodCpt { get; set; } = null!;

    [Required]
    public int Nol { get; set; }

    [Required]
    public DateOnly? Jma { get; set; }
    [Required]
    public DateOnly? JmaVal { get; set; }

    [MaxLength(7)]
    public string? NoChq { get; set; }

    [MaxLength(60)]
    public string? Lib1 { get; set; }

    [MaxLength(60)]
    public string? Lib2 { get; set; }

    public double? Deb { get; set; }

    public double? Cre { get; set; }

    [Required, MaxLength(6)]
    public string? CodSfa { get; set; }
    
    public double? SldProgressif { get; set; }
}
