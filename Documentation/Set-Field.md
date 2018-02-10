---
external help file:
applicable: 
schema: 2.0.0
---
# Set-Field

## SYNOPSIS
Changes one or more properties of a field in a specific list or for the whole web

## SYNTAX 

### 
```powershell
Set-Field [-List <ListPipeBind>]
          [-Identity <FieldPipeBind>]
          [-Values <Hashtable>]
          [-UpdateExistingLists [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Set-PnPField -Identity AssignedTo -Values @{JSLink="customrendering.js";Group="My fields"}
```

Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to "My Fields". Lists that are already using the AssignedTo field will not be updated.

### ------------------EXAMPLE 2------------------
```powershell
PS:> Set-PnPField -Identity AssignedTo -Values @{JSLink="customrendering.js";Group="My fields"} -UpdateExistingLists
```

Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to "My Fields". Lists that are already using the AssignedTo field will also be updated.

### ------------------EXAMPLE 3------------------
```powershell
PS:> Set-PnPField -List "Tasks" -Identity "AssignedTo" -Values @{JSLink="customrendering.js"}
```

Updates the AssignedTo field on the Tasks list to use customrendering.js for the JSLink

## PARAMETERS

### -Identity


```yaml
Type: FieldPipeBind
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

### -UpdateExistingLists


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Values


```yaml
Type: Hashtable
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## OUTPUTS

### [SharePointPnP.PowerShell.Core.Model.Field](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.field.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)