using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x02000DB4 RID: 3508
	internal interface IAsyncDebuggerClient
	{
		// Token: 0x060063D8 RID: 25560
		void SendStopEvent();

		// Token: 0x060063D9 RID: 25561
		void OnWatchesUpdated(WatchType watchType);

		// Token: 0x060063DA RID: 25562
		void OnSourceCodeChanged(int sourceID);

		// Token: 0x060063DB RID: 25563
		void OnExecutionEnded();

		// Token: 0x060063DC RID: 25564
		void OnException(ScriptRuntimeException ex);

		// Token: 0x060063DD RID: 25565
		void Unbind();
	}
}
