using System;
using System.Collections.Generic;

namespace Quark.Abstractions;

public interface IQuarkTarget
{
    public List<ExecutingRunnerAsync> ManageActions { get; set; }
    public QuarkTargetTypes Type { get; init; }
    public string Name { get; init; }
    public List<IQuarkTask> Tasks { get; init; }
    public Dictionary<string, object> Facts { get; init; }
    public TargetStatus Status { get; set; }
}

[Flags]
public enum TargetStatus
{
    Unknown =  0 << 1,
    Healthy = 1 << 1,
    Faulted = 1 << 2,
    FactsGathered = 1 << 3,
}
