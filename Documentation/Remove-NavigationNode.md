---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-NavigationNode

## SYNOPSIS
Returns navigation nodes for a web

## SYNTAX 

### 
```powershell
Remove-NavigationNode [-Identity <NavigationNodePipeBind>]
                      [-Force [<SwitchParameter>]]
                      [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPNavigationNode -Identity 1032
```

Removes the navigation node with the specified id

### ------------------EXAMPLE 2------------------
```powershell
PS:> $nodes = Get-PnPNavigationNode -QuickLaunch
PS:>$nodes | Select-Object -First 1 | Remove-PnPNavigationNode -Force
```

Retrieves all navigation nodes from the Quick Launch navigation, then removes the first node in the list and it will not ask for a confirmation

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
Type: NavigationNodePipeBind
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