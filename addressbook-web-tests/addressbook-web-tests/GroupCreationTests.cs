﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests :TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData ("admin", "secret"));
            GoToGroupsPage();
            InitNewGroupCreation();
            GroupData group = new GroupData("111");
            group.Header = "222";
            group.Footer = "333";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            //driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}