namespace Domain
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public String Id { get; }
    }
}