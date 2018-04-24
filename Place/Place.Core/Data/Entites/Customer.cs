using Place.Core.Data.Entites.Abstract;

namespace Place.Core.Data.Entites
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CountOfTasks { get; set; }
        public int DurationHours { get; set; }
        public int ExecutedTasks { get; set; }
    }
}
