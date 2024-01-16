using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class Branch
{
    [Key]
    public int Id { get; set; }

    public int FlowId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? NextFlowId { get; set; }

    public virtual Flow Flow { get; set; } = null!;

    public virtual Flow? NextFlow { get; set; }
}
