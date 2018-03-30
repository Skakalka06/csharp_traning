using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class ContactCreationTests :TestBase
    {
      
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData();
            contact.Firstame = "Ivan";
            contact.Lastname = "Pupkin";

            app.Contacts.Create(contact);
        }

    }
}

