namespace DITestAssembly
{
    public class TestPartGen<T>: TestPartGenBase<T, int> {}

    public class TestPartGenBrother<T>: TestPartGenBase<double, T> {}

    public class TestPartGenBase<T, T1> {}
}