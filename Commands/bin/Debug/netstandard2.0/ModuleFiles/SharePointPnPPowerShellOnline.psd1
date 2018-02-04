@{
    RootModule = 'SharePointPnP.PowerShell.Core.dll'
    ModuleVersion = '1.0.0.0'
    Description = 'SharePoint Patterns and Practices PowerShell Cmdlets for SharePoint Online'
    GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
    Author = 'SharePoint Patterns and Practices'
    CompanyName = 'SharePoint Patterns and Practices'
    PowerShellVersion = '6.0'
    ProcessorArchitecture = 'None'
    FunctionsToExport = '*'
    CmdletsToExport = 'Add-App','Connect-Online','Get-App','Get-ContentType','Get-CustomAction','Get-HomePage','Get-JavaScriptLink','Get-MasterPage'
    VariablesToExport = '*'
    FormatsToProcess = '.\SharePointPnP.PowerShell.Core.Format.ps1xml'
    DefaultCommandPrefix = 'PnP'
    PrivateData = @{
        PSData = @{
            ProjectUri = 'https://aka.ms/sppnp'
            IconUri = 'https://raw.githubusercontent.com/SharePoint/PnP-PowerShell/master/Commands/Resources/pnp.ico'
        }
    }
}