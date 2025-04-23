namespace OrderSolution.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string CNPJ { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
