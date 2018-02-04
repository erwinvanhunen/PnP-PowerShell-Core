param($ProjectDir, $ConfigurationName, $TargetDir, $TargetFileName, $SolutionDir)

if($ConfigurationName -like "Debug*")
{
	$DocumentsFolder = [environment]::getfolderpath("mydocuments");
	$DestinationFolder = "$DocumentsFolder\PowerShell\Modules\SharePointPnPPowerShellCore"
	
	# Module folder there?
	if(Test-Path $DestinationFolder)
	{
		# Yes, empty it
		Remove-Item "$DestinationFolder\*" -Recurse -Force > $null
	} else {
		# No, create it
		Write-Host "Creating target folder: $DestinationFolder"
		New-Item -Path $DestinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	if(Test-Path "$DestinationFolder\Tools")
	{
		# Yes, empty it
		Remove-Item "$DestinationFolder\Tools\*" -Force > $null
	} else {
		Write-Host "Creating Tools folder";
		New-Item -Path "$DestinationFolder\Tools" -ItemType Directory -Force >$null
	}

	Write-Host "Copying files from $TargetDir to $DestinationFolder"
	Try {
		Copy-Item "$TargetDir\*.*" -Destination "$DestinationFolder"
		Copy-Item "$TargetDir\Tools\*.*" -Destination "$DestinationFolder\Tools"
		Copy-Item "$TargetDir\ModuleFiles\*.*" -Destination "$DestinationFolder"
	}
	Catch
	{
		exit 1
	}
} elseif ($ConfigurationName -like "Release*")
{
    $DocumentsFolder = [environment]::getfolderpath("mydocuments");
	$DestinationFolder = "$documentsFolder\PowerShell\Modules\SharePointPnP.PowerShell.Core"

	# Module folder there?
	if(Test-Path $DestinationFolder)
	{
		# Yes, empty it
		Remove-Item $DestinationFolder\*
	} else {
		# No, create it
		Write-Host "Creating target folder: $DestinationFolder"
		New-Item -Path $DestinationFolder -ItemType Directory -Force >$null # Suppress output
	}

	Write-Host "Copying files from $TargetDir to $DestinationFolder"
	Try
	{
		Copy-Item "$TargetDir\publish\*.*" -Destination "$DestinationFolder"
	} 
	Catch
	{
		exit 1
	}
}

	