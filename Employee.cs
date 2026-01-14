namespace AddSolBackend
{
    public class Employee
    {
        public int Id { get; set; } // Unique ID for database
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = "Offline";
    }
}

