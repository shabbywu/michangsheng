using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1D RID: 3357
	public sealed class ProxyUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06005DED RID: 24045 RVA: 0x0026464A File Offset: 0x0026284A
		internal ProxyUserDataDescriptor(IProxyFactory proxyFactory, IUserDataDescriptor proxyDescriptor, string friendlyName = null)
		{
			this.m_ProxyFactory = proxyFactory;
			this.Name = (friendlyName ?? (proxyFactory.TargetType.Name + "::proxy"));
			this.m_ProxyDescriptor = proxyDescriptor;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x00264680 File Offset: 0x00262880
		public IUserDataDescriptor InnerDescriptor
		{
			get
			{
				return this.m_ProxyDescriptor;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06005DEF RID: 24047 RVA: 0x00264688 File Offset: 0x00262888
		// (set) Token: 0x06005DF0 RID: 24048 RVA: 0x00264690 File Offset: 0x00262890
		public string Name { get; private set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06005DF1 RID: 24049 RVA: 0x00264699 File Offset: 0x00262899
		public Type Type
		{
			get
			{
				return this.m_ProxyFactory.TargetType;
			}
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x002646A6 File Offset: 0x002628A6
		private object Proxy(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return this.m_ProxyFactory.CreateProxyObject(obj);
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x002646B9 File Offset: 0x002628B9
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.Index(script, this.Proxy(obj), index, isDirectIndexing);
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x002646D1 File Offset: 0x002628D1
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return this.m_ProxyDescriptor.SetIndex(script, this.Proxy(obj), index, value, isDirectIndexing);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x002646EB File Offset: 0x002628EB
		public string AsString(object obj)
		{
			return this.m_ProxyDescriptor.AsString(this.Proxy(obj));
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x002646FF File Offset: 0x002628FF
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return this.m_ProxyDescriptor.MetaIndex(script, this.Proxy(obj), metaname);
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x00259E25 File Offset: 0x00258025
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04005416 RID: 21526
		private IUserDataDescriptor m_ProxyDescriptor;

		// Token: 0x04005417 RID: 21527
		private IProxyFactory m_ProxyFactory;
	}
}
