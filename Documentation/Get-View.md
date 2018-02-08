---
external help file:
applicable: 
schema: 2.0.0
---
# Get-View

## SYNOPSIS
Returns one or all views from a list

## SYNTAX 

### 
```powershell
Get-View [-List <ListPipeBind>]
         [-Identity <ViewPipeBind>]
         [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Get-PnPView -List "Demo List"
```

Returns all views associated from the specified list

### ------------------EXAMPLE 2------------------
```powershell
Get-PnPView -List "Demo List" -Identity "Demo View"
```

Returns the view called "Demo View" from the specified list

### ------------------EXAMPLE 3------------------
```powershell
Get-PnPView -List "Demo List" -Identity "5275148a-6c6c-43d8-999a-d2186989a661"
```

Returns the view with the specified ID from the specified list

## PARAMETERS

### -Identity


```yaml
Type: ViewPipeBind
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

### -Context


```yaml
Type: SPOnlineContext
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## OUTPUTS

### [SharePointPnP.PowerShell.Core.Model.View](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.view.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)