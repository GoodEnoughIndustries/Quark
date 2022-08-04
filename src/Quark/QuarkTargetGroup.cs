using Microsoft.Extensions.Hosting;
using Polly;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quark;

public class QuarkTargetGroup : IQuarkTargetGroup
{
    public QuarkTargetGroup(
        string pattern,
        QuarkTargetTypes targetType,
        Action<IQuarkTargetBuilder> targetBuilder,
        params object[] tags)
    {
        this.Pattern = pattern;
        this.Type = targetType;
        this.Tags = tags;
        this.Builder = targetBuilder;
    }

    public QuarkTargetTypes Type { get; }
    public string Pattern { get; }
    public List<IQuarkTarget> Targets { get; set; } = new();
    public IEnumerable<object> Tags { get; set; } = new List<object>();
    public Action<IQuarkTargetBuilder> Builder { get; set; }
    public List<IQuarkTask> QuarkTasks { get; set; } = new List<IQuarkTask>();
    public List<IQuarkCredential> Credentials { get; set; } = new();
    public List<Func<IHostBuilder, IQuarkTask>> BuildActions { get; set; } = new();

    public void BuildTasks(IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var targetBuilder = new QuarkTargetBuilder();
        this.Builder(targetBuilder);
        this.BuildActions.AddRange(targetBuilder.BuildActions);
        List<IQuarkTask> tasks = new();

        targetBuilder.BuildActions.ForEach(ba => tasks.Add(ba(builder)));

        this.Credentials.AddRange(targetBuilder.Credentials);
        this.QuarkTasks.AddRange(targetBuilder.QuarkTasks);
        this.BuildActions.AddRange(targetBuilder.BuildActions);

        //if (expander.TryExpand(this, out var targets))
        //{
        //    this.Targets.AddRange(targets);
        //}
        //else
        //{
        //    this.Targets.Add(new QuarkTarget
        //    {
        //        Name = this.Pattern,
        //        Type = this.Type,
        //    });
        //}
        //
        //
        //foreach (var target in this.Targets)
        //{
        //    target.Tasks.AddRange(this.QuarkTasks);
        //}
        //
        //List<Task> buildTasks = new();
        //foreach (var target in this.Targets)
        //{
        //    foreach (var task in this.QuarkTasks)
        //    {
        //        task.Targets.Add(target);
        //        //buildTasks.Add(task.BuildAsync(context, token));
        //    }
        //}
    }

    public Task BuildTasksAsync(IQuarkExecutionContext context, IQuarkTargetExpander expander, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(expander);

        var targetBuilder = new QuarkTargetBuilder();
        this.Builder(targetBuilder);

        if (targetBuilder.TargetGroups.Any())
        {

        }

        this.Credentials.AddRange(targetBuilder.Credentials);
        this.QuarkTasks.AddRange(targetBuilder.QuarkTasks);
        this.BuildActions.AddRange(targetBuilder.BuildActions);

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


        foreach (var target in this.Targets)
        {
            target.Tasks.AddRange(this.QuarkTasks);
        }

        List<Task> buildTasks = new();
        foreach (var target in this.Targets)
        {
            foreach (var task in this.QuarkTasks)
            {
                task.Targets.Add(target);
                buildTasks.Add(task.BuildAsync(context, token));
            }
        }

        return Task.WhenAll(buildTasks);
    }

    public IQuarkTargetGroup WithTag(object tag)
        => WithTags(new[] { tag });

    public IQuarkTargetGroup WithTags(params object[] tags)
    {
        return this;
    }
}
