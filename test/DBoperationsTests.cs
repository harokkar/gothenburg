namespace GothenburgTest
{
	using NUnit.Framework;
	using db_operations;

	//foo variable should be declared as an instance of the test class
	/*[SetUp] public void SetUp() { foo = new Foo(); }
	[TearDown] public void TearDown() { foo.cleanUp(); }*/

	[TestFixture]
	public class DBOperationsTests
	{
		[Test]
		public void tags_getTest ()
		{
			DBOperations testdbop = new db_operations();
			int val = testdbop.foo(1);
			Assert.AreEqual(3, val);
		}
	}
}
