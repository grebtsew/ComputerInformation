using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ComputerInformation;

namespace MSTestComputerInformation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            // Act
            // Test create form1
            new Form1();
            // Assert
            Assert.IsTrue(true);
        }
    }
}
