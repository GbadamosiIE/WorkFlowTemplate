using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class Flow
{
    [Key]
    public int Id { get; set; }

    public int WorkflowId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Branch> BranchFlows { get; set;} = new List<Branch>();

    public virtual ICollection<Branch> BranchNextFlows { get;set; } = new List<Branch>();

    public virtual ICollection<FlowCallback> FlowCallbacks { get; set; } = new List<FlowCallback>();

    public virtual ICollection<FlowInstance> FlowInstances { get; set; } = new List<FlowInstance>();

    public virtual Workflow Workflow { get; set; } = null!;
}
