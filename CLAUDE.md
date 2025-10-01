# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SpeexDSPSharp is a cross-platform C# wrapper for the native SpeexDSP library, providing audio signal processing capabilities including echo cancellation, jitter buffering, and preprocessing (AGC, denoising, VAD). The library uses P/Invoke to call native SpeexDSP binaries.

**Documentation**: https://avionblock.github.io/SpeexDSPSharp/index.html

## Project Structure

### Core Components

- **SpeexDSPSharp.Core/**: Main library containing C# wrappers (targets netstandard2.1)
  - `NativeSpeexDSP.cs`: P/Invoke declarations for native speexdsp functions
  - `SpeexDSPPreprocessor.cs`: Audio preprocessing wrapper (AGC, denoise, VAD)
  - `SpeexDSPEchoCanceler.cs`: Echo cancellation wrapper
  - `SpeexDSPJitterBuffer.cs`: Network jitter buffer wrapper
  - `SafeHandlers/`: SafeHandle implementations for native resource management
  - `Structures/`: Native struct definitions for interop
  - `Enums.cs`: Control enumerations for ioctl-style configuration

- **SpeexDSPSharp.Natives/**: NuGet package containing precompiled native binaries
  - `runtimes/`: Platform-specific native libraries (Windows, Linux, Android for x86/x64/ARM variants)

- **SpeexDSPSharp/**: Meta-package that references both Core and Natives

- **Tester/**: Example console application using NAudio for audio I/O

- **docs/**: DocFX documentation site

## Building and Testing

### Build the Core Library
```bash
dotnet build SpeexDSPSharp.Core/SpeexDSPSharp.Core.csproj -c Release
```

### Run Test Application
```bash
dotnet run --project Tester/Tester.csproj
```

### Build NuGet Packages
```bash
# On Unix/Mac:
./PackAll.sh

# On Windows:
PackAll.bat
```

This builds the core library and packs three NuGet packages:
- SpeexDSPSharp.Core.nupkg
- SpeexDSPSharp.Natives.nupkg
- SpeexDSPSharp.nupkg (meta-package)

### Build Documentation
```bash
dotnet tool update -g docfx
docfx docs/docfx.json
```

Documentation is deployed via GitHub Actions workflow (`.github/workflows/deploy-site.yml`) on manual trigger.

## Architecture

### Native Interop Pattern

All three main DSP components (Preprocessor, EchoCanceler, JitterBuffer) follow the same pattern:

1. **Initialization**: Create native state handle via P/Invoke (e.g., `speex_preprocess_state_init`)
2. **SafeHandle Management**: Wrap native pointer in SafeHandle subclass for automatic cleanup
3. **Processing Methods**: Provide overloads accepting `Span<byte>`, `Span<short>`, `Span<float>`, and array variants
4. **Control Interface**: `Ctl<T>()` method for ioctl-style parameter control using enum values
5. **Disposal**: Implement IDisposable pattern to call native destroy function

### Platform-Specific Native Loading

- **iOS/macOS/Catalyst**: Uses `__Internal__` for statically linked binaries
- **Other platforms**: Loads `speexdsp` shared library via standard P/Invoke resolution

### Memory Safety

All native pointers are managed through SafeHandle implementations to prevent leaks. User-facing APIs use `Span<T>` for zero-copy processing with pinned memory.

## Common Development Tasks

### Adding New Native Functions

1. Add P/Invoke declaration to `NativeSpeexDSP.cs`
2. If creating new state type, add corresponding SafeHandle in `SafeHandlers/`
3. Create managed wrapper class following existing patterns (Preprocessor/EchoCanceler/JitterBuffer)
4. Add XML documentation comments matching native speexdsp docs

### Testing Audio Features

The Tester application demonstrates real-time audio processing using NAudio:
- 48kHz, mono audio with 20ms frame size
- Enables AGC, denoise, and VAD on the preprocessor
- Processes audio in `Recorder_DataAvailable` callback

### Working with Native Binaries

Native binaries are stored in `SpeexDSPSharp.Natives/runtimes/{RID}/native/` following NuGet conventions. To add new platforms:

1. Compile speexdsp for target platform
2. Place binary in appropriate RID folder (e.g., `linux-arm64/native/libspeexdsp.so`)
3. Update `.nuspec` to include new runtime

## Important Notes

- All processing methods expect frame sizes matching initialization parameters (typically 10-20ms of audio)
- Echo cancellation requires careful timing - see native speexdsp documentation
- The `Ctl()` method returns 0 on success, -1 on unknown request
- VAD (Voice Activity Detection) only returns meaningful values when enabled via `SPEEX_PREPROCESS_SET_VAD`
- Multi-channel echo cancellation uses separate init method (`speex_echo_state_init_mc`)
