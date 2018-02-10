---
external help file:
applicable: 
schema: 2.0.0
---
# Get-Folder

## SYNOPSIS
Return a folder object

## SYNTAX 

### 
```powershell
Get-Folder [-Url <String>]
```

## DESCRIPTION
Retrieves a folder if it exists. Use Ensure-PnPFolder to create the folder if it does not exist.

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPFolder -RelativeUrl "Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the current web

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPFolder -RelativeUrl "/sites/demo/Shared Documents"
```

Returns the folder called 'Shared Documents' which is located in the root of the current web

## PARAMETERS

### -Url


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)[Ensure-PnPFolder](https://github.com/OfficeDev/PnP-PowerShell/blob/master/Documentation/EnsureSPOFolder.md)