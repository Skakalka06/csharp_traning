using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactRemovalTests
    {
        [TestFixture]
        public class GroupRemovalTests : TestBase
        {
            [Test]
            public void GroupRemovalTest()
            {
                app.Contacts.Remove(1);
            }
        }
    }
}
