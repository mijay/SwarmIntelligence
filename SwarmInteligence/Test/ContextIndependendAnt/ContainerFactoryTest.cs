using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.DependecyInjection.Impl;
using CommonTest;
using DITestAssembly;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace Test.ContextIndependendAnt
{
    public class ContainerFactoryTest: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            var assemblyProvider = new Mock<IAssemblyProvider>(MockBehavior.Strict);
            assemblyProvider
                .Setup(x => x.GetAssemblies())
                .Returns(new[] { Assembly.Load("DITestAssembly") });
            var containerFactory = new ContainerFactory(assemblyProvider.Object);
            createdContainer = containerFactory.Create();
        }

        #endregion

        private static void AssertHasInstancesOf(IEnumerable<object> collection, params Type[] types)
        {
            Assert.That(collection.Count(), Is.EqualTo(types.Length));
            foreach(Type type in types)
                Assert.That(collection.Count(x => x.GetType() == type), Is.EqualTo(1));
        }

        private Container createdContainer;

        [Test]
        public void GetAllForBaseTypeForTypeWithInheritor_TypeAndInheritedTypeInstancesReturned()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestInheritanceAbstractBase>(),
                                 typeof(TestInheritanceBaseNonAbstract), typeof(TestInheritance));
        }

        [Test]
        public void GetAllForNonabstrGenericBaseWithCorrectTypeArg_TypeAndBaseTypeInstancesReturned()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestGenericInheritanceBase<int>>(),
                                 typeof(TestGenericInheritanceBase<int>), typeof(TestGenericInheritance));
        }

        [Test]
        public void GetAllForNonabstrGenericBaseWithIncorrectTypeArg_BaseTypeInstanceReturned()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestGenericInheritanceBase<double>>(),
                                 typeof(TestGenericInheritanceBase<double>));
        }

        [Test]
        public void GetAllForTypeWithInheritor_TypeAndInheritedTypeInstancesReturned()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestInheritanceBaseNonAbstract>(),
                                 typeof(TestInheritanceBaseNonAbstract), typeof(TestInheritance));
        }

        [Test]
        public void GetBaseClass_ConcreteClassInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestSimpleBase>(), Is.TypeOf<TestSimple>());
        }

        [Test]
        public void GetBaseWithManyImpl_Throws()
        {
            Assert.Throws<StructureMapException>(() => createdContainer.GetInstance<TestSimpleBadBase>());
        }

        [Test]
        public void GetClassInterface_ConcreteClassInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<ITestSimple>(), Is.TypeOf<TestSimple>());
        }

        [Test]
        public void GetCommonClass_ClassInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestSimple>(), Is.TypeOf<TestSimple>());
        }

        [Test]
        public void GetGenericBaseClassWithCorrectTypeArg_ClassInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestSimpleGenericBase<int>>(), Is.TypeOf<TestSimple>());
        }

        [Test]
        public void GetGenericBaseClassWithIncorrectTypeArg_Throws()
        {
            Assert.Throws<StructureMapException>(() => createdContainer.GetInstance<TestSimpleGenericBase<double>>());
        }

        [Test]
        public void GetNonabstrGenericBaseWithCorrectTypeArg_BaseTypeInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestGenericInheritanceBase<int>>(),
                        Is.TypeOf<TestGenericInheritanceBase<int>>());
        }

        [Test]
        public void GetNonabstrGenericBaseWithIncorrectTypeArg_BaseTypeInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestGenericInheritanceBase<double>>(),
                        Is.TypeOf<TestGenericInheritanceBase<double>>());
        }

        [Test]
        public void GetTypeWithInheritor_TypeInstanceReturned()
        {
            Assert.That(createdContainer.GetInstance<TestInheritanceBaseNonAbstract>(),
                        Is.TypeOf<TestInheritanceBaseNonAbstract>());
        }

        //todo: тесты на тип у которого больше ограничений на аргументы чем у предка (есть брат с другими ограничениями)
        //todo: полу-открытые типы
        //todo: наследование с частичным определением аргументов
    }
}