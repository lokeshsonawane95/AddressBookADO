using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookADO
{
    public class Details
    {
        public int contactID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public long phoneNo { get; set; }
        public string eMail { get; set; }
        public int addressBookNameId { get; set; }
        public string addressBookName { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
    }
}
