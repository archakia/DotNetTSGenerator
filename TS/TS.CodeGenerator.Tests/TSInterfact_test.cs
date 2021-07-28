using System.Collections.Generic;
using Xunit;

namespace TS.CodeGenerator.tests
{
    public interface ITestGenerics<T>
    {
        T MyMethod(T parm1, T parm2);
    }

    public class TestGenerics<T>
    {
        public IEnumerable<T> Items { get; set; } 
    }

  

    public class TSInterfact_test
    {
        [Fact]
        public void Test_InterfaceGenerics()
        {
            //arrange
              var gen = new TSInterface(typeof (ITestGenerics<>), (t) =>
              {
                  if (t.IsGenericParameter)
                      return t.Name;
                  return Settings.StartingTypeMap[t];
              });
            gen.Initialize();
            //act
            var res = gen.ToTSString();

            //assert
            Assert.Contains("interface ITestGenerics<T>", res);
        }

        [Fact]
        public void Test_ClassGenerics()
        {
            //arrange
            var gen = new TSInterface(typeof(TestGenerics<>), (t) =>
            {
                if (t.Name.Contains("IEnumerable`1"))
                    return "T[]";
                return Settings.StartingTypeMap[t];
            });
            gen.Initialize();
            //act
            var res = gen.ToTSString();

            //assert
            Assert.Contains("interface ITestGenerics<T>", res);
            Assert.Contains("Items: T[];", res);
        }

       


    }
}
