dotnet build ./SpeexDSPSharp.Core/SpeexDSPSharp.Core.csproj -c Release

nuget pack ./SpeexDSPSharp.Core/SpeexDSPSharp.Core.nuspec -OutputDirectory local-nuget
nuget pack ./SpeexDSPSharp.Natives/SpeexDSPSharp.Natives.nuspec -OutputDirectory local-nuget
nuget pack ./SpeexDSPSharp/SpeexDSPSharp.nuspec -OutputDirectory local-nuget
