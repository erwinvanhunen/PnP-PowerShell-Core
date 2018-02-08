---
external help file:
applicable: 
schema: 2.0.0
---
# Get-Feature

## SYNOPSIS
Returns all activated or a specific activated feature

## SYNTAX 

### 
```powershell
Get-Feature [-Identity <GuidPipeBind>]
            [-Scope <FeatureScope>]
            [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPFeature
```

This will return all activated web scoped features

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPFeature -Scope Site
```

This will return all activated site scoped features

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will return a specific activated web scoped feature

### ------------------EXAMPLE 4------------------
```powershell
PS:> Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22 -Scope Site
```

This will return a specific activated site scoped feature

## PARAMETERS

### -Identity


```yaml
Type: GuidPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Scope


```yaml
Type: FeatureScope
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

### [List<SharePointPnP.PowerShell.Core.Model.Feature>](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.feature.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)