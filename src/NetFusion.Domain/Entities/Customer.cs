namespace NetFusion.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }

        private Customer() { }

        public Customer(string name, string email, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
