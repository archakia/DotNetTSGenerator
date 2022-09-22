using Xunit;

namespace TS.CodeGenerator.tests
{
    enum test1
    {
        First,
        Second
    }

    public class TSEnumeration_Test
    {
        [Fact]
        public void TestSimpleProperty()
        {
            //arrange
            var c = typeof(test1);
            var e = new TSConstEnumeration(c);
            e.Initialize();

            //act
            var res = e.ToTSString();

            //assert
            Assert.True(!string.IsNullOrEmpty(res));
        }
    }
}
