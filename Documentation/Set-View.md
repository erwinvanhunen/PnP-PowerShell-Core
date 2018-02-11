---
external help file:
applicable: 
schema: 2.0.0
---
# Set-View

## SYNOPSIS
Change view properties

## SYNTAX 

### 
```powershell
Set-View [-List <ListPipeBind>]
         [-Identity <ViewPipeBind>]
         [-Title <String>]
         [-Query <String>]
         [-Fields <String[]>]
         [-ViewType <ViewType>]
         [-RowLimit <UInt32>]
         [-Personal [<SwitchParameter>]]
         [-SetAsDefault [<SwitchParameter>]]
         [-Paged [<SwitchParameter>]]
         [-Values <Hashtable>]
```

## DESCRIPTION
Sets one or more properties of an existing view.

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Set-PnPView -List "Tasks" -Identity "All Tasks" -Values @{JSLink="hierarchytaskslist.js|customrendering.js";Title="My view"}
```

Updates the "All Tasks" view on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink and changes the title of the view to "My view"

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPList -Identity "Tasks" | Get-PnPView | Set-PnPView -Values @{JSLink="hierarchytaskslist.js|customrendering.js"}
```

Updates all views on list "Tasks" to use hierarchytaskslist.js and customrendering.js for the JSLink

## PARAMETERS

### -Fields


```yaml
Type: String[]
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Identity


```yaml
Type: ViewPipeBind
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

### -Paged


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Personal


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Query


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -RowLimit


```yaml
Type: UInt32
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -SetAsDefault


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Title


```yaml
Type: String
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

### -ViewType


```yaml
Type: ViewType
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## OUTPUTS

### [SharePointPnP.PowerShell.Core.Model.Field](https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.view.aspx)

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)