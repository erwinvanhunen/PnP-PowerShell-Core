---
external help file:
applicable: 
schema: 2.0.0
---
# Set-List

## SYNOPSIS
Updates list settings

## SYNTAX 

### 
```powershell
Set-List [-Identity <ListPipeBind>]
         [-EnableContentTypes <Boolean>]
         [-BreakRoleInheritance [<SwitchParameter>]]
         [-CopyRoleAssignments [<SwitchParameter>]]
         [-ClearSubscopes [<SwitchParameter>]]
         [-Title <String>]
         [-Hidden <Boolean>]
         [-EnableVersioning <Boolean>]
         [-EnableMinorVersions <Boolean>]
         [-MajorVersions <UInt32>]
         [-MinorVersions <UInt32>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
Set-PnPList -Identity "Demo List" -EnableContentTypes $true
```

Switches the Enable Content Type switch on the list

### ------------------EXAMPLE 2------------------
```powershell
Set-PnPList -Identity "Demo List" -Hidden $true
```

Hides the list from the SharePoint UI.

### ------------------EXAMPLE 3------------------
```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true
```

Turns on major versions on a list

### ------------------EXAMPLE 4------------------
```powershell
Set-PnPList -Identity "Demo List" -EnableVersioning $true -MajorVersions 20
```

Turns on major versions on a list and sets the maximum number of Major Versions to keep to 20.

### ------------------EXAMPLE 5------------------
```powershell
Set-PnPList -Identity "Demo Library" -EnableVersioning $true -EnableMinorVersions $true -MajorVersions 20 -MinorVersions 5
```

Turns on major versions on a document library and sets the maximum number of Major versions to keep to 20 and sets the maximum of Minor versions to 5.

## PARAMETERS

### -BreakRoleInheritance


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -ClearSubscopes


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -CopyRoleAssignments


```yaml
Type: SwitchParameter
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -EnableContentTypes


```yaml
Type: Boolean
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -EnableMinorVersions


```yaml
Type: Boolean
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -EnableVersioning


```yaml
Type: Boolean
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Hidden


```yaml
Type: Boolean
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

### -MajorVersions


```yaml
Type: UInt32
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -MinorVersions


```yaml
Type: UInt32
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

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)