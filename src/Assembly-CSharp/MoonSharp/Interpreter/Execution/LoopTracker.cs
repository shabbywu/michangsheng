using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115C RID: 4444
	internal class LoopTracker
	{
		// Token: 0x04006130 RID: 24880
		public FastStack<ILoop> Loops = new FastStack<ILoop>(16384);
	}
}
