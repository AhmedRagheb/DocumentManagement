# Package the application into a zip file for deployment

Write-Host "['Packaging project']"

# The output folder which is eventually zipped
$outputFolder = Join-Path (Get-Item -Path ".\" -Verbose).FullName 'package'

# The input folder that is copied into the output folder before zipping
$inputFolder = Join-Path (Get-Item -Path ".\" -Verbose).FullName 'dist'

# The full path of the output zip file that is created
$outputFile = Join-Path (Get-Item -Path ".\" -Verbose).FullName 'package.zip'

Write-Host "Removing output folder $outputFolder"
Remove-Item $outputFolder -Force -Recurse -ErrorAction SilentlyContinue

Write-Host "Removing output file $outputFile"
Remove-Item $outputFile -Force -Recurse -ErrorAction SilentlyContinue

Write-Host "Creating output folder $outputFolder"
New-Item $outputFolder -ItemType Directory -Force | Out-Null

Write-Host "Copying input folder $inputFolder to output folder $outputFolder"
Copy-Item $inputFolder $outputFolder -Force -Recurse

Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

Write-Host "Creating zip file $outputFile from folder $outputFolder"

[System.IO.Compression.ZipFile]::CreateFromDirectory($outputFolder, $outputFile, $compressionLevel, $false)
