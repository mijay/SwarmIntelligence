using System;

namespace Utils.DependencyInjection
{
    public interface IInstanceProvider {
        object GetInstance(Type type, InstanciatingBehaviour behaviour);
    }
}