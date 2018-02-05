---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Publish-App

## SYNOPSIS
Publishes/Deploys/Trusts an available app in the app catalog

## SYNTAX 

### 
```powershell
Publish-App [-Identity <AppMetadataPipeBind>]
            [-SkipFeatureDeployment [<SwitchParameter>]]
            [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Publish-PnPApp
```

This will deploy/trust an app into the app catalog. Notice that the app needs to be available in the app catalog

## PARAMETERS

### -Identity


```yaml
Type: AppMetadataPipeBind
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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)