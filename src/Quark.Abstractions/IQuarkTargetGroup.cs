using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark.Abstractions
{
    public interface IQuarkTargetGroup
    {
        public IQuarkTargetGroup WithTags(params object[] tags);
        public IQuarkTargetGroup WithTag(object tag);
        public QuarkTargetTypes Type { get; }
        public string Pattern { get; }
        public List<IQuarkTarget> Targets { get; set; }
        public Task BuildTasksAsync(IQuarkTargetExpander expander);
        public Action<IQuarkTargetBuilder> Builder { get; }
        public List<IQuarkTask> QuarkTasks { get; }
    }
}
