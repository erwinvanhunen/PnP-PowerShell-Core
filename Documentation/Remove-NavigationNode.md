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
                      [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPNavigationNode
```

Returns all navigation nodes in the quicklaunch navigation

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPNavigationNode -QuickLaunch
```

Returns all navigation nodes in the quicklaunch navigation

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPNavigationNode -TopNavigationBar
```

Returns all navigation nodes in the top navigation bar

### ------------------EXAMPLE 4------------------
```powershell
PS:> Get-PnPNavigationNode -Id 1025
```

Returns the navigation node with id 1025

## PARAMETERS

### -Identity


```yaml
Type: NavigationNodePipeBind
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