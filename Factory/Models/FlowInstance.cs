using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class FlowInstance
{
    [Key]
    public int Id { get; set; }

    public int WorkflowInstanceId { get; set; }

    public int FlowId { get; set; }

    public int FlowInstanceStatusId { get; set; }

    public DateTime? StartedOn { get; set; }

    public DateTime? EndedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual Flow Flow { get; set; } = null!;

    public virtual FlowInstanceStatus FlowInstanceStatus { get; set; } = null!;

    public virtual WorkflowInstance WorkflowInstance { get; set; } = null!;
}
