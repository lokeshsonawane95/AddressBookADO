namespace AddressBookADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            Console.WriteLine("\t\t\t\t\tWelcome to Address Book Program using ADO");
            Console.WriteLine("1. Retrieve Contact Details from Database");
            Console.WriteLine("2. Update Contact Details in Database");
            Console.WriteLine("3. Retrieve Contact Details in specific date range from Database");
            Console.Write("Enter your choice : ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    addressBookOperations.RetrieveContactDetails();
                    break;
                case 2:
                    UpdateDatabase();
                    addressBookOperations.RetrieveContactDetails();
                    break;
                case 3:
                    addressBookOperations.RetrieveDetailsInSpecificDateRange();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Enter correct choice");
                    break;
            }
        }

        public static void UpdateDatabase()
        {
            Details details = new Details();

            details.firstName = "Lokesh";
            details.lastName = "Sonawane";
            details.address = "Warje";
            details.city = "Pune";
            details.addressBookName = "A";

            AddressBookOperations addressBookOperations = new AddressBookOperations();

            bool result = addressBookOperations.UpdateContactDetails(details);

            Console.WriteLine(result == true ? "Data is updated into database" : "Data is not updated into database");
        }
    }
}