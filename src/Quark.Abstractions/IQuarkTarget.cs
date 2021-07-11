namespace Quark.Abstractions
{
    public interface IQuarkTarget
    {
        public QuarkTargetTypes Type { get; set; }
        public string Name { get; }
    }
}
