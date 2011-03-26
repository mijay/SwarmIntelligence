using System;
using System.Collections.Generic;

namespace Utils.DependencyInjection
{
    public interface ITypeProvider {
        IEnumerable<Type> Types { get; }
        void BuildUp();
    }
}