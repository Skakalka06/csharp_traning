using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            app.Groups.CreateGroupIfNotExist();
            app.Contacts.CreateContactIfNotExist();
            ContactData contact = null;
            GroupData group = null;

            while (!app.Contacts.CheckingExistContactInGroups(out contact, out group))
            {
                ContactData c = new ContactData("4551", "33331");
                app.Contacts.Create(c);
            }
            List<ContactData> oldList = group.GetContacts();


            app.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }

    }
}
