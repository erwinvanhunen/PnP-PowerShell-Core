# PnP PowerShell overview

SharePoint Patterns and Practices (PnP) contains a library of PowerShell commands (PnP PowerShell) that allows you to perform complex provisioning and artifact management actions towards SharePoint. The commands use CSOM and can work against both SharePoint Online as SharePoint On-Premises.

![SharePoint Patterns and Practices](https://devofficecdn.azureedge.net/media/Default/PnP/sppnp.png)

_**Applies to:** SharePoint Online | SharePoint 2016 | SharePoint 2013_

## Installation #

There are 3 ways to install the cmdlets. We recommend, where possible, to install them from the [PowerShell Gallery](https://www.powershellgallery.com). Alternatively you can download the setup files or run a PowerShell script to download the PowerShellGet module and install the cmdlets subsequently.

### PowerShell Gallery ###
If you main OS is Windows 10, or if you have [PowerShellGet](https://github.com/powershell/powershellget) installed, you can run the following commands to install the PowerShell cmdlets:

|**SharePoint Version**|**Command to install**|
|------------------|------------------|
|SharePoint Online|```Install-Module SharePointPnPPowerShellOnline ```|
|SharePoint 2016|```Install-Module SharePointPnPPowerShell2016```|
|SharePoint 2013|```Install-Module SharePointPnPPowerShell2013```|

*Notice*: if you install the latest PowerShellGet from Github, you might receive an error message stating 
>PackageManagement\Install-Package : The version 'x.x.x.x' of the module 'SharePointPnPPowerShellOnline' being installed is not catalog signed.

In order to install the cmdlets when you get this error specify the -SkipPublisherCheck switch with the Install-Module cmdlet, e.g. ```Install-Module SharePointPnPPowerShellOnline -SkipPublisherCheck -AllowClobber```

### Setup files ##
You can download setup files from the [releases](https://github.com/officedev/pnp-powershell/releases) section of the PnP PowerShell repository. These files will up be updated on a monthly basis. Run the install and restart any open instances of PowerShell to use the cmdlets.

### Installation script ##
This is an alternative for installation on machines that have at least PowerShell v3 installed. You can find the version of PowerShell  by opening PowerShell and running ```$PSVersionTable.PSVersion```. The value for ```Major``` should be above 3.

To install the cmdlets you can run the below command which will install PowerShell Package Management and then install the PowerShell Modules from the PowerShell Gallery.

```powershell
Invoke-Expression (New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/OfficeDev/PnP-PowerShell/master/Samples/Modules.Install/Install-SharePointPnPPowerShell.ps1')
```

## Updating ##
Every month a new release will be made available of the PnP PowerShell Cmdlets. If you earlier installed the cmdlets using the setup file, simply download the [latest version](https://github.com/SharePoint/PnP-PowerShell/releases/latest) and run the setup. This will update your existing installation.

If you have installed the cmdlets using PowerShellGet with ```Install-Module``` from the PowerShell Gallery then you will be able to use the following command to install the latest updated version:

```powershell
Update-Module SharePointPnPPowerShell*
``` 

This will automatically load the module after starting PowerShell 3.0.

You can check the installed PnP-PowerShell versions with the following command:

```powershell
Get-Module SharePointPnPPowerShell* -ListAvailable | Select-Object Name,Version | Sort-Object Version -Descending
```

## Getting Started #

To use the library you first need to connect to your tenant:

```powershell
Connect-PnPOnline –Url https://yoursite.sharepoint.com –Credentials (Get-Credential)
```

To view all cmdlets, enter

```powershell
Get-Command -Module *PnP*
```

At the following links you will find a few videos on how to get started with the cmdlets:

* https://channel9.msdn.com/blogs/OfficeDevPnP/PnP-Web-Cast-Introduction-to-Office-365-PnP-PowerShell
* https://channel9.msdn.com/blogs/OfficeDevPnP/Introduction-to-PnP-PowerShell-Cmdlets
* https://channel9.msdn.com/blogs/OfficeDevPnP/PnP-Webcast-PnP-PowerShell-Getting-started-with-latest-updates

### Setting up credentials ##
See this [wiki page](https://github.com/OfficeDev/PnP-PowerShell/wiki/How-to-use-the-Windows-Credential-Manager-to-ease-authentication-with-PnP-PowerShell) for more information on how to use the Windows Credential Manager to setup credentials that you can use in unattended scripts

## Cmdlet overview


### Apps 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Add&#8209;App](Add-App.md)** |Add/uploads an available app to the app catalog|SharePoint Online
**[Get&#8209;App](Get-App.md)** |Returns the available apps from the app catalog|SharePoint Online
**[Install&#8209;App](Install-App.md)** |Installs an available app from the app catalog|SharePoint Online
**[Publish&#8209;App](Publish-App.md)** |Publishes/Deploys/Trusts an available app in the app catalog|SharePoint Online
**[Remove&#8209;App](Remove-App.md)** |Removes an app from the app catalog|SharePoint Online
**[Uninstall&#8209;App](Uninstall-App.md)** |Uninstalls an available add-in from the site|
**[Unpublish&#8209;App](Unpublish-App.md)** |Unpublishes/retracts an available add-in from the app catalog|SharePoint Online
**[Update&#8209;App](Update-App.md)** |Updates an available app from the app catalog|SharePoint Online


### Branding 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Add&#8209;CustomAction](Add-CustomAction.md)** |Adds a custom action|All
**[Remove&#8209;CustomAction](Remove-CustomAction.md)** |Removes a custom action|
**[Get&#8209;HomePage](Get-HomePage.md)** |Returns the URL to the page set as home page|
**[Set&#8209;HomePage](Set-HomePage.md)** |Sets the home page of the current web.|
**[Get&#8209;JavaScriptLink](Get-JavaScriptLink.md)** |Returns all or a specific custom action(s) with location type ScriptLink|
**[Get&#8209;MasterPage](Get-MasterPage.md)** |Returns the URLs of the default Master Page and the custom Master Page.|
**[Set&#8209;MasterPage](Set-MasterPage.md)** |Set the masterpage|All
**[Set&#8209;MinimalDownloadStrategy](Set-MinimalDownloadStrategy.md)** |Activates or deactivates the minimal downloading strategy.|All
**[Add&#8209;NavigationNode](Add-NavigationNode.md)** |Adds an item to a navigation element|All
**[Get&#8209;NavigationNode](Get-NavigationNode.md)** |Returns navigation nodes for a web|
**[Remove&#8209;NavigationNode](Remove-NavigationNode.md)** |Returns navigation nodes for a web|
**[Set&#8209;NavigationNode](Set-NavigationNode.md)** |Sets the home page of the current web.|


### Client-Side Pages 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Get&#8209;AvailableClientSideComponents](Get-AvailableClientSideComponents.md)** |Gets the available client side components on a particular page|SharePoint Online
**[Get&#8209;ClientSidePage](Get-ClientSidePage.md)** |Gets a Client-Side Page|SharePoint Online


### Content Types 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Add&#8209;ContentType](Add-ContentType.md)** |Adds a new content type|
**[Get&#8209;ContentType](Get-ContentType.md)** |Retrieves a content type|
**[Remove&#8209;ContentType](Remove-ContentType.md)** |Removes a content type from a web|
**[Remove&#8209;ContentTypeFromList](Remove-ContentTypeFromList.md)** |Removes a content type from a list|
**[Add&#8209;ContentTypeToList](Add-ContentTypeToList.md)** |Adds a new content type to a list|
**[Remove&#8209;FieldFromContentType](Remove-FieldFromContentType.md)** |Removes a site column from a content type|
**[Add&#8209;FieldToContentType](Add-FieldToContentType.md)** |Adds an existing site column to a content type|


### Event Receivers 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Add&#8209;EventReceiver](Add-EventReceiver.md)** |Adds a new remote event receiver|
**[Get&#8209;EventReceiver](Get-EventReceiver.md)** |Return registered eventreceivers|
**[Remove&#8209;EventReceiver](Remove-EventReceiver.md)** |Remove an eventreceiver|All


### Features 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Disable&#8209;Feature](Disable-Feature.md)** |Disables a feature|
**[Enable&#8209;Feature](Enable-Feature.md)** |Enables a feature|
**[Get&#8209;Feature](Get-Feature.md)** |Returns all activated or a specific activated feature|


### Fields 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Add&#8209;Field](Add-Field.md)** |Add a field|All
**[Get&#8209;Field](Get-Field.md)** |Returns a field from a list or site|


### Lists 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Get&#8209;List](Get-List.md)** |Returns a List object|
**[Get&#8209;View](Get-View.md)** |Returns one or all views from a list|


### Tenant Administration 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Get&#8209;CustomAction](Get-CustomAction.md)** |Retrieves custom actions|
**[Connect&#8209;Online](Connect-Online.md)** |Connects to your tenant|
**[Send&#8209;RestRequest](Send-RestRequest.md)** |Executes a REST request|
**[New&#8209;Site](New-Site.md)** |Creates a new site collection|SharePoint Online
**[Get&#8209;StorageEntity](Get-StorageEntity.md)** |Retrieve Storage Entities / Farm Properties.|SharePoint Online
**[Get&#8209;TenantAppCatalogUrl](Get-TenantAppCatalogUrl.md)** |Retrieves the url of the tenant scoped app catalog.|SharePoint Online
**[Get&#8209;WebTemplate](Get-WebTemplate.md)** |Returns the available web templates.|SharePoint Online


### Web Parts 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Get&#8209;ClientSideComponent](Get-ClientSideComponent.md)** |Retrieve one or more Client-Side components from a page|SharePoint Online


### Webs 
Cmdlet|Description|Platform
:-----|:----------|:-------
**[Get&#8209;Context](Get-Context.md)** |Returns the current context|
**[Get&#8209;PropertyBag](Get-PropertyBag.md)** |Returns the property bag values.|
**[Set&#8209;PropertyBagValue](Set-PropertyBagValue.md)** |Sets a property bag value|
**[Get&#8209;RequestAccessEmail](Get-RequestAccessEmail.md)** |Returns the request access e-mail addresses|SharePoint Online
**[Get&#8209;SubWeb](Get-SubWeb.md)** |Returns the subwebs of the current web|
**[Get&#8209;Web](Get-Web.md)** |Returns the current web object|
**[New&#8209;Web](New-Web.md)** |Creates a new subweb under the current web|


## Additional resources
<a name="bk_addresources"> </a>

-  [SharePoint PnP PowerShell on GitHub](https://github.com/SharePoint/PnP-PowerShell)
