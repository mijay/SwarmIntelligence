using System;
using Common;
using Common.DependecyInjection.Impl;
using CommonTest;
using DITestAssembly.GenericArgumentMapTest;
using NUnit.Framework;

namespace Test.Common
{
    public class GenericArgumentsMapBuilderTest: TestBase
    {
        private static void CreateTAndGenericContext(Type type, Type baseTypePattern,
                                                     out Type foundBaseType,
                                                     out GenericArgumentsMapBuilder.GenericContext contextOfInherited)
        {
            contextOfInherited = new GenericArgumentsMapBuilder.GenericContext(type.GetGenericArguments());

            foreach(Type t in type.GetBaseTypesAndInterfaces())
                if((t.IsGenericType && t.GetGenericTypeDefinition() == baseTypePattern)
                   || (!t.IsGenericType && t == baseTypePattern)) {
                    foundBaseType = t;
                    return;
                }
            throw new InvalidOperationException();
        }

        [Test]
        public void BaseTypeUseNotAllGenericParameters_BuildRetursNull()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(GenericChild<>), typeof(NonGenericBase), out baseType, out context);

            Assert.That(GenericArgumentsMapBuilder.BuildFor(baseType, context), Is.Null);
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParametersInDifferentOrder_ExtractedOrderIsLikeInChild()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(InheritanceWithReoder<,>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            CollectionAssert.AreEqual(genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<int, double>)),
                                      new[] { typeof(double), typeof(int) });
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParameters_CanExtractFromBase()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ChildType<>), typeof(BaseType<>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            CollectionAssert.AreEqual(genericArgumentsMapBuilder.ExtractContext(typeof(BaseType<int>)), new[] { typeof(int) });
        }

        [Test]
        public void MapCreatedForOneType_UsedWithOther_ExceptionOccurs()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ChildType<>), typeof(BaseType<>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            Assert.Throws<ArgumentException>(() => genericArgumentsMapBuilder.ExtractContext(typeof(FakeBaseType<int>)));
        }

        [Test]
        public void
            ПриНаследованииСтрогоФиксируетсяОдинИзПараметровБазовогоТипа_ExtractИзБазовогоТипаСПарметромНеРавнымЗафиксироанному_ВозвращаемNull
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ChildWithOnlyOneParam<>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            Assert.That(genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<double, string>)),
                        Is.Null);
        }

        [Test]
        public void
            ПриНаследованииСтрогоФиксируетсяОдинИзПараметровБазовогоТипа_ExtractИзБазовогоТипаСПарметромРавнымЗафиксироанному_ВсеРаботает
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ChildWithOnlyOneParam<>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            CollectionAssert.AreEqual(genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<double, int>)),
                                      new[] { typeof(double) });
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGeneric_ExtractИзБазовогоТипаСПравильнымиПараметрами_ВсеРаботает
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(NestedGeneric<,>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            CollectionAssert.AreEqual(
                genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<BaseType<double>>>)),
                new[] { typeof(double), typeof(int) });
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGeneric_ExtractИзБазовогоТипаСНемногоНеправильнымиПараметрами_ReturnNull
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(NestedGeneric<,>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            Assert.That(
                genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<FakeBaseType<double>>>)),
                Is.Null);
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGenericСМногократнымУпоминаниемОдногоПараметра_ExtractИзБазовогоТипаСПравильнымиПараметрами_ВсеРаботает
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ComplexConstracint<>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            CollectionAssert.AreEqual(
                genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<int>>)),
                new[] {  typeof(int) });
        }

        [Test]
        public void
            ПриОпределенииНаследникаИспользуютсяВложенныеGenericСМногократнымУпоминаниемОдногоПараметра_ExtractИзБазовогоТипаВКоторомВЭтихУпоминанияхИспользуютсяРазныеТипы_Null
            ()
        {
            Type baseType;
            GenericArgumentsMapBuilder.GenericContext context;
            CreateTAndGenericContext(typeof(ComplexConstracint<>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMapBuilder genericArgumentsMapBuilder = GenericArgumentsMapBuilder.BuildFor(baseType, context);

            Assert.That(
                genericArgumentsMapBuilder.ExtractContext(typeof(TwoGenParamBase<BaseType<int>, FakeBaseType<double>>)),
                Is.Null);
        }
    }
}