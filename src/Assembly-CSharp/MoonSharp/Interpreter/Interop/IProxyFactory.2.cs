using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D15 RID: 3349
	public interface IProxyFactory<TProxy, TTarget> : IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x06005DAD RID: 23981
		TProxy CreateProxyObject(TTarget target);
	}
}
