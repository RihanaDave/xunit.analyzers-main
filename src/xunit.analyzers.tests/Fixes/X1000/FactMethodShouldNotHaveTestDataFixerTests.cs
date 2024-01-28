using Xunit;
using Xunit.Analyzers.Fixes;
using Verify = CSharpVerifier<Xunit.Analyzers.FactMethodShouldNotHaveTestData>;

public class FactMethodShouldNotHaveTestDataFixerTests
{
	[Fact]
	public async void RemovesDataAttribute()
	{
		var before = @"
using Xunit;

public class TestClass {
    [Fact]
    [InlineData(1)]
    public void [|TestMethod|](int x) { }
}";

		var after = @"
using Xunit;

public class TestClass {
    [Fact]
    public void TestMethod(int x) { }
}";

		await Verify.VerifyCodeFix(before, after, FactMethodShouldNotHaveTestDataFixer.Key_RemoveDataAttributes);
	}
}
