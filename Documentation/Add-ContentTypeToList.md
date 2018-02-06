---
external help file:
applicable: 
schema: 2.0.0
---
# Add-ContentTypeToList

## SYNOPSIS
Adds a new content type to a list

## SYNTAX 

### 
```powershell
Add-ContentTypeToList [-List <ListPipeBind>]
                      [-ContentType <ContentTypePipeBind>]
                      [-DefaultContentType [<SwitchParameter>]]
                      [-Connection <SPOnlineConnection>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Add-PnPContentTypeToList -List "Documents" -ContentType "Project Document" -DefaultContentType
```

This will add an existing content type to a list and sets it as the default content type

## PARAMETERS

### -ContentType


```yaml
Type: ContentTypePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -DefaultContentType


```yaml
Type: SwitchParameter
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