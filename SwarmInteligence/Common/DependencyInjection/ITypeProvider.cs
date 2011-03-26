using System;
using System.Collections.Generic;

namespace Common.DependencyInjection
{
    public interface ITypeProvider {
        IEnumerable<Type> Types { get; }
        void BuildUp();
    }
}