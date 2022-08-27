using Microsoft.Extensions.Hosting;
using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark;

public class QuarkTargetGroup : IQuarkTargetGroup
{
    public QuarkTargetGroup(
        string pattern,
        QuarkTargetTypes targetType,
        ExecutingRunnerAsync targetManager,
        params object[] tags)
    {
        this.Pattern = pattern;
        this.Type = targetType;
        this.Tags = tags;
        this.Manager = targetManager;
    }

    public QuarkTargetTypes Type { get; }
    public string Pattern { get; }
    public List<IQuarkTarget> Targets { get; set; } = new();
    public IEnumerable<object> Tags { get; set; } = new List<object>();
    public ExecutingRunnerAsync Manager { get; set; }
    public List<IQuarkTask> QuarkTasks { get; set; } = new List<IQuarkTask>();
    public List<IQuarkCredential> Credentials { get; set; } = new();
    public List<ExecutingRunnerAsync> ManageActions { get; set; } = new();

    public void BuildTargets(IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        QuarkTargetExpander expander = new();

        if (expander.TryExpand(this, out var targets))
        {
            this.Targets.AddRange(targets);
        }
        else
        {
            this.Targets.Add(new QuarkTarget
            {
                Name = this.Pattern,
                Type = this.Type,
            });
        }
    }

    // public Task BuildTasksAsync(IQuarkExecutionContext context, IQuarkTargetExpander expander, CancellationToken token)
    // {
    //     ArgumentNullException.ThrowIfNull(expander);
    // 
    //     var targetBuilder = new QuarkTargetRunner();
    //     this.Manager(targetBuilder);
    // 
    //     this.Credentials.AddRange(targetBuilder.Credentials);
    //     this.QuarkTasks.AddRange(targetBuilder.QuarkTasks);
    //     this.ManageActions.AddRange(targetBuilder.ManageActions);
    // 
    //     if (expander.TryExpand(this, out var targets))
    //     {
    //         this.Targets.AddRange(targets);
    //     }
    //     else
    //     {
    //         this.Targets.Add(new QuarkTarget
    //         {
    //             Name = this.Pattern,
    //             Type = this.Type,
    //         });
    //     }
    // 
    // 
    //     foreach (var target in this.Targets)
    //     {
    //         target.Tasks.AddRange(this.QuarkTasks);
    //     }
    // 
    //     List<Task> buildTasks = new();
    //     foreach (var target in this.Targets)
    //     {
    //         foreach (var task in this.QuarkTasks)
    //         {
    //             task.Targets.Add(target);
    //             buildTasks.Add(task.BuildAsync(context, token));
    //         }
    //     }
    // 
    //     return Task.WhenAll(buildTasks);
    // }

    public IQuarkTargetGroup WithTag(object tag)
        => WithTags(new[] { tag });

    public IQuarkTargetGroup WithTags(params object[] tags)
    {
        return this;
    }
}
