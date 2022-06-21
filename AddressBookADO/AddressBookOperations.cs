using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookADO
{
    public class AddressBookOperations
    {
        public static string connectionString = @"Data source = .; database = AddressBookSystem; integrated security = true";

        SqlConnection connection = new SqlConnection(connectionString);

        List<Details> contactDetails = new List<Details>();

        public List<Details> RetrieveContactDetails()
        {
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPRetrieveContactDetails", connection);

                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Details details = new Details();
                            details.contactID = reader.GetInt32(0);
                            details.firstName = reader.GetString(1);
                            details.lastName = reader.GetString(2);
                            details.address = reader.GetString(3);
                            details.city = reader.GetString(4);
                            details.state = reader.GetString(5);
                            details.zip = reader.GetInt32(6);
                            details.phoneNo = reader.GetInt64(7);
                            details.eMail = reader.GetString(8);
                            details.addressBookNameId = reader.GetInt32(9);
                            details.addressBookName = reader.GetString(10);
                            details.typeId = reader.GetInt32(11);
                            details.typeName = reader.GetString(12);

                            contactDetails.Add(details);
                        }

                        reader.Close();

                        connection.Close();

                        return contactDetails;
                    }
                    else
                    {
                        Console.WriteLine("No records");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadList()
        {
            if (contactDetails.Count > 0)
            {
                foreach (Details details in contactDetails)
                {
                    Console.WriteLine("Contact ID : " + details.contactID);
                    Console.WriteLine("First Name : " + details.firstName);
                    Console.WriteLine("Last Name : " + details.lastName);
                    Console.WriteLine("Address : " + details.address);
                    Console.WriteLine("City : " + details.city);
                    Console.WriteLine("State : " + details.state);
                    Console.WriteLine("Zip : " + details.zip);
                    Console.WriteLine("Phone number : " + details.phoneNo);
                    Console.WriteLine("Email : " + details.eMail);
                    Console.WriteLine("Address Book ID : " + details.addressBookNameId);
                    Console.WriteLine("Address Book Name : " + details.addressBookName);
                    Console.WriteLine("Type ID : " + details.typeId);
                    Console.WriteLine("Type Name : " + details.typeName);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No records");
            }
        }
    }
}
