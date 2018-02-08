---
external help file:
applicable: 
schema: 2.0.0
---
# Uninstall-App

## SYNOPSIS
Uninstalls an available add-in from the site

## SYNTAX 

### 
```powershell
Uninstall-App [-Identity <AppMetadataPipeBind>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Uninstall-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will uninstall the specified app from the current site.

## PARAMETERS

### -Identity


```yaml
Type: AppMetadataPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)