---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Get-StorageEntity

## SYNOPSIS
Retrieve Storage Entities / Farm Properties.

## SYNTAX 

### 
```powershell
Get-StorageEntity [-Key <String>]
                  [-AppCatalogUrl <String>]
                  [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPStorageEntity
```

Returns all site storage entities/farm properties

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPTenantSite -Key MyKey
```

Returns the storage entity/farm property with the given key.

## PARAMETERS

### -AppCatalogUrl


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