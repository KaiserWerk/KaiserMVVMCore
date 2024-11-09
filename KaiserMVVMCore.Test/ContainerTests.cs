using System.ComponentModel;

namespace KaiserMVVMCore.Test
{
    public class ContainerTests
    {
        [Test]
        public void TestRegisterAndGetInstanceForInterface()
        {
            Container.Default.Register<ITestInterfaceB, TestClassB>();
            Container.Default.Register<ITestInterfaceA, TestClassA>();


            

            ITestInterfaceB instanceB = Container.Default.GetInstance<ITestInterfaceB>();
            TestClassB? implB = instanceB as TestClassB;
            Assert.That(implB, Is.Not.Null);

            ITestInterfaceA instanceA = Container.Default.GetInstance<ITestInterfaceA>();
            TestClassA? implA = instanceA as TestClassA;
            Assert.That(implA, Is.Not.Null);
        }
    }

    public interface ITestInterfaceA { }
    public interface ITestInterfaceB { }

    public class TestClassA : ITestInterfaceA 
    {
        public TestClassA(ITestInterfaceB testClassB)
        { }
    }
    public class TestClassB : ITestInterfaceB { }


}