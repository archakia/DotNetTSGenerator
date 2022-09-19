Dotnet 6.0 TS Generator
========================

This will build a d.ts file from a .net assembly.

Now In Nuget Form:
[https://www.nuget.org/packages/TS.CodeGenerator]

After adding both TS.CodeGenerator and TS.CodeGenerator.MSBuildTargets Nuget packages to your project, add this target to to your .csproj:
```
<Target Name="GenerateTypescriptTask_1" AfterTargets="Build" Condition="'$(MSBuildRuntimeType)' == 'Core' And '$(MSBuildToolsVersion)' == 'Current'">
      <GenerateTypescriptTask InputDLL="$(MSBuildThisFileDirectory)bin\debug\net6.0\aaa.dll" OutputDTS="..\out.d.ts"/>
</Target>
```

Be sure to modify the InputDLL and OutputDTS values to match your project's needs!



Rationale
----------
This is designed to run as a post build step on your *.Contracts.dll.  It will take PONOs and convert them to Typescript interfaces.



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