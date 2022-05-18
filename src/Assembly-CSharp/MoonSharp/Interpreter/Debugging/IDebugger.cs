using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x0200117D RID: 4477
	public interface IDebugger
	{
		// Token: 0x06006CE7 RID: 27879
		DebuggerCaps GetDebuggerCaps();

		// Token: 0x06006CE8 RID: 27880
		void SetDebugService(DebugService debugService);

		// Token: 0x06006CE9 RID: 27881
		void SetSourceCode(SourceCode sourceCode);

		// Token: 0x06006CEA RID: 27882
		void SetByteCode(string[] byteCode);

		// Token: 0x06006CEB RID: 27883
		bool IsPauseRequested();

		// Token: 0x06006CEC RID: 27884
		bool SignalRuntimeException(ScriptRuntimeException ex);

		// Token: 0x06006CED RID: 27885
		DebuggerAction GetAction(int ip, SourceRef sourceref);

		// Token: 0x06006CEE RID: 27886
		void SignalExecutionEnded();

		// Token: 0x06006CEF RID: 27887
		void Update(WatchType watchType, IEnumerable<WatchItem> items);

		// Token: 0x06006CF0 RID: 27888
		List<DynamicExpression> GetWatchItems();

		// Token: 0x06006CF1 RID: 27889
		void RefreshBreakpoints(IEnumerable<SourceRef> refs);
	}
}
