using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

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
            return "name=" + Lastname;
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

        public string Firstname { get; set; }


        public string Lastname { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

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
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return Email + "\r\n" + Email2 + "\r\n" + Email3;
                }
            }

            set
            {
                allEmails = value;
            }
        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone =="")
            {
                return "";
            }
            return phone.Replace(" ","").Replace("-","").Replace("(","").Replace(")","") + "\r\n";
        }

    }
}
