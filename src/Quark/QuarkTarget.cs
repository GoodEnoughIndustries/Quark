using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark;

public class QuarkTarget : IQuarkTarget
{
    public string Name { get; init; } = string.Empty;
    public QuarkTargetTypes Type { get; init; } = QuarkTargetTypes.Unknown;
    public TargetStatus Status { get; set; } = TargetStatus.Unknown;
    public List<IQuarkTask> Tasks { get; init; } = new();
    public Dictionary<string, object> Facts { get; init; } = new();
    public List<ExecutingRunnerAsync> ManageActions { get; set; } = new();

    public override string ToString() => $"{this.Type}: {this.Name}";
}
