using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D67 RID: 3431
	public interface IDebugger
	{
		// Token: 0x060060F3 RID: 24819
		DebuggerCaps GetDebuggerCaps();

		// Token: 0x060060F4 RID: 24820
		void SetDebugService(DebugService debugService);

		// Token: 0x060060F5 RID: 24821
		void SetSourceCode(SourceCode sourceCode);

		// Token: 0x060060F6 RID: 24822
		void SetByteCode(string[] byteCode);

		// Token: 0x060060F7 RID: 24823
		bool IsPauseRequested();

		// Token: 0x060060F8 RID: 24824
		bool SignalRuntimeException(ScriptRuntimeException ex);

		// Token: 0x060060F9 RID: 24825
		DebuggerAction GetAction(int ip, SourceRef sourceref);

		// Token: 0x060060FA RID: 24826
		void SignalExecutionEnded();

		// Token: 0x060060FB RID: 24827
		void Update(WatchType watchType, IEnumerable<WatchItem> items);

		// Token: 0x060060FC RID: 24828
		List<DynamicExpression> GetWatchItems();

		// Token: 0x060060FD RID: 24829
		void RefreshBreakpoints(IEnumerable<SourceRef> refs);
	}
}
