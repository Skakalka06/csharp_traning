using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.test
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfNotExist();

            ContactData newData = new ContactData( "Zaycev","Misha");

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];


            app.Contacts.Modify(oldData, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldData.Firstname = newData.Firstname;
            oldData.Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
