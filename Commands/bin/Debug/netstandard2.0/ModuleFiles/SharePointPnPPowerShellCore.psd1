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
    CmdletsToExport = 'Add-App','Connect-Online','Disable-Feature','Enable-Feature','Get-App','Get-ContentType','Get-CustomAction','Get-EventReceiver','Get-Feature','Get-Field','Get-HomePage','Get-JavaScriptLink','Get-List','Get-MasterPage','Get-PropertyBag','Get-RequestAccessEmail','Get-SubWeb','Get-View','Get-Web','Send-RestRequest'
    VariablesToExport = '*'
AliasesToExport = 'Get-SubWebs'
    FormatsToProcess = '.\SharePointPnP.PowerShell.Core.Format.ps1xml'
    DefaultCommandPrefix = 'PnP'
    PrivateData = @{
        PSData = @{
            ProjectUri = 'https://aka.ms/sppnp'
            IconUri = 'https://raw.githubusercontent.com/SharePoint/PnP-PowerShell/master/Commands/Resources/pnp.ico'
        }
    }
}