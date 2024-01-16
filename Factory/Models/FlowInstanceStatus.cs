using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class FlowInstanceStatus
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FlowInstance> FlowInstances { get; } = new List<FlowInstance>();
}
