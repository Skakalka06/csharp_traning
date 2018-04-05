using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.test
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
       
            [Test]
            public void ContactModificationTest()
            {
            ContactData newData = new ContactData();
            newData.Firstame = "Ivan";
            newData.Lastname = "Pupkin";

            app.Contacts.Modify(1, newData);
            }
    }
}
