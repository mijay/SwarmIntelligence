using System.Collections;

namespace DITestAssembly
{
    public abstract class TestWhere<T> {}

    public class TestWhereStruct<T>: TestWhere<T>
        where T: struct {}

    public class TestWhereEnumerable<T>: TestWhere<T>
        where T: IEnumerable {}
}