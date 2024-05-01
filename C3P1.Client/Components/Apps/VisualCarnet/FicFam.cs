using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C3P1.Client.Components.Apps.VisualCarnet;

public partial class FicFam
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(6)]
    public string? CodFam { get; set; }

    [MaxLength(35)]
    public string? Nom { get; set; }
}
