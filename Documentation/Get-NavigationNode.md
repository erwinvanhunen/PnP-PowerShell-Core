---
external help file:
applicable: 
schema: 2.0.0
---
# Get-NavigationNode

## SYNOPSIS
Returns navigation nodes for a web

## SYNTAX 

### 
```powershell
Get-NavigationNode [-Location <NavigationType>]
                   [-Id <Int>]
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
PS:> Get-PnPNavigationNode -Id 2030
```

Returns the selected navigation node and retrieves any children

## PARAMETERS

### -Id


```yaml
Type: Int
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Location


```yaml
Type: NavigationType
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)