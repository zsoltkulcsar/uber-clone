namespace Uber.Common.Entities
{
    public abstract class TrackableEntity<T> : Entity<T>
        where T : struct
    {
        public DateTimeOffset CreatedDateTime { get; set; }

        public T? ModifiedById { get; set; }
    }
}
