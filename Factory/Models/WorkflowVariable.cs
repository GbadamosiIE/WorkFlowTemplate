using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models;

public partial class WorkflowVariable
{
    [Key]
    public int Id { get; set; }

    public int WorkflowId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;
}
