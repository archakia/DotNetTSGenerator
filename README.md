Dotnet 6.0 TS Generator
========================

This will build a d.ts file from a .net assembly.

Simply add both TS.CodeGenerator and TS.CodeGenerator.Console Nuget packages to your project, and the built-in MSBuild target will generate a .d.ts file at the root of your project folder after the project is built.
[https://www.nuget.org/packages/TS.CodeGenerator]
[https://www.nuget.org/packages/TS.CodeGenerator.Console]

TODO: Get the MSBuildTargets project working if an explicit MSBuild target in one "fat" NuGet package is desirable.
After adding both TS.CodeGenerator and TS.CodeGenerator.MSBuildTargets Nuget packages to your project, add this target to to your .csproj:
```
<Target Name="RunGenerateTypescriptTask" AfterTargets="Build">
      <GenerateTypescriptTask InputDLL="$(MSBuildThisFileDirectory)bin\debug\net6.0\aaa.dll" OutputDTS="..\out.d.ts"/>
</Target>
```

Be sure to modify the InputDLL and OutputDTS values to match your project's needs!
END TODO


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