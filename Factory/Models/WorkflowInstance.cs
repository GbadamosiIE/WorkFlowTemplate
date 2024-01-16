using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class WorkflowInstance
{
    [Key]
    public int Id { get; set; }

    public int WorkflowId { get; set; }

    public string? Parameters { get; set; }

    public Guid StartedBy { get; set; }

    public DateTime StartedOn { get; set; }

    public DateTime? EndedOn { get; set; }

    public int WorkflowInstanceStatusId { get; set; }

    public Guid? ExternalReferenceInstanceId { get; set; }

    public string? Notes { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }

    public virtual ICollection<FlowInstance> FlowInstances { get; } = new List<FlowInstance>();

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual WorkflowInstanceStatus WorkflowInstanceStatus { get; set; } = null!;
}
