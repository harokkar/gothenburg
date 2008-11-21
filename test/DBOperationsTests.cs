namespace GothenburgTest
{
	using NUnit.Framework;
	using Gothenburg;

	//foo variable should be declared as an instance of the test class
	/*[SetUp] public void SetUp() { foo = new Foo(); }
	[TearDown] public void TearDown() { foo.cleanUp(); }*/

	[TestFixture]
	public class DBOperationsTests
	{
		[Test]
		public void tags_getTest ()
		{
			DBOperations testdbop = new DBOperations( "FOO" );
			int val = testdbop.foo(1);
			Assert.AreEqual(3, val);
		}
	}
}
