namespace AddressBookADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            Console.WriteLine("\t\t\t\t\tWelcome to Address Book Program using ADO");
            Console.WriteLine("Retrieving Contact Details from Database");
            addressBookOperations.RetrieveContactDetails();
            addressBookOperations.ReadList();
        }
    }
}