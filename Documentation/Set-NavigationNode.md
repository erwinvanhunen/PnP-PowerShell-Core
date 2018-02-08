---
external help file:
applicable: 
schema: 2.0.0
---
# Set-NavigationNode

## SYNOPSIS
Sets the home page of the current web.

## SYNTAX 

### 
```powershell
Set-NavigationNode [-Identity <NavigationNodePipeBind>]
                   [-NewId <Int>]
                   [-Title <String>]
                   [-Url <String>]
                   [-IsVisible <Boolean>]
                   [-IsExternal <Boolean>]
                   [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx
```

Sets the home page to the home.aspx file which resides in the SitePages library

## PARAMETERS

### -Identity


```yaml
Type: NavigationNodePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -IsExternal


```yaml
Type: Boolean
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -IsVisible


```yaml
Type: Boolean
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -NewId


```yaml
Type: Int
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Title


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Url


```yaml
Type: String
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