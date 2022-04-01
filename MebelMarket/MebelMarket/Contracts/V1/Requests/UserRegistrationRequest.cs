namespace MebelMarket.Contracts.V1.Requests
{
    public sealed class UserRegistrationRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
