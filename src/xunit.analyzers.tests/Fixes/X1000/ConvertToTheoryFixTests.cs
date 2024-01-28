using Xunit;
using Xunit.Analyzers.Fixes;
using Verify_X1001 = CSharpVerifier<Xunit.Analyzers.FactMethodMustNotHaveParameters>;
using Verify_X1005 = CSharpVerifier<Xunit.Analyzers.FactMethodShouldNotHaveTestData>;

public class ConvertToTheoryFixTests
{
	[Fact]
	public async void From_X1001()
	{
		var before = @"
using Xunit;

public class TestClass {
    [Fact]
    public void [|TestMethod|](int a) { }
}";

		var after = @"
using Xunit;

public class TestClass {
    [Theory]
    public void TestMethod(int a) { }
}";

		await Verify_X1001.VerifyCodeFix(before, after, ConvertToTheoryFix.Key_ConvertToTheory);
	}

	[Fact]
	public async void From_X1005()
	{
		var before = @"
using Xunit;

public class TestClass {
    [Fact]
    [InlineData(42)]
    public void [|TestMethod|]() { }
}";

		var after = @"
using Xunit;

public class TestClass {
    [Theory]
    [InlineData(42)]
    public void TestMethod() { }
}";

		await Verify_X1005.VerifyCodeFix(before, after, ConvertToTheoryFix.Key_ConvertToTheory);
	}
}
