This will build a d.ts file from a .net assembly.

Now In Nuget Form:
[https://www.nuget.org/packages/TS.CodeGenerator]

Add this to to your csproj
```
<Target Name="GenerateTypescriptTask_1" AfterTargets="Build" Condition="'$(MSBuildRuntimeType)' == 'Core' And '$(MSBuildToolsVersion)' == 'Current'">
      <GenerateTypescriptTask InputDLL="$(MSBuildThisFileDirectory)bin\debug\net5.0\aaa.dll" OutputDTS="..\out.d.ts"/>
</Target>
```

