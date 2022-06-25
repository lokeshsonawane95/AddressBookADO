using AddressBookADO;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace AddressBookADOMSTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestMethod]
        public void AddingMultipleContactsIntoDataBaseUsingThreading()
        {
            List<Details> contactDetails = new List<Details>();
            contactDetails.Add(new Details { firstName = "Dave", lastName = "Batista", address = "Andheri", city = "Mumbai", state = "Maharashtra", zip = 546132, phoneNo = 8528528525, eMail = "teenasidhar@gmail.com", dateAdded = Convert.ToDateTime("2020-01-01"), addressBookName = "E" });
            contactDetails.Add(new Details { firstName = "Bruce", lastName = "Wayne", address = "Kothrud", city = "Pune", state = "Maharashtra", zip = 124424, phoneNo = 7568459855, eMail = "chelsysehrawat@gmail.com", dateAdded = Convert.ToDateTime("2020-01-01"), addressBookName = "E" });
            contactDetails.Add(new Details { firstName = "Sachin", lastName = "Bansal", address = "Andheri", city = "Mumbai", state = "Maharashtra", zip = 125144, phoneNo = 7539514566, eMail = "muditjain@gmail.com", dateAdded = Convert.ToDateTime("2021-01-01"), addressBookName = "F" });
            contactDetails.Add(new Details { firstName = "Binni", lastName = "Bansal", address = "Kothrud", city = "Pune", state = "Maharashtra", zip = 125184, phoneNo = 9638257895, eMail = "vineetgoyal@gmail.com", dateAdded = Convert.ToDateTime("2022-01-01"), addressBookName = "F" });

            AddressBookOperations addressBookOperations = new AddressBookOperations();
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            addressBookOperations.AddingMultipleContactDetailsUsingThreading(contactDetails);
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time: " + stopwatch.Elapsed);
        }

        [TestMethod]
        public void onCallingGetApi_ReturnAddressBook()
        {
            
            RestRequest request = new RestRequest("/AddressBook", Method.GET);
            
            IRestResponse response = client.Execute(request);
            
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            
            List<Details> dataResponse = JsonConvert.DeserializeObject<List<Details>>(response.Content);
            
            foreach (Details contactDetails in dataResponse)
            {
                Console.WriteLine("AddressBookName:- " + contactDetails.addressBookName + " First Name:- " + contactDetails.firstName + " Last Name:- " + contactDetails.lastName + " Address:- " + contactDetails.address + " City:- " + contactDetails.city + " State:- " + contactDetails.state + " Zip:- " + contactDetails.zip + " phone number:- " + contactDetails.phoneNo + " Email:- " + contactDetails.eMail + " Date:-" + contactDetails.dateAdded);
            }
            //adding data in database using threading
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            addressBookOperations.AddingMultipleContactDetailsUsingThreading(dataResponse);
        }
    }
}