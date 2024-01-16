using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class WorkflowInstanceStatus
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<WorkflowInstance> WorkflowInstances { get; } = new List<WorkflowInstance>();
}
