namespace Factory;

public class AddFlow
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BranchDto> Branches { get; set; }
    }
public class CreateWorkflowDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ApplicationName { get; set; }
    public string ExternalReferenceId { get; set; }
    public ICollection<FlowDto> Flows { get; set; }
    public ICollection<Variable> Variables { get; set; }
}

public class FlowDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<BranchDto> Branches { get; set; }
}

public class BranchDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string NextFlowId { get; set; }
}

public class Variable
{
    public string Name { get; set; }
    public string VariableType { get; set; }
    public string Value { get; set; }
}