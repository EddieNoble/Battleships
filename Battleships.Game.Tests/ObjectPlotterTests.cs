namespace Battleships.Game.Tests
{
    // Minimum coverage for early development.

    [TestClass]
    public class ObjectPlotterTests
    {
        [TestMethod]
        public void GetRange2dReturnsOnlyPossiblePointsCorrectly()
        {
            // Arrange
            var expectedResultCount = 2;

            // Act
            var result = ObjectPlotter.GetRange2d(2, 2, new List<Tuple<int, int>>());

            // Assert
            Assert.AreEqual(expectedResultCount, result.Count);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void GetRange2dIfInsufficentCooridnatesAreAvailableReturnsAnEmptyList(int testCase)
        {
            // Arrange
            TestParameters param = GetTest1Case(testCase);

            // Act
            var result = ObjectPlotter.GetRange2d(param.RequiredPoints, param.Size, param.ExcludedPoints);

            // Assert
            Assert.AreEqual(param.ExpectedResult, result.Count);
        }

        TestParameters GetTest1Case(int index)
        {
            var caseList = new List<TestParameters>();
            var excludedPoints = new List<Tuple<int, int>>();
            caseList.Add(new TestParameters(5, 2, excludedPoints, 0));
            excludedPoints = [new(0, 0)];
            caseList.Add(new TestParameters(4, 2, excludedPoints, 0));
            caseList.Add(new TestParameters(3, 2, excludedPoints, 0));
            excludedPoints.Add(new(1, 0));
            caseList.Add(new TestParameters(2, 2, excludedPoints, 0));
            caseList.Add(new TestParameters(2, 2, excludedPoints, 0));
            excludedPoints.Add(new(1, 0));
            excludedPoints.Add(new(2, 0));
            excludedPoints.Add(new(0, 1));
            excludedPoints.Add(new(0, 2));
            caseList.Add(new TestParameters(3, 3, excludedPoints, 0));
            // Add 6H
            excludedPoints = [new(4, 0), new(4, 1), new(4, 2), new(4, 3), new(4, 4), new(4, 5)];
            // Add 5V
            var v5 = new List<Tuple<int, int>> { new(0, 4), new(1, 4), new(2, 4), new(3, 4), new(5, 4) };
            excludedPoints.AddRange(v5);
            caseList.Add(new TestParameters(6, 6, excludedPoints, 0));

            return caseList[index];
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void GetRange2dIfSufficentCooridnatesAreAvailableReturnsExpectedRows(int testCase)
        {
            // Arrange
            TestParameters param = GetTest2Case(testCase);

            // Act
            var result = ObjectPlotter.GetRange2d(param.RequiredPoints, param.Size, param.ExcludedPoints);

            // Assert
            Assert.AreEqual(param.ExpectedResult, result.Count);
        }

        TestParameters GetTest2Case(int index)
        {
            var caseList = new List<TestParameters>();
            var excludedPoints = new List<Tuple<int, int>>();
            caseList.Add(new TestParameters(2, 2, excludedPoints, 2));
            excludedPoints = [new(0, 0)];
            caseList.Add(new TestParameters(3, 3, excludedPoints, 3));
            excludedPoints.Add(new(1, 0));
            caseList.Add(new TestParameters(3, 3, excludedPoints, 3));
            // Add 5H
            excludedPoints = [new(2, 3), new(2, 4), new(2, 5), new(2, 6), new(2, 7)];
            // Add 4V
            var v4 = new List<Tuple<int, int>> { new(2, 4), new(3, 4), new(4, 4), new(5, 4) };
            excludedPoints.AddRange(v4);
            caseList.Add(new TestParameters(4, 10, excludedPoints, 4));
            return caseList[index];
        }

        private class TestParameters
        {
            public int RequiredPoints { get; }
            public int Size { get; }
            public List<Tuple<int, int>> ExcludedPoints { get; }
            public int ExpectedResult { get; }

            public TestParameters(int requiredPoints, int size, List<Tuple<int, int>> excludedPoints, int expectedResult)
            {
                RequiredPoints = requiredPoints;
                Size = size;
                ExcludedPoints = excludedPoints;
                ExpectedResult = expectedResult;
            }
        }


    }
}