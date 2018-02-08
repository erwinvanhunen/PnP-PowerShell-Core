---
external help file:
applicable: 
schema: 2.0.0
---
# Get-JavaScriptLink

## SYNOPSIS
Returns all or a specific custom action(s) with location type ScriptLink

## SYNTAX 

### 
```powershell
Get-JavaScriptLink [-Name <String>]
                   [-Scope <CustomActionScope>]
                   [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPJavaScriptLink
```

Returns all web scoped JavaScript links

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPJavaScriptLink -Scope All
```

Returns all web and site scoped JavaScript links

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPJavaScriptLink -Scope Web
```

Returns all Web scoped JavaScript links

### ------------------EXAMPLE 4------------------
```powershell
PS:> Get-PnPJavaScriptLink -Scope Site
```

Returns all Site scoped JavaScript links

### ------------------EXAMPLE 5------------------
```powershell
PS:> Get-PnPJavaScriptLink -Name Test
```

Returns the web scoped JavaScript link named Test

## PARAMETERS

### -Name


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Scope


```yaml
Type: CustomActionScope
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

### [SharePointPnP.PowerShell.Core.Model.UserCustomAction](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.usercustomaction.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)