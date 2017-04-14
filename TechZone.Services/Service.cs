namespace TechZone.Services
{
    using Data;

    public abstract class Service
    {
        protected Service()
        {
            this.Context = new TechZoneContext();
        }

        protected TechZoneContext Context { get; }
    }
}