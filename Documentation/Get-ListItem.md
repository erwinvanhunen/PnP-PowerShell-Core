---
external help file:
applicable: 
schema: 2.0.0
---
# Get-ListItem

## SYNOPSIS
Retrieves list items

## SYNTAX 

### 
```powershell
Get-ListItem [-List <ListPipeBind>]
             [-Id <Int>]
             [-UniqueId <GuidPipeBind>]
             [-Query <String>]
             [-Fields <String[]>]
             [-PageSize <Int>]
             [-ScriptBlock <ScriptBlock>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPListItem -List Tasks
```

Retrieves all list items from the Tasks list

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPListItem -List Tasks -Id 1
```

Retrieves the list item with ID 1 from from the Tasks list

### ------------------EXAMPLE 3------------------
```powershell
PS:> Get-PnPListItem -List Tasks -UniqueId bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3
```

Retrieves the list item with unique id bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3 from from the tasks lists

### ------------------EXAMPLE 4------------------
```powershell
PS:> (Get-PnPListItem -List Tasks -Fields "Title","GUID").FieldValues
```

Retrieves all list items, but only includes the values of the Title and GUID fields in the list item object

### ------------------EXAMPLE 5------------------
```powershell
PS:> Get-PnPListItem -List Tasks -Query "<View><Query><Where><Eq><FieldRef Name='GUID'/><Value Type='Guid'>bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3</Value></Eq></Where></Query></View>"
```

Retrieves all list items based on the CAML query specified

### ------------------EXAMPLE 6------------------
```powershell
PS:> Get-PnPListItem -List Tasks -PageSize 1000
```

Retrieves all list items from the Tasks list in pages of 1000 items

### ------------------EXAMPLE 7------------------
```powershell
PS:> Get-PnPListItem -List Tasks -PageSize 1000 -ScriptBlock { Param($items) $items.Context.ExecuteQuery() } | % { $_.BreakRoleInheritance($true, $true) }
```

Retrieves all list items from the Tasks list in pages of 1000 items and breaks permission inheritance on each item

## PARAMETERS

### -Fields


```yaml
Type: String[]
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Id


```yaml
Type: Int
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

### -PageSize


```yaml
Type: Int
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

### -ScriptBlock


```yaml
Type: ScriptBlock
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -UniqueId


```yaml
Type: GuidPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)