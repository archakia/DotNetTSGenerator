Dotnet 5.0 TS Generator
========================

This will build a d.ts file from a .net assembly.

Now In Nuget Form:
[https://www.nuget.org/packages/TS.CodeGenerator]

Add this to to your csproj
```
<Target Name="GenerateTypescriptTask_1" AfterTargets="Build" Condition="'$(MSBuildRuntimeType)' == 'Core' And '$(MSBuildToolsVersion)' == 'Current'">
      <GenerateTypescriptTask InputDLL="$(MSBuildThisFileDirectory)bin\debug\net5.0\aaa.dll" OutputDTS="..\out.d.ts"/>
</Target>
```


Rationalle
----------
This is designed to run as a post build step on your *.Contracts.dll.  It will take PONO and convert them to Typescript Interfaces:


Example Output
--------------
```typescript
//MYNS.Model
interface IModel{
  //properties
	Name: string; //System.String
}



//MYNS.IModelService
interface IModelService{

  //methods
	GetModelFromModel(aModel:IModel/*Model*/,path:string/*String*/):JQueryPromise<IModel>;
}
```




OLD
===


example post build step ps1:
----------------------------

execute: powershell -file "$(SolutionDir)\MY.Contracts\build_dts.ps1"

example build_dts.ps1 (starts from MY.Contracts.dll and generates MY.Contracts.d.ts):
-------------------------------------------------------------------------------------

```powershell
$pwd = split-path -parent $MyInvocation.MyCommand.Definition

[Environment]::CurrentDirectory = $pwd

$dirDll = ([System.IO.Path]::Combine($pwd, "bin\debug"))

if (![System.IO.Directory]::Exists($dirDll)) {
    $dirDll = [System.IO.Path]::Combine($pwd, "..\..\bin\MY.contracts")

    if (![System.IO.Directory]::Exists($dirDll)) {
        throw "Cannot find $dirDll"
    }
}
$dirDll = [System.IO.Path]::Combine($dirDll, "MY.Contracts.dll")

Write-Host $dirDll

$outFileName = "MY.Contracts.d.ts"
$filePath = ([System.IO.Path]::Combine($pwd, $outFileName))



Write-Host "Creating d.ts file from assembly: "+$dirDll

[Reflection.Assembly]::LoadFile("TS.CodeGenerator.dll")
$assemblyReader = new-object -Typename TS.CodeGenerator.AssemblyReader -ArgumentList $dirDll

$outStream = $assemblyReader.GenerateTypingsStream()

If (Test-Path $filePath){
	Remove-Item $filePath
}

$fs = New-Object IO.FileStream $filePath ,'Append' 
$sw = New-Object IO.StreamWriter -ArgumentList  $fs
$sw.WriteLine('/// <reference path="../jquery/jquery.d.ts" />');
$sw.Flush();
$outStream.WriteTo($fs);
$fs.Flush();
$fs.Close();
$outStream.Close();

#copy it where you want it
$outPath = (get-item $pwd ).parent.FullName + "\MY.Web\Scripts\typings\MY\" + $outFileName ;
Write-Host "Copying generated dts: " + $outPath 
copy-item  -Force $filePath $outPath



exit $lastexitcode

```
