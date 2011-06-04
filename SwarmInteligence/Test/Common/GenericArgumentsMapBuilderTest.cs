using System;
using Common;
using Common.Collections;
using Common.DependecyInjection.Impl;
using CommonTest;
using DITestAssembly.GenericArgumentMapTest;
using NUnit.Framework;

namespace Test.Common
{
    public class GenericArgumentsMapBuilderTest: TestBase
    {
        private static void GetBaseTypeAsABaseOfInheritor(Type type, Type baseTypePattern,
                                                          out Type foundBaseType)
        {
            foreach(Type t in type.GetBaseTypesAndInterfaces())
                if((t.IsGenericType && t.GetGenericTypeDefinition() == baseTypePattern)
                   || (!t.IsGenericType && t == baseTypePattern)) {
                    foundBaseType = t;
                    return;
                }
            throw new InvalidOperationException();
        }

        public static void RunTestOn(Type inheritedType, Type baseType, params Type[] expectedResult)
        {
            Type foundBaseType;
            GetBaseTypeAsABaseOfInheritor(inheritedType,
                                          baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : baseType,
                                          out foundBaseType);
            Func<Type, Type[]> build = BaseToInheritorMapper.Build(inheritedType, foundBaseType);
            Assert.That(build, Is.Not.Null);

            Type[] result = build(baseType);
            if(expectedResult.IsNullOrEmpty())
                Assert.That(result, Is.Null);
            else
                CollectionAssert.AreEqual(result, expectedResult);
        }

        [Test]
        public void BaseTypeUseNotAllGenericParameters_BuildRetursNull()
        {
            Type baseType;
            GetBaseTypeAsABaseOfInheritor(typeof(GenericChild<>), typeof(NonGenericBase), out baseType);
            Func<Type, Type[]> build = BaseToInheritorMapper.Build(typeof(GenericChild<>), baseType);

            Assert.That(build, Is.Null);
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParametersInDifferentOrder_ExtractedOrderIsLikeInChild()
        {
            RunTestOn(typeof(InheritanceWithReoder<,>),
                      typeof(TwoGenParamBase<int, double>),
                      typeof(double), typeof(int));
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParameters_CanExtractFromBase()
        {
            RunTestOn(typeof(ChildType<>),
                      typeof(BaseType<int>),
                      typeof(int));
        }

        [Test]
        public void MapCreatedForOneType_UsedWithOther_NullReturned()
        {
            Type baseType;
            GetBaseTypeAsABaseOfInheritor(typeof(ChildType<>), typeof(BaseType<>), out baseType);
            Func<Type, Type[]> build = BaseToInheritorMapper.Build(typeof(ChildType<>), baseType);

            Assert.That(build(typeof(FakeBaseType<int>)), Is.Null);
        }

        [Test]
        public void
            ПриНаследованииСтрогоФиксируетсяОдинИзПараметровБазовогоТипа_ExtractИзБазовогоТипаСПарметромНеРавнымЗафиксироанному_ВозвращаемNull
            ()
        {
            RunTestOn(typeof(ChildWithOnlyOneParam<>),
                      typeof(TwoGenParamBase<double, string>));
        }

        [Test]
        public void
            ПриНаследованииСтрогоФиксируетсяОдинИзПараметровБазовогоТипа_ExtractИзБазовогоТипаСПарметромРавнымЗафиксироанному_ВсеРаботает
            ()
        {
            RunTestOn(typeof(ChildWithOnlyOneParam<>),
                      typeof(TwoGenParamBase<double, int>),
                      typeof(double));
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGeneric_ExtractИзБазовогоТипаСНемногоНеправильнымиПараметрами_ReturnNull
            ()
        {
            RunTestOn(typeof(NestedGeneric<,>),
                      typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<FakeBaseType<double>>>));
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGeneric_ExtractИзБазовогоТипаСПравильнымиПараметрами_ВсеРаботает
            ()
        {
            RunTestOn(typeof(NestedGeneric<,>),
                      typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<BaseType<double>>>),
                      typeof(double), typeof(int));
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGenericСМногократнымУпоминаниемОдногоПараметра_ExtractИзБазовогоТипаВКоторомВЭтихУпоминанияхИспользуютсяРазныеТипы_Null
            ()
        {
            RunTestOn(typeof(ComplexConstracint<>),
                      typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<double>>));
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGenericСМногократнымУпоминаниемОдногоПараметра_ExtractИзБазовогоТипаСПравильнымиПараметрами_ВсеРаботает
            ()
        {
            RunTestOn(typeof(ComplexConstracint<>),
                      typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<int>>),
                      typeof(int));
        }
    }
}