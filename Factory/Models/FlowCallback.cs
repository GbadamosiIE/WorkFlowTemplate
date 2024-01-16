using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class FlowCallback
{
    [Key]
    public int Id { get; set; }

    public string CallbackUrl { get; set; } = null!;

    public string CallbackMetadata { get; set; } = null!;

    public int? FlowId { get; set; }

    public virtual Flow? Flow { get; set; }
}
