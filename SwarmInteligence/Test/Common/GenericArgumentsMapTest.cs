using System;
using System.Diagnostics.Contracts;
using Common;
using Common.DependecyInjection.Impl;
using CommonTest;
using DITestAssembly.GenericArgumentMapTest;
using NUnit.Framework;

namespace Test.Common
{
    public class GenericArgumentsMapTest: TestBase
    {
        private static void CreateTAndGenericContext(Type type, Type baseTypePattern,
                                                     out Type foundBaseType,
                                                     out GenericArgumentsMap.GenericContext contextOfInherited)
        {
            contextOfInherited = new GenericArgumentsMap.GenericContext(type.GetGenericArguments());

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
            GenericArgumentsMap.GenericContext context;
            CreateTAndGenericContext(typeof(GenericChild<>), typeof(NonGenericBase), out baseType, out context);

            Assert.That(GenericArgumentsMap.BuildFor(baseType, context), Is.Null);
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParametersInDifferentOrder_ExtractedOrderIsLikeInChild()
        {
            Type baseType;
            GenericArgumentsMap.GenericContext context;
            CreateTAndGenericContext(typeof(InheritanceWithReoder<,>), typeof(TwoGenParamBase<,>), out baseType, out context);
            GenericArgumentsMap genericArgumentsMap = GenericArgumentsMap.BuildFor(baseType, context);

            CollectionAssert.AreEqual(genericArgumentsMap.ExtractContext(typeof(TwoGenParamBase<int, double>)),
                                      new[] { typeof(double), typeof(int) });
        }

        [Test]
        public void ChildAndBaseTypeHasSameTypeParameters_CanExtractFromBase()
        {
            Type baseType;
            GenericArgumentsMap.GenericContext context;
            CreateTAndGenericContext(typeof(ChildType<>), typeof(BaseType<>), out baseType, out context);
            GenericArgumentsMap genericArgumentsMap = GenericArgumentsMap.BuildFor(baseType, context);

            CollectionAssert.AreEqual(genericArgumentsMap.ExtractContext(typeof(BaseType<int>)), new[] { typeof(int) });
        }

        [Test]
        public void MapCreatedForOneType_UsedWithOther_ExceptionOccurs()
        {
            Type baseType;
            GenericArgumentsMap.GenericContext context;
            CreateTAndGenericContext(typeof(ChildType<>), typeof(BaseType<>), out baseType, out context);
            GenericArgumentsMap genericArgumentsMap = GenericArgumentsMap.BuildFor(baseType, context);

            Assert.Throws<ArgumentException>(() => genericArgumentsMap.ExtractContext(typeof(FakeBaseType<int>)));
        }
    }
}