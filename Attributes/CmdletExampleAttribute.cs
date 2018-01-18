using System;

namespace SharePointPnP.PowerShell.Core.Attributes
{
    /// <summary>
    /// Specify this attribute on a cmdlet class in order to provider examples in the documentation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,
                      AllowMultiple = true)]
    public sealed class CmdletExampleAttribute : Attribute
    {
       
        /// <summary>
        /// Example code. e.g. 'PS:> Get-MyCmdlet -Parameter1 value'
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Any introduction text
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// Any remarks, to be rendered underneath the example
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// The sort order of the example within the list of all examples.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
