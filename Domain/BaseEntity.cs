namespace Domain
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Id { get; }
    }
}