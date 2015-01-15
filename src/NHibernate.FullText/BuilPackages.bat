if not exist "./NuGet Packages" mkdir "./NuGet Packages"
del /Q "NuGet Packages\*"
call "./../.nuget/NuGet.exe" pack NHibernate.FullText.csproj -Build -IncludeReferencedProjects -Properties Configuration=Release -OutputDirectory "./NuGet Packages"
