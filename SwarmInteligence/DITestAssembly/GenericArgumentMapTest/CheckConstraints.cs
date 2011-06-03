namespace DITestAssembly.GenericArgumentMapTest
{
    public class ChildWithOnlyOneParam<T> : TwoGenParamBase<T, int> { }

    public class TwoGenParamBase<T, T1> {}
}