using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace TS.CodeGenerator.tests
{
    interface MyClass<T>
    {
        T GetT(T input);
        IEnumerable<int> data { get; set; }
    }

    interface IMYGenericProperties<T>
    {
        IEnumerable<T> GenericList { get; set; } 
    }

    public class MyGenericPropertiesClass<T>
    {
        public IEnumerable<T> GenericList { get; set; }
    }

    public class GenericParameter_Test
    {
        [Fact]
        public void Fact1()
        {
            //arrange
            var c = typeof(MyClass<>);
            var gen = new TSGenerator(c.GetTypeInfo().Assembly);

            //act
            gen.AddInterface(c);
            var res = gen.ToTSString();

            //assert
            Assert.Contains("GetT?(input:T/*T*/):JQueryPromise<T>;", res);
        }

        [Fact]
        public void TestGenericProperty()
        {
            //arrange
            var c = typeof(IMYGenericProperties<>);
            var gen = new TSGenerator(c.GetTypeInfo().Assembly);

            //act
            gen.AddInterface(c);
            var res = gen.ToTSString();

            //assert
            Assert.Contains("GenericList: T[];", res);
        }

        [Fact]
        public void TestGenericPropertyClass()
        {
            //arrange
            var c = typeof(MyGenericPropertiesClass<>);
            var gen = new TSGenerator(c.GetTypeInfo().Assembly);
            
            //act
            gen.AddInterface(c);
            var res = gen.ToTSString();

            //assert
            Assert.Contains("GenericList: T[];", res);
        }

        [Fact]
        public void Fact2()
        {
            //arrange
            var c = typeof(MyClass<>);
            var gen = new TSGenerator(c.GetTypeInfo().Assembly);

            //act
            gen.AddInterface(c);
            var res = gen.ToTSString();

            //assert
            Assert.Contains("number[]", res);
        }
    }
}