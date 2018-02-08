---
external help file:
applicable: 
schema: 2.0.0
---
# Get-PropertyBag

## SYNOPSIS
Returns the property bag values.

## SYNTAX 

### 
```powershell
Get-PropertyBag [-Key <String>]
                [-Folder <String>]
                [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPPropertyBag
```

This will return all web property bag values

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPPropertyBag -Key MyKey
```

This will return the value of the key MyKey from the web property bag

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPPropertyBag -Folder /MyFolder
```

This will return all property bag values for the folder MyFolder which is located in the root of the current web

### ------------------EXAMPLE 4------------------
```powershell
PS:> Get-PnPPropertyBag -Folder /MyFolder -Key vti_mykey
```

This will return the value of the key vti_mykey from the folder MyFolder which is located in the root of the current web

### ------------------EXAMPLE 5------------------
```powershell
PS:> Get-PnPPropertyBag -Folder / -Key vti_mykey
```

This will return the value of the key vti_mykey from the root folder of the current web

## PARAMETERS

### -Folder


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Key


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