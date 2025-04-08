using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG2500_A2_Chinook.Helpers;
using System;

namespace PROG2500_A2_Chinook.Tests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void GetFirstCharacter_ValidInput_ReturnsFirstCharUpper()
        {
            string input = "test";
            string result = StringHelper.GetFirstCharacter(input);
            Assert.AreEqual("T", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstCharacter_EmptyInput_ThrowsException()
        {
            string input = "";
            StringHelper.GetFirstCharacter(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstCharacter_NullInput_ThrowsException()
        {
            string input = null;
            StringHelper.GetFirstCharacter(input);
        }

        [TestMethod]
        public void FormatName_ValidInput_ReturnsLastFirstFormat()
        {
            string firstName = "John";
            string lastName = "Doe";
            string result = StringHelper.FormatName(firstName, lastName);
            Assert.AreEqual("Doe, John", result);
        }

        [TestMethod]
        public void FormatName_EmptyFirstName_ReturnsOnlyLastName()
        {
            string firstName = "";
            string lastName = "Doe";
            string result = StringHelper.FormatName(firstName, lastName);
            Assert.AreEqual("Doe", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatName_EmptyLastName_ThrowsException()
        {
            string firstName = "John";
            string lastName = "";
            StringHelper.FormatName(firstName, lastName);
        }

        [TestMethod]
        public void FormatLocationInfo_ValidCityAndState_ReturnsCombined()
        {
            string city = "Boston";
            string state = "MA";
            string result = StringHelper.FormatLocationInfo(city, state);
            Assert.AreEqual("Boston, MA", result);
        }

        [TestMethod]
        public void FormatLocationInfo_EmptyState_ReturnsOnlyCity()
        {
            string city = "London";
            string state = "";
            string result = StringHelper.FormatLocationInfo(city, state);
            Assert.AreEqual("London", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatLocationInfo_EmptyCity_ThrowsException()
        {
            string city = "";
            string state = "ON";
            StringHelper.FormatLocationInfo(city, state);
        }

        [TestMethod]
        public void ContainsIgnoreCase_MatchingText_ReturnsTrue()
        {
            string source = "Hello World";
            string search = "world";
            bool result = StringHelper.ContainsIgnoreCase(source, search);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TruncateString_StringLongerThanMaxLength_ReturnsTruncatedString()
        {
            string input = "This is a long string that should be truncated";
            int maxLength = 10;
            string result = StringHelper.TruncateString(input, maxLength);
            Assert.AreEqual("This is a ...", result);
        }
    }
}
