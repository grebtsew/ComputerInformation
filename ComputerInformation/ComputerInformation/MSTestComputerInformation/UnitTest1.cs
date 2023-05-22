using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ComputerInformation;

namespace MSTestComputerInformation
{
    [TestClass]
    public class UnitTests
    {
       
        [TestMethod]
        public void TestFormLoad()
        {

            // Act
            // Test create form1
            new Form1();
            // Assert
            Assert.IsTrue(true);
        }
    }
}
