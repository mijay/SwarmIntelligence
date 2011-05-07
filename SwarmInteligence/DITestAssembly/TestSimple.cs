namespace DITestAssembly
{
    public class TestSimple: TestSimpleBase, ITestSimple {}

    public abstract class TestSimpleBase: TestSimpleGenericBase<int>, ITestSimple {}

    public abstract class TestSimpleGenericBase<T>: TestSimpleBadBase
        where T: struct {}

    public abstract class TestSimpleBadBase {}

    public class TestSimpleBadBaseImpl: TestSimpleBadBase {}

    public interface ITestSimple {}
}