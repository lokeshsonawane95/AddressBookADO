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
            Console.WriteLine("4. Retrieve Contact Details by city or state from Database");
            Console.WriteLine("5. Insert Contact Details into Database tables");
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
                case 4:
                    addressBookOperations.RetrieveContanctsByCityOrState();
                    break;
                case 5:
                    InsertIntoTables();
                    addressBookOperations.RetrieveContactDetails();
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

        public static void InsertIntoTables()
        {
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            Details details = new Details();

            details.firstName = "Lok";
            details.lastName = "Son";
            details.address = "Warje";
            details.city = "Pune";
            details.state = "Maharashtra";
            details.zip = 411058;
            details.phoneNo = 9876543210;
            details.eMail = "lok.son@gmail.com";
            details.dateAdded = Convert.ToDateTime("2021-08-01");
            details.addressBookName = "D";

            bool result = addressBookOperations.InsertDataIntoTables(details);

            Console.WriteLine(result == true ? "Contact details are inserted into database" : "Contact details are not inserted into database");
        }
    }
}