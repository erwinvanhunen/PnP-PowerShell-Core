---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Get-ClientSideComponent

## SYNOPSIS
Retrieve one or more Client-Side components from a page

## SYNTAX 

### 
```powershell
Get-ClientSideComponent [-Page <ClientSidePagePipeBind>]
                        [-InstanceId <GuidPipeBind>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPClientSideComponent -Page Home
```

Returns all controls defined on the given page.

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPClientSideComponent -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82
```

Returns a specific control defined on the given page.

## PARAMETERS

### -InstanceId


```yaml
Type: GuidPipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Page


```yaml
Type: ClientSidePagePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)