# Cmdlet Documentation #
Below you can find a list of all the available cmdlets. Many commands provide built-in help and examples. Retrieve the detailed help with 

```powershell
Get-Help Connect-PnPOnline -Detailed
```

## Apps
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Add&#8209;App](Add-App.md)** |Add/uploads an available app to the app catalog|SharePoint Online
**[Get&#8209;App](Get-App.md)** |Returns the available apps from the app catalog|SharePoint Online
**[Install&#8209;App](Install-App.md)** |Installs an available app from the app catalog|SharePoint Online
**[Publish&#8209;App](Publish-App.md)** |Publishes/Deploys/Trusts an available app in the app catalog|SharePoint Online
**[Remove&#8209;App](Remove-App.md)** |Removes an app from the app catalog|SharePoint Online
**[Uninstall&#8209;App](Uninstall-App.md)** |Uninstalls an available add-in from the site|
**[Unpublish&#8209;App](Unpublish-App.md)** |Unpublishes/retracts an available add-in from the app catalog|SharePoint Online
**[Update&#8209;App](Update-App.md)** |Updates an available app from the app catalog|SharePoint Online
## Branding
Cmdlet|Description|Platforms
:-----|:----------|:--------
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
## Client-Side Pages
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;AvailableClientSideComponents](Get-AvailableClientSideComponents.md)** |Gets the available client side components on a particular page|SharePoint Online
**[Get&#8209;ClientSidePage](Get-ClientSidePage.md)** |Gets a Client-Side Page|SharePoint Online
## Content Types
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Add&#8209;ContentType](Add-ContentType.md)** |Adds a new content type|
**[Get&#8209;ContentType](Get-ContentType.md)** |Retrieves a content type|
**[Remove&#8209;ContentType](Remove-ContentType.md)** |Removes a content type from a web|
**[Remove&#8209;ContentTypeFromList](Remove-ContentTypeFromList.md)** |Removes a content type from a list|
**[Add&#8209;ContentTypeToList](Add-ContentTypeToList.md)** |Adds a new content type to a list|
**[Remove&#8209;FieldFromContentType](Remove-FieldFromContentType.md)** |Removes a site column from a content type|
**[Add&#8209;FieldToContentType](Add-FieldToContentType.md)** |Adds an existing site column to a content type|
## Event Receivers
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Add&#8209;EventReceiver](Add-EventReceiver.md)** |Adds a new remote event receiver|
**[Get&#8209;EventReceiver](Get-EventReceiver.md)** |Return registered eventreceivers|
**[Remove&#8209;EventReceiver](Remove-EventReceiver.md)** |Remove an eventreceiver|All
## Features
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Disable&#8209;Feature](Disable-Feature.md)** |Disables a feature|
**[Enable&#8209;Feature](Enable-Feature.md)** |Enables a feature|
**[Get&#8209;Feature](Get-Feature.md)** |Returns all activated or a specific activated feature|
## Fields
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Add&#8209;Field](Add-Field.md)** |Add a field|All
**[Get&#8209;Field](Get-Field.md)** |Returns a field from a list or site|
**[Remove&#8209;Field](Remove-Field.md)** |Removes a field from a list or a site|
**[Set&#8209;Field](Set-Field.md)** |Changes one or more properties of a field in a specific list or for the whole web|
**[Add&#8209;FieldFromXml](Add-FieldFromXml.md)** |Adds a field to a list or as a site column based upon a CAML/XML field definition|
## Files and Folders
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;Folder](Get-Folder.md)** |Return a folder object|
## Lists
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;List](Get-List.md)** |Returns a List object|
**[Get&#8209;ListItem](Get-ListItem.md)** |Retrieves list items|
**[Get&#8209;View](Get-View.md)** |Returns one or all views from a list|
## Tenant Administration
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;CustomAction](Get-CustomAction.md)** |Retrieves custom actions|
**[Connect&#8209;Online](Connect-Online.md)** |Connects to your tenant|
**[Send&#8209;RestRequest](Send-RestRequest.md)** |Executes a REST request|
**[New&#8209;Site](New-Site.md)** |Creates a new site collection|SharePoint Online
**[Get&#8209;StorageEntity](Get-StorageEntity.md)** |Retrieve Storage Entities / Farm Properties.|SharePoint Online
**[Get&#8209;TenantAppCatalogUrl](Get-TenantAppCatalogUrl.md)** |Retrieves the url of the tenant scoped app catalog.|SharePoint Online
**[Get&#8209;WebTemplate](Get-WebTemplate.md)** |Returns the available web templates.|SharePoint Online
## Web Parts
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;ClientSideComponent](Get-ClientSideComponent.md)** |Retrieve one or more Client-Side components from a page|SharePoint Online
## Webs
Cmdlet|Description|Platforms
:-----|:----------|:--------
**[Get&#8209;Context](Get-Context.md)** |Returns the current context|
**[Get&#8209;PropertyBag](Get-PropertyBag.md)** |Returns the property bag values.|
**[Set&#8209;PropertyBagValue](Set-PropertyBagValue.md)** |Sets a property bag value|
**[Get&#8209;RequestAccessEmail](Get-RequestAccessEmail.md)** |Returns the request access e-mail addresses|SharePoint Online
**[Get&#8209;SubWeb](Get-SubWeb.md)** |Returns the subwebs of the current web|
**[Get&#8209;Web](Get-Web.md)** |Returns the current web object|
**[New&#8209;Web](New-Web.md)** |Creates a new subweb under the current web|
