using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickElastic.Core.DataProviders;

namespace QuickElastic.Tests.Core.DataProviders
{
    [TestClass]
    public class UserDataProviderTests
    {
        [TestMethod]
        public void WhenRequestingUserData_ThenDataIsReturned()
        {
            // Arrange
            var userDataProvider = new UserDataProvider();

            // Act
            var users = userDataProvider.GetData().Result;

            // Assert
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void WhenRequestingUserData_ThenUsernamesAreUnique()
        {
            // Arrange
            var userDataProvider = new UserDataProvider();

            // Act
            var users = userDataProvider.GetData().Result.ToList();

            // Assert
            Assert.AreEqual(users.Count, users.Select(u => u.Username).Distinct().Count());
            Assert.IsTrue(users.Any());
        }
    }
}
