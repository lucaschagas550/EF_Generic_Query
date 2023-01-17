namespace EF.Generic_Query.API.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity() =>
            Id = Guid.NewGuid();
    }
}
