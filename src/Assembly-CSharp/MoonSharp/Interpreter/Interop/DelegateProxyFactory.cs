using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F7 RID: 4343
	public class DelegateProxyFactory<TProxy, TTarget> : IProxyFactory<TProxy, TTarget>, IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x060068D4 RID: 26836 RVA: 0x00047DD1 File Offset: 0x00045FD1
		public DelegateProxyFactory(Func<TTarget, TProxy> wrapDelegate)
		{
			this.wrapDelegate = wrapDelegate;
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x00047DE0 File Offset: 0x00045FE0
		public TProxy CreateProxyObject(TTarget target)
		{
			return this.wrapDelegate(target);
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x00047DEE File Offset: 0x00045FEE
		public object CreateProxyObject(object o)
		{
			return this.CreateProxyObject((TTarget)((object)o));
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060068D7 RID: 26839 RVA: 0x00047E01 File Offset: 0x00046001
		public Type TargetType
		{
			get
			{
				return typeof(TTarget);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060068D8 RID: 26840 RVA: 0x00047E0D File Offset: 0x0004600D
		public Type ProxyType
		{
			get
			{
				return typeof(TProxy);
			}
		}

		// Token: 0x04005FFD RID: 24573
		private Func<TTarget, TProxy> wrapDelegate;
	}
}
