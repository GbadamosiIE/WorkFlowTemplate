using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WorkflowController:ControllerBase
{
    private readonly Workflow2Context _context;
    public WorkflowController(Workflow2Context context)
    {
        _context = context;
    }
    [HttpGet("workflows")]
    public async Task<IActionResult> GetWorkflows(string? FilterParameter)
    {
        var workflows = await _context.Workflows.ToListAsync();
        if (FilterParameter == null)
        {
            return Ok(workflows);
        }
        else
        {
            var filteredWorkflows = workflows.Where(w => w.Name.Contains(FilterParameter) || w.Description.Contains(FilterParameter) || w.ApplicationName.Contains(FilterParameter) || w.ExternalReferenceId.ToString().Contains(FilterParameter) || w.IsActive.ToString().Contains(FilterParameter));
            return Ok(filteredWorkflows);

        }
    }
  
[HttpPost("createworkflow")]
public async Task<IActionResult> CreateWorkFlow(CreateWorkflowDto payload)
{
    if (payload == null)
    {
        return BadRequest("Workflow Creation Failed");
    }

    var workflow = new Workflow
    {
        Name = payload.Name,
        Description = payload.Description,
        ApplicationName = payload.ApplicationName,
        ExternalReferenceId = Guid.NewGuid(),
        CreatedOn = DateTime.Now,
        CreatedBy = Guid.NewGuid(),
        ModifiedOn = DateTime.Now,
        ModifiedBy = Guid.NewGuid(),
        IsActive = true
    };

    await _context.Workflows.AddAsync(workflow);
    var flows = payload.Flows.Select(flow => new Flow
    {
        Name = flow.Name,
        Description = flow.Description,
        Workflow = workflow,
        BranchFlows = flow.Branches.Select(branch => new Branch
        {
            Name = branch.Name,
            Description = branch.Description,
            NextFlowId = Convert.ToInt32(branch.NextFlowId),
        }).ToList()
    }).ToList();
    var variables = new List<WorkflowVariable>();
    if(payload.Variables != null)
    {
       variables = payload.Variables.Select(variable => new WorkflowVariable
        {
            Name = variable.Name,
            Type = variable.VariableType,
            Value = variable.Value,
            Workflow = workflow
        }).ToList();
        await _context.WorkflowVariables.AddRangeAsync(variables);
    }
    await _context.Flows.AddRangeAsync(flows);
    await _context.Branches.AddRangeAsync(flows.SelectMany(flow => flow.BranchFlows));
    

    await _context.SaveChangesAsync();

    return Ok("Workflow Created Successfully");
}

[HttpPost("create-workflow-instance")]
public async Task<IActionResult> CreateWorkflowInstance(string workflowid, CreateWorkflowInstanceDto payload){
    if (payload == null || workflowid == null)
    {
        return Ok("Workflow Instance Creation Failed");
    }
    var workflow = await _context.Workflows.FindAsync(Convert.ToInt32(workflowid));
    if (workflow == null)
    {
        return BadRequest("Workflow Not Found");
    }
    var workflowInstance = new WorkflowInstance()
    {
        WorkflowId = workflow.Id,
        Parameters = payload.Parameter,
        Notes = payload.Notes,
        StartedOn = DateTime.Now,
        CreatedBy = Guid.NewGuid(),
        ModifiedBy = Guid.NewGuid(),
        Workflow = workflow,
        WorkflowInstanceStatusId = 1
    };
    await _context.WorkflowInstances.AddAsync(workflowInstance);
    await _context.SaveChangesAsync();
    return Ok("Workflow Instance Created Successfully");
}
[HttpGet("workflow-definition/{workflowid}")]
public async Task<IActionResult> WorkflowDefinition(string workflowid){
    if (workflowid == null)
    {
        return Ok(new ArgumentNullException("Workflow Id is Null"));
    }
    var workflow = await _context.Workflows.FindAsync(Convert.ToInt32(workflowid));
    if (workflow == null)
    {
        return BadRequest("Workflow Not Found");
    }
    var workflowDefinition = new CreateWorkflowDto()
    {
        Name = workflow.Name,
        Description = workflow.Description,
        ApplicationName = workflow.ApplicationName,
        ExternalReferenceId = workflow.ExternalReferenceId.ToString(),
        Flows = workflow.Flows.Select(f => new FlowDto()
        {
            Name = f.Name,
            Description = f.Description,
            Branches = f.BranchFlows.Select(b => new BranchDto()
            {
                Name = b.Name,
                Description = b.Description,
                NextFlowId = b.NextFlowId.ToString()
            }).ToList()
        }).ToList(),
        Variables = workflow.WorkflowVariables.Select(v => new Variable()
        {
            Name = v.Name,
            VariableType = v.Type,
            Value = v.Value
        }).ToList()
    };
    return Ok(workflowDefinition);
}
[HttpGet("workflow-status/{workflowid}")]
public async Task<IActionResult> WorkflowStatus(string WorkflowIdOrApplicationReferenceId){
    if (WorkflowIdOrApplicationReferenceId == null)
    {
        return Ok(new ArgumentNullException("Workflow Id is Null"));
    }
    var workflow = await _context.Workflows.FirstOrDefaultAsync(w => w.Id == Convert.ToInt32(WorkflowIdOrApplicationReferenceId) || w.ExternalReferenceId == Guid.Parse(WorkflowIdOrApplicationReferenceId));
    if (workflow == null)
    {
        return BadRequest("Workflow Not Found");
    }
    return Ok();
}
[HttpGet("change-workflow-instance-status/{workflowinstanceid}")]
public async Task<IActionResult> ChangeWorkflowInstanceStatus(string workflowinstanceid, string status){
    if (workflowinstanceid == null)
    {
        return Ok(new ArgumentNullException("Workflow Instance Id is Null"));
    }
    var workflowInstance = await _context.WorkflowInstances.FindAsync(Convert.ToInt32(workflowinstanceid));
    if (workflowInstance == null)
    {
        return BadRequest("Workflow Instance Not Found");
    }
    var statusId = await _context.WorkflowInstanceStatuses.FirstOrDefaultAsync(s => s.Name == status);
    if (statusId == null)
    {
        return BadRequest("Workflow Instance Status Not Found");
    }
    workflowInstance.WorkflowInstanceStatusId = statusId.Id;
    await _context.SaveChangesAsync();
    return Ok("Workflow Instance Status Changed Successfully");
}
[HttpGet("Workflow-history/{workflowid}")]
public async Task<IActionResult> WorkflowHistory(string workflowId, string? FilterParameter){
    if (workflowId == null)
    {
        return Ok(new ArgumentNullException("Workflow Id is Null"));
    }
    var workflow = await _context.Workflows.FindAsync(Convert.ToInt32(workflowId));
    if (workflow == null)
    {
        return BadRequest("Workflow Not Found");
    }
    var workflowInstances = await _context.WorkflowInstances.Where(w => w.WorkflowId == workflow.Id).ToListAsync();
    if (FilterParameter == null)
    {
        return Ok(workflowInstances);
    }
    else
    {
        var filteredWorkflowInstances = workflowInstances.Where(w => w.Parameters.Contains(FilterParameter) || w.Notes.Contains(FilterParameter) || w.StartedOn.ToString().Contains(FilterParameter) || w.WorkflowInstanceStatusId.ToString().Contains(FilterParameter));
        return Ok(filteredWorkflowInstances);
    }
}
[HttpPost("flow-instance")]
public async Task<IActionResult> FlowInstance(int FlowId, int workflowinstanceid){
    if (FlowId < 1 || workflowinstanceid < 1)
    {
        return Ok(new ArgumentNullException("Invalid Flow Id or Workflow Instance Id!!!"));
    }
    var flow = await _context.Flows.AnyAsync(flow => flow.Id ==Convert.ToInt32(FlowId));
    if (!flow )
    {
        return BadRequest("Flow Not Found");
    }
    var workflowInstance = await _context.WorkflowInstances.AnyAsync(WorkflowInstances => WorkflowInstances.Id ==Convert.ToInt32(workflowinstanceid));
    if (!workflowInstance)
    {
        return BadRequest("Workflow Instance Not Found");
    }
    var flowInstance = new FlowInstance()
    {
        FlowId = FlowId,
        WorkflowInstanceId = workflowinstanceid,
        FlowInstanceStatusId = 1,
        StartedOn = DateTime.Now,
    };
    await _context.FlowInstances.AddAsync(flowInstance);
    await _context.SaveChangesAsync();
    return Ok("Flow Instance Created Successfully");
}
[HttpGet("flow-instance-status/{flowinstanceid}")]
public async Task<IActionResult> FlowInstanceStatus(string flowinstanceid, string status){
    if (flowinstanceid == null)
    {
        return Ok(new ArgumentNullException("Flow Instance Id is Null"));
    }
    var flowInstance = await _context.FlowInstances.FindAsync(Convert.ToInt32(flowinstanceid));
    if (flowInstance == null)
    {
        return BadRequest("Flow Instance Not Found");
    }
    var statusId = await _context.FlowInstanceStatuses.FirstOrDefaultAsync(s => s.Name == status);
    if (statusId == null)
    {
        return BadRequest("Flow Instance Status Not Found");
    }
    flowInstance.FlowInstanceStatusId = statusId.Id;
    await _context.SaveChangesAsync();
    return Ok("Flow Instance Status Changed Successfully");
}
[HttpGet("flow-instance-history/{flowinstanceid}")]
public async Task<IActionResult> FlowInstanceHistory(string flowinstanceid, string? FilterParameter){
    if (flowinstanceid == null)
    {
        return Ok(new ArgumentNullException("Flow Instance Id is Null"));
    }
    var flowInstance = await _context.FlowInstances.Where(f => f.Id == Convert.ToInt32(flowinstanceid)).ToListAsync();
    if (FilterParameter == null)
    {
        return Ok(flowInstance);
    }
    else
    {
        var filteredFlowInstances = flowInstance.Where(f => f.StartedOn.ToString().Contains(FilterParameter) || f.FlowInstanceStatusId.ToString().Contains(FilterParameter));
        return Ok(filteredFlowInstances);
    }
}
[HttpGet("branch-instance/{branchid}")]
public async Task<IActionResult> BranchInstance(string branchid, string? FilterParameter){
    if (branchid == null)
    {
        return Ok(new ArgumentNullException("Branch Id is Null"));
    }
    var branch = await _context.Branches.Where(b => b.Id == Convert.ToInt32(branchid)).ToListAsync();
    if (FilterParameter == null)
    {
        return Ok(branch);
    }
    else
    {
        var filteredBranches = branch.Where(b => b.Name.Contains(FilterParameter) || b.Description.Contains(FilterParameter) || b.NextFlowId.ToString().Contains(FilterParameter));
        return Ok(filteredBranches);
    }
}
}
