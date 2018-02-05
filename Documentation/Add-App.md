---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Add-App

## SYNOPSIS
Add/uploads an available app to the app catalog

## SYNTAX 

### 
```powershell
Add-App [-Path <String>]
        [-Publish [<SwitchParameter>]]
        [-SkipFeatureDeployment [<SwitchParameter>]]
        [-Overwrite [<SwitchParameter>]]
        [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Add-PnPApp -Path ./myapp.sppkg
```

This will upload the specified app package to the app catalog

### ------------------EXAMPLE 2------------------
```powershell
PS:> Add-PnPApp -Path ./myapp.sppkg -Publish
```

This will upload the specified app package to the app catalog and deploy/trust it at the same time.

## PARAMETERS

### -Overwrite


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Path


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Publish


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -SkipFeatureDeployment


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Connection


```yaml
Type: SPOnlineConnection
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## OUTPUTS

### SharePointPnP.PowerShell.Core.Model.AppMetadata

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)