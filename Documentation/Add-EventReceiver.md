---
external help file:
applicable: 
schema: 2.0.0
---
# Add-EventReceiver

## SYNOPSIS
Adds a new remote event receiver

## SYNTAX 

### 
```powershell
Add-EventReceiver [-List <ListPipeBind>]
                  [-Name <String>]
                  [-Url <String>]
                  [-EventReceiverType <EventReceiverType>]
                  [-Synchronization <EventReceiverSynchronization>]
                  [-SequenceNumber <Int>]
                  [-Force [<SwitchParameter>]]
```

## EXAMPLES

### ------------------EXAMPLE 1------------------
```powershell
PS:> Add-PnPEventReceiver -List "ProjectList" -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType ItemAdded -Synchronization Asynchronous
```

This will add a new remote event receiver that is executed after an item has been added to the ProjectList list

### ------------------EXAMPLE 2------------------
```powershell
PS:> Add-PnPEventReceiver -Name "TestEventReceiver" -Url https://yourserver.azurewebsites.net/eventreceiver.svc -EventReceiverType WebAdding -Synchronization Synchronous
```

This will add a new remote event receiver that is executed while a new subsite is being created

## PARAMETERS

### -EventReceiverType


```yaml
Type: EventReceiverType
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Force


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

### -Name


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -SequenceNumber


```yaml
Type: Int
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Synchronization


```yaml
Type: EventReceiverSynchronization
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

### -Url


```yaml
Type: String
Parameter Sets: 

Required: False
Position: 0
Accept pipeline input: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](http://aka.ms/sppnp)