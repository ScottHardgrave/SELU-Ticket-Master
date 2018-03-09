namespace SP18.PF.Core.Features.Shared
{
    public sealed class State
    {
        public string Abbr { get; }
        public string Name { get; }

        public State(string abbr, string name)
        {
            this.Abbr = abbr;
            this.Name = name;
        }
    }
}