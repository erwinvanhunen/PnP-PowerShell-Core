---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-Field

## SYNOPSIS
Removes a field from a list or a site

## SYNTAX 

### 
```powershell
Remove-Field [-Identity <FieldPipeBind>]
             [-List <ListPipeBind>]
             [-Force [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPField -Identity "Speakers"
```

Removes the speakers field from the site columns

### ------------------EXAMPLE 2------------------
```powershell
PS:> Remove-PnPField -List "Demo list" -Identity "Speakers"
```

Removes the speakers field from the list Demo list

## PARAMETERS

### -Force


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Identity


```yaml
Type: FieldPipeBind
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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)