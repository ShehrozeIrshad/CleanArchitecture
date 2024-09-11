namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public User
            (
            Guid id, string email, string password, 
            string firstName, string lastName, string device,
            string ipAddress
            )
            : base(id) { }

        private User() { }

        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string? Device { get; private set; }
        public string? IpAddress { get; private set; }
        public decimal Balance { get; private set; } = 5;
    }
}
