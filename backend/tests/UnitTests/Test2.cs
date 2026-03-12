namespace UnitTests
{
    [TestClass]
    public class Test2
    {
        // inject test context;
        public TestContext TestContext {get; set;}
        public static List<string> Products;

        [ClassInitialize]
        // chạy 1 lần cho cả class test, chỉ tạo list 1 phần
        public static void Setup(TestContext context)
        {
            Products = new List<string>()
            {
                "Iphone",
                "Samsing",
                "Mackbook"
            };
            context.WriteLine("Initial class");
        }
        [TestMethod] 
        // log thông tin test
        public void Add_ShouldReturnCorrectResult()
        {
            int a = 2;
            int b = 3; 

            int result = a + b;
            TestContext.WriteLine($"Running test: {TestContext.TestName}"); // gi log, và lấy ra case test hiện tại
            TestContext.WriteLine($"a = {a}, b = {b}, result = {result}");

            Assert.AreEqual(5, result);
        }
        [TestMethod]
        public void ShouldContainIphone()
        {
            CollectionAssert.Contains(Products, "Iphone");
        }
        [TestMethod]
        public void TestClassResultFile()
        {
            string logFilePath = Path.Combine(TestContext.TestRunDirectory, "Testlog.txt");
            File.WriteAllText(logFilePath, "this is s sample log entry for the test");
            TestContext.WriteLine($"Thư mục đang test{TestContext.TestRunDirectory}, thư mục lưu kết quả {TestContext.ResultsDirectory}");
            TestContext.AddResultFile(logFilePath);

            Assert.IsTrue(File.Exists(logFilePath), "The log file was not created");
            Assert.IsTrue(new FileInfo(logFilePath).Length > 0, "The log file is emtyp");


        }
    }
}