---
external help file:
applicable: 
schema: 2.0.0
---
# Set-ListPermission

## SYNOPSIS
Sets list permissions

## SYNTAX 

### 
```powershell
Set-ListPermission [-Identity <ListPipeBind>]
                   [-Group <GroupPipeBind>]
                   [-User <String>]
                   [-AddRole <String>]
                   [-RemoveRole <String>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -AddRole 'Contribute'
```

Adds the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'

### ------------------EXAMPLE 2------------------
```powershell
PS:> Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -RemoveRole 'Contribute'
```

Removes the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'

## PARAMETERS

### -AddRole


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Group


```yaml
Type: GroupPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Identity


```yaml
Type: ListPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -RemoveRole


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -User


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)