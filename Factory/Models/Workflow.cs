using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class Workflow
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ApplicationName { get; set; } = null!;

    public Guid ExternalReferenceId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public Guid ModifiedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Flow> Flows { get; } = new List<Flow>();

    public virtual ICollection<WorkflowInstance> WorkflowInstances { get; } = new List<WorkflowInstance>();

    public virtual ICollection<WorkflowVariable> WorkflowVariables { get; } = new List<WorkflowVariable>();
}
