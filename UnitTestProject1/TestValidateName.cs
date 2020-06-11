using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class TestValidateName
    {
        [TestMethod]
        public void CheckDigit()
        {
            List<Catalog> catalogs = new List<Catalog>() { };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, "1");
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckSpace()
        {
            List<Catalog> catalogs = new List<Catalog>() { };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, " ");
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckTheSameName()
        {
            string name = "name";
            List<Catalog> catalogs = new List<Catalog>() { new Catalog { Id = 1, Name = name } };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, name);
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckEmptyName()
        {
            List<Catalog> catalogs = new List<Catalog>() { };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, "");
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckDigitWithSpace()
        {
            List<Catalog> catalogs = new List<Catalog>() { };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, " 5 ");
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckDigitWithString()
        {
            List<Catalog> catalogs = new List<Catalog>() { };
            bool res;
            res = UserInteraction.UserInput.IsWrongName(catalogs, " 5s");
            Assert.AreEqual(false, res);
        }
    }
}