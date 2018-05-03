using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails ="";
        private string fullInformation;

        public ContactData()
        {

        }

        public ContactData(string lastname, string firstname)
        {
            Firstname = firstname;
            Lastname = lastname;
        
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return   (Lastname == other.Lastname) && (Firstname == other.Firstname);
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + Firstname + "\nlastname=" + Lastname + "\naddress=" + Address;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int compareLastname = Lastname.CompareTo(other.Lastname);

            if (compareLastname == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return compareLastname;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "Id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {

                if (!string.IsNullOrEmpty(allEmails))
                {
                    return allEmails;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        allEmails = Email;
                    }
                    if (!string.IsNullOrEmpty(Email2))
                    {
                        if (!string.IsNullOrEmpty(Email))
                        {
                            allEmails = allEmails + Environment.NewLine;
                        }
                        allEmails = allEmails + Email2;
                    }

                    if (!string.IsNullOrEmpty(Email3))
                    {
                        if (!string.IsNullOrEmpty(Email2) || !string.IsNullOrEmpty(Email)) 
                        {
                            allEmails = allEmails + Environment.NewLine;
                        }
                        allEmails = allEmails + Email3;

                    }

                    return allEmails;
                }
            }

            set
            {
                allEmails = value;
            }
        }

        public string FullInformation
        {
            get
            {
                if (fullInformation != null)
                {
                    return fullInformation;
                }
                else
                {
                   
                    string full = Lastname + " " + Firstname + Environment.NewLine;


                    if (Address != null)
                    {
                        full = full + Address;

                    }

                    if (!string.IsNullOrEmpty(HomePhone))
                    {

                        full = Environment.NewLine + full + Environment.NewLine + ("H: " + HomePhone) + Environment.NewLine;
                    }

                    if (!string.IsNullOrEmpty(MobilePhone))
                    {
                        if (string.IsNullOrEmpty(HomePhone))
                        {
                            full = full + Environment.NewLine;
                        }
                        full = full + ("M: " + MobilePhone) + Environment.NewLine;

                    }
                    if (!string.IsNullOrEmpty(WorkPhone))
                    {
                        if (string.IsNullOrEmpty(MobilePhone) && string.IsNullOrEmpty(HomePhone))
                        {
                            full = full + Environment.NewLine;
                        }
                        full = full + ("W: " + WorkPhone) + Environment.NewLine;

                    }
                    if (!string.IsNullOrEmpty(Email))
                    {
                        full = full + Environment.NewLine + Email;
                        if (!string.IsNullOrEmpty(Email2)|| !string.IsNullOrEmpty(Email3))
                        {
                            full = full + Environment.NewLine;
                        }
                    }


                    if (!string.IsNullOrEmpty(Email2))
                    {
                        if (string.IsNullOrEmpty(Email))
                        {
                            full = full + Environment.NewLine;
                        }


                        full = full + Email2; 

                        if (!string.IsNullOrEmpty(Email3))
                        {
                            full = full + Environment.NewLine;
                        }
                    }

                    if (!string.IsNullOrEmpty(Email3))
                    {
                        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Email2))
                        {
                            full = full + Environment.NewLine;
                        }
                        full = full +  Email3;
                    }

                    return full;
                }
            }

            set
            {
                fullInformation = value;
            }
        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone =="")
            {
                return "";
            }
            return Regex.Replace(phone, "[-() ]", "") + "\r\n";
        }


        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        where c.Deprecated == "0000-00-00 00:00:00"
                        select c).ToList();
            }
        }
    }
}
