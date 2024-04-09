namespace Battleships.Game.Tests
{
    // Minimum coverage for early development.

    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        [DataRow(new int[] { 2, 1 }, 1)]
        [DataRow(new int[] { 1, 2, 3, 5, 7, 8 }, 2)]
        public void GetConsecutiveGroupsReturnsExpectedGroups(int[] list, int expectedCount)
        {
            // Arrange

            // Act
            var result = Utilities.GetConsecutiveGroups([.. list]);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<List<int>>));
            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        [DataRow('C', 2)]
        [DataRow('Z', 25)]
        public void GetLetterIndex(char toFind, int expected)
        {
            // Arrange

            // Act
            var result = Utilities.GetLetterIndex(toFind);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
