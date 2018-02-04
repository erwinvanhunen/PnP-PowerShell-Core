---
external help file:
applicable: 
schema: 2.0.0
---
# Get-SubWeb

## SYNOPSIS
Returns the subwebs of the current web

## SYNTAX 

### 
```powershell
Get-SubWeb [-Recurse [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPSubWebs
```

This will return all sub webs for the current web

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPSubWebs -recurse
```

This will return all sub webs for the current web and its sub webs

## PARAMETERS

### -Recurse


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)