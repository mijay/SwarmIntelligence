using System;
using Common;
using Common.Collections;
using Common.DependecyInjection.Impl.GenericArgumentExtraction;
using CommonTest;
using DITestAssembly.GenericArgumentMapTest;
using NUnit.Framework;

namespace Test.Common
{
	public class ExtractorTest: TestBase
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

			var extractionContext = new ExtractionContext(inheritedType);
			Extractor extractor = Extractor.Build(foundBaseType, extractionContext);
			Assert.That(extractor, Is.Not.Null);
			Assert.That(extractionContext.IsResolved());

			GenericArgumentsMap argumentsMap = extractionContext.GetInitialMap();
			if(expectedResult.IsNullOrEmpty())
				Assert.Throws<Extractor.CannotExtractException>(() => extractor.Extract(baseType, argumentsMap));
			else {
				extractor.Extract(baseType, argumentsMap);
				Type[] result = argumentsMap.ToArray();
				CollectionAssert.AreEqual(result, expectedResult);
			}
		}

		[Test]
		public void BaseTypeUseNotAllGenericParameters_BuildDoesNotResolveContext()
		{
			Type inheritedType = typeof(GenericChild<>);
			Type baseType = typeof(NonGenericBase);
			Type foundBaseType;
			GetBaseTypeAsABaseOfInheritor(inheritedType, baseType, out foundBaseType);

			var extractionContext = new ExtractionContext(inheritedType);
			Extractor extractor = Extractor.Build(foundBaseType, extractionContext);
			Assert.That(extractor, Is.Not.Null);
			Assert.That(extractionContext.IsResolved(), Is.False);
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
		public void MapCreatedForOneType_UsedWithOther_ExceptionOccures()
		{
			Type inheritedType = typeof(ChildType<>);
			Type baseType = typeof(BaseType<>);
			Type foundBaseType;
			GetBaseTypeAsABaseOfInheritor(inheritedType, baseType, out foundBaseType);

			var extractionContext = new ExtractionContext(inheritedType);
			Extractor extractor = Extractor.Build(foundBaseType, extractionContext);
			Assert.That(extractor, Is.Not.Null);
			Assert.That(extractionContext.IsResolved());

			Assert.Throws<Extractor.CannotExtractException>(
				() => extractor.Extract(typeof(FakeBaseType<int>), extractionContext.GetInitialMap()));
		}

		[Test]
		public void
			ПриНаследованииСтрогоФиксируетсяОдинИзПараметровБазовогоТипа_ExtractИзБазовогоТипаСПарметромНеРавнымЗафиксироанному_ExceptionOccurs
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
			ПриОпределенииНаследникаИспользуютсяВложенныеGeneric_ExtractИзБазовогоТипаСНемногоНеправильнымиПараметрами_ExceptionOccurs
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
			ПриОпределенииНаследникаИспользуютсяВложенныеGenericСМногократнымУпоминаниемОдногоПараметра_ExtractИзБазовогоТипаВКоторомВЭтихУпоминанияхИспользуютсяРазныеТипы_ExceptionOccurs
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