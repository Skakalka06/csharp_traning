using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {

            app.Groups.CreateGroupIfNotExist();

            GroupData newData = new GroupData("555")
            {
                Header = null,
                Footer = null
            };

            app.Groups.Modify(0, newData);
        }

    }
}
