using System;
using System.Collections.Generic;
using System.Data;
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

        public void RetrieveContactDetails()
        {
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("select * from AddressBook", connection);

                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Details details = new Details();
                            details.contactID = reader.GetInt32(8);
                            details.firstName = reader.GetString(0);
                            details.lastName = reader.GetString(1);
                            details.address = reader.GetString(2);
                            details.city = reader.GetString(3);
                            details.state = reader.GetString(4);
                            details.zip = reader.GetInt32(5);
                            details.phoneNo = reader.GetInt64(6);
                            details.eMail = reader.GetString(7);
                            /*details.addressBookNameId = reader.GetInt32(9);
                            details.addressBookName = reader.GetString(10);
                            details.typeId = reader.GetInt32(11);
                            details.typeName = reader.GetString(12);*/

                            contactDetails.Add(details);
                        }

                        reader.Close();

                        connection.Close();

                        AddressBookOperations addressBookOperations = new AddressBookOperations();

                        addressBookOperations.ReadList(contactDetails);
                    }
                    else
                    {
                        Console.WriteLine("No records");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Details> ContactDetailsListMethod()
        {
            return contactDetails;
        }

        public void ReadList(List<Details> contactDetails)
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
                    //Console.WriteLine("Address Book ID : " + details.addressBookNameId);
                    //Console.WriteLine("Address Book Name : " + details.addressBookName);
                    //Console.WriteLine("Type ID : " + details.typeId);
                    //Console.WriteLine("Type Name : " + details.typeName);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No records");
            }
        }

        public bool UpdateContactDetails(Details details)
        {
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPUpdateContactDetails", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@firstname", details.firstName);
                    sqlCommand.Parameters.AddWithValue("@lastname", details.lastName);
                    sqlCommand.Parameters.AddWithValue("@address", details.address);
                    sqlCommand.Parameters.AddWithValue("@city", details.city);
                    sqlCommand.Parameters.AddWithValue("@addressBookName", details.addressBookName);

                    connection.Open();

                    int result = sqlCommand.ExecuteNonQuery();

                    connection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Details GettingUpdatedDetails(Details contact)
        {
            try
            {
                using (connection)
                {
                    string query = "Select a.firstname,a.lastname,a.address,a.city,a.state,a.zip,a.phonenumber,a.email,c.addressbookname from addressbook a join addressbookmapper b on a.contactid=b.contactid join addressbooknames c on c.addressbookid=b.addressbookid where a.firstname=@firstname and a.lastname=@lastname and c.addressbookname=@addressbookname";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@firstname", contact.firstName);
                    command.Parameters.AddWithValue("@lastname", contact.lastName);
                    command.Parameters.AddWithValue("@addressbookname", contact.addressBookName);
                    connection.Open();
                    
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    
                    if (sqlDataReader.HasRows)
                    {
                        
                        while (sqlDataReader.Read())
                        {
                            
                            Details contactDetails = new Details();
                            contactDetails.firstName = sqlDataReader.GetString(0);
                            contactDetails.lastName = sqlDataReader.GetString(1);
                            contactDetails.address = sqlDataReader.GetString(2);
                            contactDetails.city = sqlDataReader.GetString(3);
                            contactDetails.state = sqlDataReader.GetString(4);
                            contactDetails.zip = sqlDataReader.GetInt32(5);
                            contactDetails.phoneNo = sqlDataReader.GetInt64(6);
                            contactDetails.eMail = sqlDataReader.GetString(7);
                            contactDetails.addressBookName = sqlDataReader.GetString(8);
                            
                            
                            return contactDetails;
                        }

                        sqlDataReader.Close();
                        
                        connection.Close();
                        return null;
                    }
                    else
                    {
                        throw new Exception("No records");
                    }
                }
            }
            
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public void RetrieveDetailsInSpecificDateRange()
        {
            List<Details> detailsList = new List<Details>();

            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("select * from addressbook where dateadded between cast('2019-01-01' as date) and cast('2020-01-01' as date)", connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Details details = new Details();
                            details.firstName = sqlDataReader.GetString(0);
                            details.lastName = sqlDataReader.GetString(1);
                            details.address = sqlDataReader.GetString(2);
                            details.city = sqlDataReader.GetString(3);
                            details.state = sqlDataReader.GetString(4);
                            details.zip = sqlDataReader.GetInt32(5);
                            details.phoneNo = sqlDataReader.GetInt64(6);
                            details.eMail = sqlDataReader.GetString(7);

                            detailsList.Add(details);
                        }

                        sqlDataReader.Close();

                        connection.Close();

                        AddressBookOperations addressBookOperations = new AddressBookOperations();

                        addressBookOperations.ReadList(detailsList);
                    }
                    else
                    {
                        Console.WriteLine("No records found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RetrieveContanctsByCityOrState()
        {
            List<Details> detailsList = new List<Details>();

            try
            {
                using (connection)
                {
                    Console.WriteLine("Want data according to\nPress 1 for City\nPress other number for State");
                    Console.Write("Enter your choice : ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    string query = "";
                    if (choice == 1)
                    {
                        query = "select * from addressbook where city = 'Mumbai'";
                    }
                    else
                    {
                        query = "select * from addressbook where state = 'Maharashtra'";
                    }

                    SqlCommand sqlCommand = new SqlCommand(query, connection);

                    connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Details details = new Details();
                            details.firstName = sqlDataReader.GetString(0);
                            details.lastName = sqlDataReader.GetString(1);
                            details.address = sqlDataReader.GetString(2);
                            details.city = sqlDataReader.GetString(3);
                            details.state = sqlDataReader.GetString(4);
                            details.zip = sqlDataReader.GetInt32(5);
                            details.phoneNo = sqlDataReader.GetInt64(6);
                            details.eMail = sqlDataReader.GetString(7);

                            detailsList.Add(details);
                        }

                        sqlDataReader.Close();

                        connection.Close();

                        AddressBookOperations addressBookOperations = new AddressBookOperations();

                        addressBookOperations.ReadList(detailsList);
                    }
                    else
                    {
                        Console.WriteLine("No records found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertDataIntoTables(Details details)
        {
            List<Details> detailsList = new List<Details>();

            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SPInsertContactDetails", connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@firstname", details.firstName);
                    sqlCommand.Parameters.AddWithValue("@lastname", details.lastName);
                    sqlCommand.Parameters.AddWithValue("@address", details.address);
                    sqlCommand.Parameters.AddWithValue("@city", details.city);
                    sqlCommand.Parameters.AddWithValue("@state", details.state);
                    sqlCommand.Parameters.AddWithValue("@zip", details.zip);
                    sqlCommand.Parameters.AddWithValue("@phonenumber", details.phoneNo);
                    sqlCommand.Parameters.AddWithValue("@email", details.eMail);
                    sqlCommand.Parameters.AddWithValue("@dateadded", details.dateAdded);
                    sqlCommand.Parameters.AddWithValue("@addressbookname", details.addressBookName);
                    connection.Open();

                    int result = sqlCommand.ExecuteNonQuery();

                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }

                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void AddingMultipleContactDetailsUsingThreading(List<Details> contactDetails)
        {
            contactDetails.ForEach(contactData =>
            {
                
                Thread thread = new Thread(()=>
                {
                    Console.WriteLine("Address being added" + contactData.firstName);
                    InsertDataIntoTables(contactData);

                    Console.WriteLine("Thread id: " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("Contact added:" + contactData.firstName);
                });
                
                thread.Start();
                
                thread.Join();

            });
        }
    }
}
