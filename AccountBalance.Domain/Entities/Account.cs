namespace AccountBalance.Domain.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name, double balance)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Balance = balance;
        }

        public void Credit(double amount)
        {
            Balance += amount;
        }

        public void Debit(double amount)
        {
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }
    }
}
