using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharePointPnP.PowerShell.Core.ModuleFilesGenerator
{
    internal class ModuleManifestGenerator
    {
        private string _assemblyPath;
        private string _configurationName;
        private List<Model.CmdletInfo> _cmdlets;
        private Version _assemblyVersion;

        public ModuleManifestGenerator(List<Model.CmdletInfo> cmdlets, string assemblyPath, string configurationName, Version assemblyVersion)
        {
            _cmdlets = cmdlets;
            _assemblyPath = assemblyPath;
            _configurationName = configurationName;
            _assemblyVersion = assemblyVersion;
        }
        internal void Generate()
        {
            var spVersion = string.Empty;
            switch (_configurationName.ToLowerInvariant())
            {
                case "debug":
                case "release":
                    {
                        spVersion = "Online";
                        break;
                    }
                case "debug15":
                case "release15":
                    {
                        spVersion = "2013";
                        break;

                    }
                case "debug16":
                case "release16":
                    {
                        spVersion = "2016";
                        break;
                    }
            }
            // Generate PSM1 file
            var aliasesToExport = new List<string>();
            var psm1Path = $"{new FileInfo(_assemblyPath).Directory}\\ModuleFiles\\SharePointPnPPowerShellCoreAliases.psm1";
            var aliasBuilder = new StringBuilder();
            foreach (var cmdlet in _cmdlets.Where(c => c.Aliases.Any()))
            {
                foreach (var alias in cmdlet.Aliases)
                {
                    var aliasLine = $"Set-Alias -Name {alias} -Value {cmdlet.FullCommand}";
                    aliasBuilder.AppendLine(aliasLine);
                    aliasesToExport.Add(alias);
                }
            }

            if (aliasesToExport.Any())
            {
                File.WriteAllText(psm1Path, aliasBuilder.ToString());
            }

            // Create Module Manifest
            var psd1Path = $"{new FileInfo(_assemblyPath).Directory}\\ModuleFiles\\SharePointPnPPowerShellCore.psd1";
            var cmdletsToExportString = string.Join(",", _cmdlets.Select(c => "'" + c.FullCommand + "'"));
            string aliasesToExportString = null;
            if (aliasesToExport.Any())
            {
                aliasesToExportString = string.Join(",", aliasesToExport.Select(x => "'" + x + "'"));
            }
            WriteModuleManifest(psd1Path, spVersion, cmdletsToExportString, aliasesToExportString);
        }

        private void WriteModuleManifest(string path, string spVersion, string cmdletsToExport, string aliasesToExport)
        {
            var aliases = "";
            var nestedModules = "";
            if (aliasesToExport != null)
            {
                aliases = $"{Environment.NewLine}AliasesToExport = {aliasesToExport}";
                nestedModules = $"{Environment.NewLine}@('SharePointPnPPowerShellCoreAliases.psm1')";
            }
            var manifest = $@"@{{
    #ModuleToProcess = 'SharePointPnPPowerShellCoreAliases.psm1'
    #NestedModules = 'SharePointPnP.PowerShell.Core.dll'
    RootModule = 'SharePointPnP.PowerShell.Core.dll'
    ModuleVersion = '{_assemblyVersion}'
    Description = 'SharePoint Patterns and Practices PowerShell Cmdlets for SharePoint {spVersion}'
    GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
    Author = 'SharePoint Patterns and Practices'
    CompanyName = 'SharePoint Patterns and Practices'
    PowerShellVersion = '5.0'
    ProcessorArchitecture = 'None'
    FunctionsToExport = '*'
    CmdletsToExport = {cmdletsToExport}
    VariablesToExport = '*'{aliases}
    FormatsToProcess = '.\SharePointPnP.PowerShell.Core.Format.ps1xml'
    DefaultCommandPrefix = 'PnP'
    PrivateData = @{{
        PSData = @{{
            ProjectUri = 'https://aka.ms/sppnp'
            IconUri = 'https://raw.githubusercontent.com/SharePoint/PnP-PowerShell/master/Commands/Resources/pnp.ico'
        }}
    }}
}}";
            File.WriteAllText(path, manifest, Encoding.UTF8);
        }
    }
}
