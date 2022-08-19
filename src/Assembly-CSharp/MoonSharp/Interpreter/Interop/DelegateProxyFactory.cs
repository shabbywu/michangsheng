using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D13 RID: 3347
	public class DelegateProxyFactory<TProxy, TTarget> : IProxyFactory<TProxy, TTarget>, IProxyFactory where TProxy : class where TTarget : class
	{
		// Token: 0x06005DA5 RID: 23973 RVA: 0x002634FF File Offset: 0x002616FF
		public DelegateProxyFactory(Func<TTarget, TProxy> wrapDelegate)
		{
			this.wrapDelegate = wrapDelegate;
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x0026350E File Offset: 0x0026170E
		public TProxy CreateProxyObject(TTarget target)
		{
			return this.wrapDelegate(target);
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x0026351C File Offset: 0x0026171C
		public object CreateProxyObject(object o)
		{
			return this.CreateProxyObject((TTarget)((object)o));
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0026352F File Offset: 0x0026172F
		public Type TargetType
		{
			get
			{
				return typeof(TTarget);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x0026353B File Offset: 0x0026173B
		public Type ProxyType
		{
			get
			{
				return typeof(TProxy);
			}
		}

		// Token: 0x040053E6 RID: 21478
		private Func<TTarget, TProxy> wrapDelegate;
	}
}
