# NativeOpus Examples

> [!NOTE]
> While directly calling native functions is typically not recommended, it may be beneficial where you may want to implement your own handlers or add missing wrapper features.

## Using a native function

```csharp
using SpeexDSPSharp.Core.SafeHandlers;
using SpeexDSPSharp.Core;

unsafe
{
	SpeexDSPEchoStateSafeHandler safeHandle = _handler = NativeSpeexDSP.speex_echo_state_init(960, 4800);

	//Successfully created an SpeexDSPEchoCanceler native object.

	//Free/Close Object
	safeHandle.Close();
}
```