---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Get-AvailableClientSideComponents

## SYNOPSIS
Gets the available client side components on a particular page

## SYNTAX 

### 
```powershell
Get-AvailableClientSideComponents [-Component <ClientSideComponentPipeBind>]
                                  [-Context <SPOnlineContext>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPAvailableClientSideComponents
```

Gets the list of available client side components on the page 'MyPage.aspx'

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPAvailableClientSideComponents -ComponentName "HelloWorld"
```

Gets the client side component 'HelloWorld'

## PARAMETERS

### -Component


```yaml
Type: ClientSideComponentPipeBind
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