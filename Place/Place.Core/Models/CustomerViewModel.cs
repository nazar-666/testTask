namespace Place.Core.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CountOfTasks { get; set; }
        public int DurationHours { get; set; }
        public int ExecutedTasks { get; set; }
        public double PercentOfExecutedTasks { get; set; }
        public double DurationOfExecutedTasks { get; set; }
    }
}