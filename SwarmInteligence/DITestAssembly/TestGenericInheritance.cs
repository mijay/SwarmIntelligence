namespace DITestAssembly
{
    public class TestGenericInheritance: TestGenericInheritanceBase<int> {}

    public class TestGenericInheritanceBase<T>
        where T: struct {}
}