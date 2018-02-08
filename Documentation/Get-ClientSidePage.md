---
external help file:
applicable: SharePoint Online
schema: 2.0.0
---
# Get-ClientSidePage

## SYNOPSIS
Gets a Client-Side Page

## SYNTAX 

### 
```powershell
Get-ClientSidePage [-Identity <ClientSidePagePipeBind>]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Get-PnPClientSidePage -Identity "MyPage.aspx"
```

Gets the Modern Page (Client-Side) named 'MyPage.aspx' in the current SharePoint site

### ------------------EXAMPLE 2------------------
```powershell
PS:> Get-PnPClientSidePage "MyPage"
```

Gets the Modern Page (Client-Side) named 'MyPage.aspx' in the current SharePoint site

## PARAMETERS

### -Identity


```yaml
Type: ClientSidePagePipeBind
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)