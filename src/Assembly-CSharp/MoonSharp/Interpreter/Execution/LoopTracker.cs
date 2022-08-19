using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4D RID: 3405
	internal class LoopTracker
	{
		// Token: 0x040054B5 RID: 21685
		public FastStack<ILoop> Loops = new FastStack<ILoop>(16384);
	}
}
