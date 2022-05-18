using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F9 RID: 4345
	public interface IProxyFactory<TProxy, TTarget> : IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x060068DC RID: 26844
		TProxy CreateProxyObject(TTarget target);
	}
}
