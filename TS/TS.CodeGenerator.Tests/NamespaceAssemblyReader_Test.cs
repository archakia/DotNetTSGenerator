using lib.test.a.b.Vehicles.TwoWheeled.MotorCycles;
using System.Reflection;
using Xunit;

namespace TS.CodeGenerator.tests
{
    public class NamespaceAssemblyReader_Test
    {
        [Fact]
        public void Fact1()
        {
            //arrange
            Settings.ConstEnumsEnabled = true;

            var nsg = new NamespaceAssemblyReader(typeof(Harley<>).GetTypeInfo().Assembly);
            //act
            var s = nsg.GenerateTypingsString();

            //assert
            Assert.NotNull(s);
        }

        [Fact]
        public void Test_IgnoreInterface()
        {
            //arrange
            Settings.ConstEnumsEnabled = true;
            Settings.IgnoreInterfaces.Add("IShouldNotShowUp");

            var nsg = new NamespaceAssemblyReader(typeof(Harley<>).GetTypeInfo().Assembly);
            //act
            var s = nsg.GenerateTypingsString();

            //assert
            Assert.True(!s.Contains("IShouldNotShowUp"));
        }
    }
}
