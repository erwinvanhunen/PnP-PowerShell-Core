---
external help file:
applicable: 
schema: 2.0.0
---
# Get-ContentType

## SYNOPSIS
Retrieves a content type

## SYNTAX 

### 
```powershell
Get-ContentType [-Identity <ContentTypePipeBind>]
                [-List <ListPipeBind>]
                [-InSiteHierarchy [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPContentType 
```

This will get a listing of all available content types within the current web

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPContentType -InSiteHierarchy
```

This will get a listing of all available content types within the site collection

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPContentType -Identity "Project Document"
```

This will get the content type with the name "Project Document" within the current context

### ------------------EXAMPLE 4------------------
```powershell
PS:> Get-PnPContentType -List "Documents"
```

This will get a listing of all available content types within the list "Documents"

## PARAMETERS

### -Identity


```yaml
Type: ContentTypePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -InSiteHierarchy


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -List


```yaml
Type: ListPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## OUTPUTS

### [SharePointPnP.PowerShell.Core.Model.ContentType](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.contenttype.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)