//
//  Include this file in your project if your project uses
//  ContractArgumentValidator or ContractAbbreviator methods
//
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Utils
{
    /// <summary>
    /// Enables factoring legacy if-then-throw into separate methods for reuse and full control over
    /// thrown exception and arguments
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractArgumentValidatorAttribute: Attribute {}

    /// <summary>
    /// Enables writing abbreviations for contracts that get copied to other methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractAbbreviatorAttribute: Attribute {}

    /// <summary>
    /// Allows setting contract and tool options at assembly, type, or method granularity.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    [Conditional("CONTRACTS_FULL")]
    internal sealed class ContractOptionAttribute: Attribute
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category",
            Justification = "Build-time only attribute")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting",
            Justification = "Build-time only attribute")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "toggle",
            Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, bool toggle) {}

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "category",
            Justification = "Build-time only attribute")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "setting",
            Justification = "Build-time only attribute")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value",
            Justification = "Build-time only attribute")]
        public ContractOptionAttribute(string category, string setting, string value) {}
    }
}