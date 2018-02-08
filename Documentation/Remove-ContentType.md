---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-ContentType

## SYNOPSIS
Removes a content type from a web

## SYNTAX 

### 
```powershell
Remove-ContentType [-Identity <ContentTypePipeBind>]
                   [-Force [<SwitchParameter>]]
                   [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPContentType -Identity "Project Document"
```

This will remove a content type called "Project Document" from the current web

### ------------------EXAMPLE 2------------------
```powershell
PS:> Remove-PnPContentType -Identity "Project Document" -Force
```

This will remove a content type called "Project Document" from the current web with force

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
Type: ContentTypePipeBind
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