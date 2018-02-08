---
external help file:
applicable: 
schema: 2.0.0
---
# Remove-FieldFromContentType

## SYNOPSIS
Removes a site column from a content type

## SYNTAX 

### 
```powershell
Remove-FieldFromContentType [-Field <FieldPipeBind>]
                            [-ContentType <ContentTypePipeBind>]
                            [-DoNotUpdateChildren [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Remove-PnPFieldFromContentType -Field "Project_Name" -ContentType "Project Document"
```

This will remove the site column with an internal name of "Project_Name" from a content type called "Project Document"

### ------------------EXAMPLE 2------------------
```powershell
PS:> Remove-PnPFieldFromContentType -Field "Project_Name" -ContentType "Project Document" -DoNotUpdateChildren
```

This will remove the site column with an internal name of "Project_Name" from a content type called "Project Document". It will not update content types that inherit from the "Project Document" content type.

## PARAMETERS

### -ContentType


```yaml
Type: ContentTypePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -DoNotUpdateChildren


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Field


```yaml
Type: FieldPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)