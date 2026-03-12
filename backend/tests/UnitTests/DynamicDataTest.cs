namespace UnitTests
{
    public class TestDataProvider
    {
        public static IEnumerable<(int value, string name)> GetTestData()
        {
            yield return (1, "first");
            yield return (2, "second");
            yield return (3, "three");
        }
    }
    [TestClass]
    public class DynamicDataTest
    {
        public static IEnumerable<(int value, string Name)> GetTestData()
        {
            yield return (1, "first");
            yield return (2, "second");
        }

        public static IEnumerable<(int value, string Name)> TestDataProperty => [
            (1, "first"),
            (2, "second"),
        ];

        public static IEnumerable<(int valud, string name)> TestDataField = [
            (1, "first"),
            (2, "second")
        ];

        [TestMethod]
        [DynamicData(nameof(GetTestData))]
        public void TestWithMethod(int value, string name)
        {
            Assert.IsTrue(value > 0);
            // Assert.IsGreaterThan(value, 0);
        }

        [TestMethod]
        [DynamicData(nameof(TestDataProperty))]
        public void TestWithProperty(int value, string name)
        {
            Assert.IsTrue(value > 0);
            // Assert.IsGreaterThan(value, 0);
        }

        [TestMethod(UnfoldingStrategy = TestDataSourceUnfoldingStrategy.Unfold)] // nhiều trương hopje 
        [DynamicData(nameof(TestDataField))]
        public void TestWithField(int value, string name)
        {
            Assert.IsTrue(value > 0);
        }

        // lấy dữ liệu test từ một lớp khác
        [TestMethod(UnfoldingStrategy = TestDataSourceUnfoldingStrategy.Fold)] // gộp lại thành nột nút test duy nhất cho nhiều key
        [DynamicData(nameof(TestDataProvider.GetTestData), typeof(TestDataProvider))]
        public void TestWithOtherClass(int value, string name)
        {
            Assert.IsTrue(value > 0);
        }
    }
}