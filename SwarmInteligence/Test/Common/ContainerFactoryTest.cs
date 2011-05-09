using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.DependecyInjection.Impl;
using CommonTest;
using DITestAssembly;
using Moq;
using NUnit.Framework;
using StructureMap;
using StructureMap.TypeRules;

namespace Test.Common
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
            var containerFactory = new ContainerFactory(assemblyProvider.Object, new SmartConventions());
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
        public void GetAbstractGenericType_CorrectNonabstractGenericInheritorReturned()
        {
            Assert.That(createdContainer.GetInstance<TestWhere<int>>(),
                        Is.TypeOf<TestWhereStruct<int>>());
            Assert.That(createdContainer.GetInstance<TestWhere<ArrayList>>(),
                        Is.TypeOf<TestWhereEnumerable<ArrayList>>());
        }

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

        [Test]
        public void IsOpenGeneric()
        {
            Assert.True(typeof(IEnumerable<>).IsOpenGeneric());
            Assert.False(typeof(IDictionary<int, double>).IsOpenGeneric());

            var openListType = typeof(IList<>);
            var collectionTypeWithTypeparam =
                openListType.GetInterfaces().Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
            Assert.True(collectionTypeWithTypeparam.IsOpenGeneric());

            var closedListType = typeof(IList<int>);
            var collectionTypeWithSpecifiedParam =
                closedListType.GetInterfaces().Single(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
            Assert.False(collectionTypeWithSpecifiedParam.IsOpenGeneric());
        }

        [Test]
        public void GetTypeWithGenericWichSutisfyesAllInheritorsConstraints_ReturnTypeAndAllInheritors()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestPartGenBase<double, int>>(),
                typeof(TestPartGen<double>), typeof(TestPartGenBrother<int>), typeof(TestPartGenBase<double, int>));
        }

        [Test]
        public void GetTypeWithGenericWichSutisfyesSomeInheritorsConstraints_ReturnTypeAndThatInheritors()
        {
            AssertHasInstancesOf(createdContainer.GetAllInstances<TestPartGenBase<double, float>>(),
                typeof(TestPartGenBrother<float>), typeof(TestPartGenBase<double, float>));
        }

        [Test]
        public void GetNongenericInterfaceWithOnlyOneGenericImplementation_Throws()
        {
            Assert.Throws<StructureMapException>(() => createdContainer.GetInstance<ITestNoGenericParams>());
        }
    }
}