namespace UnitTests;

public class Test
{
    public static int Add(int a, int b) => a + b;
}
public class TestReference
{
    public int Id { get; set; }
    public string Name { get; set; }
    public static string GetUserName(string name)
    {
        if (name == null)
            throw new ArgumentNullException();

        return name;
    }

    public static async Task<string> GetNameAsync(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        Task.Delay(1000).Wait();
        return name;
    }
}
[TestClass]
public sealed class Test1
{
    [TestMethod]
    // test kết quả bằng
    public void Test_TwoNumber_ReturnTrue()
    {
        Assert.AreEqual(5, Test.Add(2, 3));
    }
    [TestMethod]
    // test kết quả không bằng
    public void Test_TwoNumber_ReturnFalse()
    {
        Assert.AreNotEqual(5, Test.Add(3, 4));
    }


    [TestMethod]
    // test tham chiếu
    public void Test_ReferenceSame_ReturnTrue()
    {
        var expected = new TestReference()
        {
            Id = 1,
            Name = "dohan"
        };

        var actual = expected;

        Assert.AreSame(expected, actual);
    }
    [TestMethod]
    public void Test_ReferenceSame_ReturnFalse()
    {
        var expected = new TestReference()
        {
            Id = 1,
            Name = "dohan"
        };

        var actual = new TestReference();

        Assert.AreNotSame(expected, actual);
    }
    [TestMethod]
    public void Test_TypeObjectValue_ReturnOk()
    {
        var user = "han";
        Assert.IsInstanceOfType<int>(user);
    }

    [TestMethod]
    public void Test_ParameterIsNull_ReturnException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => TestReference.GetUserName(null));
    }
    [TestMethod]
    public async Task Test_ParameterIsNull_ReturnNull()
    {
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() => TestReference.GetNameAsync(null));
    }

    [TestMethod]
    // nếu collection khác thứ tự => fail
    public void Test_ListshouldBeEqual()
    {
        var expected = new List<int> { 1, 2, 3, 4 };
        var actual = expected;

        CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    // test cùng phần tử nhưng khác thứ tự
    public void Lists_ShouldBeEquivalent()
    {
        var list1 = new List<int> { 1, 2, 3 };
        var list2 = new List<int> { 3, 2, 1 };

        CollectionAssert.AreEquivalent(list1, list2);
    }
    [TestMethod]
    // kiểm tra xem có phần tử hay không
    public void List_ShouldContainValue()
    {
        var numbers = new List<int> { 1, 2, 3 };

        CollectionAssert.Contains(numbers, 2);
    }
}
