---
external help file:
applicable: 
schema: 2.0.0
---
# Add-FieldToContentType

## SYNOPSIS
Adds an existing site column to a content type

## SYNTAX 

### 
```powershell
Add-FieldToContentType [-Field <FieldPipeBind>]
                       [-ContentType <ContentTypePipeBind>]
                       [-Required [<SwitchParameter>]]
                       [-Hidden [<SwitchParameter>]]
                       [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Add-PnPFieldToContentType -Field "Project_Name" -ContentType "Project Document"
```

This will add an existing site column with an internal name of "Project_Name" to a content type called "Project Document"

## PARAMETERS

### -ContentType


```yaml
Type: ContentTypePipeBind
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

### -Hidden


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Required


```yaml
Type: SwitchParameter
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