---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Unpublish-App

## SYNOPSIS
Unpublishes/retracts an available add-in from the app catalog

## SYNTAX 

### 
```powershell
Unpublish-App [-Identity <AppMetadataPipeBind>]
              [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Unpublish-PnPApp -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will retract, but not remove, the specified app from the app catalog

## PARAMETERS

### -Identity


```yaml
Type: AppMetadataPipeBind
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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)