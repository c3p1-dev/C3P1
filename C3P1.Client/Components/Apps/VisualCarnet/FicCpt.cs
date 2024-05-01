using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C3P1.Client.Components.Apps.VisualCarnet;

public partial class FicCpt
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(6)]
    public string CodCpt { get; set; } = null!;

    [Required, MaxLength(35)]
    public string? Nom { get; set; }

    [Required, MaxLength(1)]
    public string? Visible { get; set; }
}
