# Quick Start

The easiest way to get started with SpeexDSPSharp is to install the [SpeexDSPSharp.Core](https://www.nuget.org/packages/SpeexDSPSharp.Core) package into a .NET application.

## Step 1: Install SpeexDSPSharp.Core

You can install SpeexDSPSharp.Core via the nuget package manager through your IDE, e.g. VS22, Rider, etc...

Or you can install it via the dotnet CLI.
```csharp
dotnet add package SpeexDSPSharp.Core --version x.y.z
```

## Step 2: Include SpeexDSP DLL.

By default, SpeexDSPSharp.Core DOES NOT contain the speexdsp precompiled DLL's or binaries. This is so you can choose to provide your own DLL's or binary files instead of using SpeexDSPSharp's compiled binaries.

However if you want to use the precompiled binaries that SpeexDSPSharp provides, you can install the SpeexDSPSharp.Natives package onto your platform specific projects via the nuget package manager through your IDE, e.g. VS22, Rider, etc...

Or through the dotnet CLI.

```csharp
dotnet add package SpeexDSPSharp.Natives --version x.y.z
```

# Next Steps

📖 [Read the API for more information about the library](xref:SpeexDSPSharp.Core)

💬 [Use the discussions for help.](https://github.com/AvionBlock/SpeexDSPSharp/discussions)

📗 [Check out the examples.](../examples/Home.md)