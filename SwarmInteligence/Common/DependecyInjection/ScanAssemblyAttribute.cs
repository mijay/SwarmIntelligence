using System;

namespace Common.DependecyInjection
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ScanAssemblyAttribute: Attribute {}
}