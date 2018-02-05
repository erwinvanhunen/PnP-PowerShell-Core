---
external help file:
applicable: 
schema: 2.0.0
---
# Disable-Feature

## SYNOPSIS
Disables a feature

## SYNTAX 

### 
```powershell
Disable-Feature [-Identity <GuidPipeBind>]
                [-Scope <FeatureScope>]
                [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"

### ------------------EXAMPLE 2------------------
```powershell
PS:> Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with the site scope.

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