namespace DITestAssembly.GenericArgumentMapTest
{
    public class NestedGeneric<T, T1>: TwoGenParamBase<BaseType<T1>, FakeBaseType<BaseType<T>>> {}
}