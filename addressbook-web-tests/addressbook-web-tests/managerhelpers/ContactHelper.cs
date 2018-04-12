﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper :HelperBase
    {

        public ContactHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.OpenHomePage();

                SelectContact(v);
                RemoveContact();
                SubmitDeleted();

            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

                SelectEditContact(index);
                FillContactForm(newData);
                SubmitContactModification();
                ReturnToHomePage();

            return this;
        }



        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenAddNewPage();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        public void CreateContactIfNotExist()
        {
            manager.Navigator.OpenHomePage();
            if (!IsContactExist())
            {
                ContactData contact = new ContactData
                {
                    Firstame = "Maria",
                    Lastname = "Pupkina"
                };
                Create(contact);
            }
        }

        public bool IsContactExist()
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])"));
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstame);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }



        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }


        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitDeleted()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper SelectEditContact(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
