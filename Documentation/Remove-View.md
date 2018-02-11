---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-View

## SYNOPSIS
Deletes a view from a list

## SYNTAX 

### 
```powershell
Remove-View [-Identity <ViewPipeBind>]
            [-List <ListPipeBind>]
            [-Force [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPView -List "Demo List" -Identity "All Items"
```

Removes the view with title "All Items" from the "Demo List" list.

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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)