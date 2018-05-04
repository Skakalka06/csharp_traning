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
            List<ContactData> oldList = null;
            ContactData contact = null;
            GroupData group = null;
            
            //for (int i = 0; i < GroupData.GetAll().Count; i++)
            //{
            //    group = GroupData.GetAll()[i];
            //    oldList = group.GetContacts();
            //    contact = ContactData.GetAll().Intersect(oldList).FirstOrDefault();
            //    if (contact != null)
            //    { break; }
            //}

            foreach (GroupData g in GroupData.GetAll())
            {
                group = g;
                oldList = group.GetContacts();
                contact = group.GetContacts().FirstOrDefault();
                if (contact != null) break;
            }
            if (contact == null)
            {
                System.Console.Out.Write("В группах нет контактов");
                return;
            }
            app.Contacts.DeleteContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
