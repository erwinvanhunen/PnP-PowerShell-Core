﻿@{
    #ModuleToProcess = 'SharePointPnPPowerShellCoreAliases.psm1'
    #NestedModules = 'SharePointPnP.PowerShell.Core.dll'
    RootModule = 'SharePointPnP.PowerShell.Core.dll'
    ModuleVersion = '1.0.0.0'
    Description = 'SharePoint Patterns and Practices PowerShell Cmdlets for SharePoint Online'
    GUID = '0b0430ce-d799-4f3b-a565-f0dca1f31e17'
    Author = 'SharePoint Patterns and Practices'
    CompanyName = 'SharePoint Patterns and Practices'
    PowerShellVersion = '5.0'
    ProcessorArchitecture = 'None'
    FunctionsToExport = '*'
    CmdletsToExport = 'Add-App','Add-ContentType','Add-ContentTypeToList','Add-CustomAction','Add-EventReceiver','Add-Field','Add-FieldFromXml','Add-FieldToContentType','Add-NavigationNode','Add-View','Connect-Online','Disable-Feature','Enable-Feature','Get-App','Get-AvailableClientSideComponents','Get-ClientSideComponent','Get-ClientSidePage','Get-ContentType','Get-Context','Get-CustomAction','Get-EventReceiver','Get-Feature','Get-Field','Get-Folder','Get-HomePage','Get-JavaScriptLink','Get-List','Get-ListItem','Get-MasterPage','Get-NavigationNode','Get-PropertyBag','Get-RequestAccessEmail','Get-StorageEntity','Get-SubWeb','Get-TenantAppCatalogUrl','Get-View','Get-Web','Get-WebTemplate','Install-App','Invoke-RestRequest','New-Site','New-Web','Publish-App','Remove-App','Remove-ContentType','Remove-ContentTypeFromList','Remove-CustomAction','Remove-EventReceiver','Remove-Field','Remove-FieldFromContentType','Remove-List','Remove-NavigationNode','Remove-View','Set-Field','Set-HomePage','Set-List','Set-ListPermission','Set-MasterPage','Set-MinimalDownloadStrategy','Set-NavigationNode','Set-PropertyBagValue','Set-View','Uninstall-App','Unpublish-App','Update-App'
    VariablesToExport = '*'
AliasesToExport = 'Get-Subwebs','Get-WebTemplates'
    FormatsToProcess = '.\SharePointPnP.PowerShell.Core.Format.ps1xml'
    DefaultCommandPrefix = 'PnP'
    PrivateData = @{
        PSData = @{
            ProjectUri = 'https://aka.ms/sppnp'
            IconUri = 'https://raw.githubusercontent.com/SharePoint/PnP-PowerShell/master/Commands/Resources/pnp.ico'
        }
    }
}