using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02001104 RID: 4356
	public sealed class ProxyUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06006924 RID: 26916 RVA: 0x0004811C File Offset: 0x0004631C
		internal ProxyUserDataDescriptor(IProxyFactory proxyFactory, IUserDataDescriptor proxyDescriptor, string friendlyName = null)
		{
			this.m_ProxyFactory = proxyFactory;
			this.Name = (friendlyName ?? (proxyFactory.TargetType.Name + "::proxy"));
			this.m_ProxyDescriptor = proxyDescriptor;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06006925 RID: 26917 RVA: 0x00048152 File Offset: 0x00046352
		public IUserDataDescriptor InnerDescriptor
		{
			get
			{
				return this.m_ProxyDescriptor;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x0004815A File Offset: 0x0004635A
		// (set) Token: 0x06006927 RID: 26919 RVA: 0x00048162 File Offset: 0x00046362
		public string Name { get; private set; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x0004816B File Offset: 0x0004636B
		public Type Type
		{
			get
			{
				return this.m_ProxyFactory.TargetType;
			}
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x00048178 File Offset: 0x00046378
		private object Proxy(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return this.m_ProxyFactory.CreateProxyObject(obj);
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x0004818B File Offset: 0x0004638B
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.Index(script, this.Proxy(obj), index, isDirectIndexing);
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x000481A3 File Offset: 0x000463A3
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.SetIndex(script, this.Proxy(obj), index, value, isDirectIndexing);
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x000481BD File Offset: 0x000463BD
		public string AsString(object obj)
		{
			return this.m_ProxyDescriptor.AsString(this.Proxy(obj));
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x000481D1 File Offset: 0x000463D1
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return this.m_ProxyDescriptor.MetaIndex(script, this.Proxy(obj), metaname);
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00046989 File Offset: 0x00044B89
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04006034 RID: 24628
		private IUserDataDescriptor m_ProxyDescriptor;

		// Token: 0x04006035 RID: 24629
		private IProxyFactory m_ProxyFactory;
	}
}
