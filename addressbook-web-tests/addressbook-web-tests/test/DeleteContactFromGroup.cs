using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class DeleteContactFromGroupTests :AuthTestBase
    {
        [Test]
        public void DeleteContactFromGroupTest()
        {
            app.Groups.CreateGroupIfNotExist();
            app.Contacts.CreateContactIfNotExist();
            List<ContactData> oldList = null;
            ContactData contact = null;
            GroupData group = null;

            while (!app.Groups.CheckingExistAnyContactsInGroups(out contact, out group))
            {
                group = GroupData.GetAll()[0];
                contact = ContactData.GetAll().First();

                app.Contacts.AddContactToGroup(contact, group);
            }
                oldList = group.GetContacts();
            
            app.Contacts.DeleteContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }

    }
}
