---
external help file:
applicable: 
schema: 2.0.0
---
# Set-HomePage

## SYNOPSIS
Sets the home page of the current web.

## SYNTAX 

### 
```powershell
Set-HomePage [-RootFolderRelativeUrl <String>]
             [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx
```

Sets the home page to the home.aspx file which resides in the SitePages library

## PARAMETERS

### -RootFolderRelativeUrl


```yaml
Type: String
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