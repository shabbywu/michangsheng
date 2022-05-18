using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F8 RID: 4344
	public interface IProxyFactory
	{
		// Token: 0x060068D9 RID: 26841
		object CreateProxyObject(object o);

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060068DA RID: 26842
		Type TargetType { get; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060068DB RID: 26843
		Type ProxyType { get; }
	}
}
