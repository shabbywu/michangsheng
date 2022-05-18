using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x020011E2 RID: 4578
	internal interface IAsyncDebuggerClient
	{
		// Token: 0x06007022 RID: 28706
		void SendStopEvent();

		// Token: 0x06007023 RID: 28707
		void OnWatchesUpdated(WatchType watchType);

		// Token: 0x06007024 RID: 28708
		void OnSourceCodeChanged(int sourceID);

		// Token: 0x06007025 RID: 28709
		void OnExecutionEnded();

		// Token: 0x06007026 RID: 28710
		void OnException(ScriptRuntimeException ex);

		// Token: 0x06007027 RID: 28711
		void Unbind();
	}
}
