namespace test
{
    public class Customer
    {
        public string Street { get; set; }
        public double Bill { get; set; }

        public Customer(string street, int bill)
        {
            Street = street;
            Bill = bill;
        }
    }
}