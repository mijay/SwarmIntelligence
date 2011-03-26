using System;

namespace Common.DependencyInjection
{
    public interface IInstanceProvider {
        object GetInstance(Type type, InstanciatingBehaviour behaviour);
    }
}