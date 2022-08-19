using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D14 RID: 3348
	public interface IProxyFactory
	{
		// Token: 0x06005DAA RID: 23978
		object CreateProxyObject(object o);

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06005DAB RID: 23979
		Type TargetType { get; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06005DAC RID: 23980
		Type ProxyType { get; }
	}
}
