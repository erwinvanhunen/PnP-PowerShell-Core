---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-ContentTypeFromList

## SYNOPSIS
Removes a content type from a list

## SYNTAX 

### 
```powershell
Remove-ContentTypeFromList [-List <ListPipeBind>]
                           [-ContentType <ContentTypePipeBind>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPContentTypeFromList -List "Documents" -ContentType "Project Document"
```

This will remove a content type called "Project Document" from the "Documents" list

## PARAMETERS

### -ContentType


```yaml
Type: ContentTypePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -List


```yaml
Type: ListPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)