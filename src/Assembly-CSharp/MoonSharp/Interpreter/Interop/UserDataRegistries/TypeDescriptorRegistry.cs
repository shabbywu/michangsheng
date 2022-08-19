using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;

namespace MoonSharp.Interpreter.Interop.UserDataRegistries
{
	// Token: 0x02000D2A RID: 3370
	internal static class TypeDescriptorRegistry
	{
		// Token: 0x06005EA1 RID: 24225 RVA: 0x0026813C File Offset: 0x0026633C
		internal static void RegisterAssembly(Assembly asm = null, bool includeExtensionTypes = false)
		{
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}
			if (includeExtensionTypes)
			{
				foreach (var <>f__AnonymousType in from t in asm.SafeGetTypes()
				let attributes = Framework.Do.GetCustomAttributes(t, typeof(ExtensionAttribute), true)
				where attributes != null && attributes.Length != 0
				select new
				{
					Attributes = attributes,
					DataType = t
				})
				{
					UserData.RegisterExtensionType(<>f__AnonymousType.DataType, InteropAccessMode.Default);
				}
			}
			foreach (var <>f__AnonymousType2 in from t in asm.SafeGetTypes()
			let attributes = Framework.Do.GetCustomAttributes(t, typeof(MoonSharpUserDataAttribute), true)
			where attributes != null && attributes.Length != 0
			select new
			{
				Attributes = attributes,
				DataType = t
			})
			{
				UserData.RegisterType(<>f__AnonymousType2.DataType, <>f__AnonymousType2.Attributes.OfType<MoonSharpUserDataAttribute>().First<MoonSharpUserDataAttribute>().AccessMode, null);
			}
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x002682D4 File Offset: 0x002664D4
		internal static bool IsTypeRegistered(Type type)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			bool result;
			lock (obj)
			{
				result = TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(type);
			}
			return result;
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x0026831C File Offset: 0x0026651C
		internal static void UnregisterType(Type t)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			lock (obj)
			{
				if (TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(t))
				{
					TypeDescriptorRegistry.PerformRegistration(t, null, TypeDescriptorRegistry.s_TypeRegistry[t]);
				}
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06005EA4 RID: 24228 RVA: 0x00268378 File Offset: 0x00266578
		// (set) Token: 0x06005EA5 RID: 24229 RVA: 0x0026837F File Offset: 0x0026657F
		internal static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return TypeDescriptorRegistry.s_DefaultAccessMode;
			}
			set
			{
				if (value == InteropAccessMode.Default)
				{
					throw new ArgumentException("InteropAccessMode is InteropAccessMode.Default");
				}
				TypeDescriptorRegistry.s_DefaultAccessMode = value;
			}
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x00268398 File Offset: 0x00266598
		internal static IUserDataDescriptor RegisterProxyType_Impl(IProxyFactory proxyFactory, InteropAccessMode accessMode, string friendlyName)
		{
			IUserDataDescriptor proxyDescriptor = TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.ProxyType, accessMode, friendlyName, null);
			return TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.TargetType, accessMode, friendlyName, new ProxyUserDataDescriptor(proxyFactory, proxyDescriptor, friendlyName));
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x002683CC File Offset: 0x002665CC
		internal static IUserDataDescriptor RegisterType_Impl(Type type, InteropAccessMode accessMode, string friendlyName, IUserDataDescriptor descriptor)
		{
			accessMode = TypeDescriptorRegistry.ResolveDefaultAccessModeForType(accessMode, type);
			object obj = TypeDescriptorRegistry.s_Lock;
			IUserDataDescriptor result;
			lock (obj)
			{
				IUserDataDescriptor oldDescriptor = null;
				TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type, out oldDescriptor);
				if (descriptor == null)
				{
					if (TypeDescriptorRegistry.IsTypeBlacklisted(type))
					{
						result = null;
					}
					else if (Framework.Do.GetInterfaces(type).Any((Type ii) => ii == typeof(IUserDataType)))
					{
						AutoDescribingUserDataDescriptor newDescriptor = new AutoDescribingUserDataDescriptor(type, friendlyName);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor, oldDescriptor);
					}
					else if (Framework.Do.IsGenericTypeDefinition(type))
					{
						StandardGenericsUserDataDescriptor newDescriptor2 = new StandardGenericsUserDataDescriptor(type, accessMode);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor2, oldDescriptor);
					}
					else if (Framework.Do.IsEnum(type))
					{
						StandardEnumUserDataDescriptor newDescriptor3 = new StandardEnumUserDataDescriptor(type, friendlyName, null, null, null);
						result = TypeDescriptorRegistry.PerformRegistration(type, newDescriptor3, oldDescriptor);
					}
					else
					{
						StandardUserDataDescriptor udd = new StandardUserDataDescriptor(type, accessMode, friendlyName);
						if (accessMode == InteropAccessMode.BackgroundOptimized)
						{
							ThreadPool.QueueUserWorkItem(delegate(object o)
							{
								((IOptimizableDescriptor)udd).Optimize();
							});
						}
						result = TypeDescriptorRegistry.PerformRegistration(type, udd, oldDescriptor);
					}
				}
				else
				{
					TypeDescriptorRegistry.PerformRegistration(type, descriptor, oldDescriptor);
					result = descriptor;
				}
			}
			return result;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x0026851C File Offset: 0x0026671C
		private static IUserDataDescriptor PerformRegistration(Type type, IUserDataDescriptor newDescriptor, IUserDataDescriptor oldDescriptor)
		{
			IUserDataDescriptor userDataDescriptor = TypeDescriptorRegistry.RegistrationPolicy.HandleRegistration(newDescriptor, oldDescriptor);
			if (userDataDescriptor != oldDescriptor)
			{
				if (userDataDescriptor == null)
				{
					TypeDescriptorRegistry.s_TypeRegistry.Remove(type);
				}
				else
				{
					TypeDescriptorRegistry.s_TypeRegistry[type] = userDataDescriptor;
					TypeDescriptorRegistry.s_TypeRegistryHistory[type] = userDataDescriptor;
				}
			}
			return userDataDescriptor;
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x00268564 File Offset: 0x00266764
		internal static InteropAccessMode ResolveDefaultAccessModeForType(InteropAccessMode accessMode, Type type)
		{
			if (accessMode == InteropAccessMode.Default)
			{
				MoonSharpUserDataAttribute moonSharpUserDataAttribute = Framework.Do.GetCustomAttributes(type, true).OfType<MoonSharpUserDataAttribute>().SingleOrDefault<MoonSharpUserDataAttribute>();
				if (moonSharpUserDataAttribute != null)
				{
					accessMode = moonSharpUserDataAttribute.AccessMode;
				}
			}
			if (accessMode == InteropAccessMode.Default)
			{
				accessMode = TypeDescriptorRegistry.s_DefaultAccessMode;
			}
			return accessMode;
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x002685A4 File Offset: 0x002667A4
		internal static IUserDataDescriptor GetDescriptorForType(Type type, bool searchInterfaces)
		{
			object obj = TypeDescriptorRegistry.s_Lock;
			IUserDataDescriptor result;
			lock (obj)
			{
				IUserDataDescriptor userDataDescriptor = null;
				if (TypeDescriptorRegistry.s_TypeRegistry.ContainsKey(type))
				{
					result = TypeDescriptorRegistry.s_TypeRegistry[type];
				}
				else if (TypeDescriptorRegistry.RegistrationPolicy.AllowTypeAutoRegistration(type) && !Framework.Do.IsAssignableFrom(typeof(Delegate), type))
				{
					result = TypeDescriptorRegistry.RegisterType_Impl(type, TypeDescriptorRegistry.DefaultAccessMode, type.FullName, null);
				}
				else
				{
					Type type2 = type;
					while (type2 != null)
					{
						IUserDataDescriptor userDataDescriptor2;
						if (TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type2, out userDataDescriptor2))
						{
							userDataDescriptor = userDataDescriptor2;
							break;
						}
						if (Framework.Do.IsGenericType(type2) && TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type2.GetGenericTypeDefinition(), out userDataDescriptor2))
						{
							userDataDescriptor = userDataDescriptor2;
							break;
						}
						type2 = Framework.Do.GetBaseType(type2);
					}
					if (userDataDescriptor is IGeneratorUserDataDescriptor)
					{
						userDataDescriptor = ((IGeneratorUserDataDescriptor)userDataDescriptor).Generate(type);
					}
					if (!searchInterfaces)
					{
						result = userDataDescriptor;
					}
					else
					{
						List<IUserDataDescriptor> list = new List<IUserDataDescriptor>();
						if (userDataDescriptor != null)
						{
							list.Add(userDataDescriptor);
						}
						if (searchInterfaces)
						{
							foreach (Type type3 in Framework.Do.GetInterfaces(type))
							{
								IUserDataDescriptor userDataDescriptor3;
								if (TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type3, out userDataDescriptor3))
								{
									if (userDataDescriptor3 is IGeneratorUserDataDescriptor)
									{
										userDataDescriptor3 = ((IGeneratorUserDataDescriptor)userDataDescriptor3).Generate(type);
									}
									if (userDataDescriptor3 != null)
									{
										list.Add(userDataDescriptor3);
									}
								}
								else if (Framework.Do.IsGenericType(type3) && TypeDescriptorRegistry.s_TypeRegistry.TryGetValue(type3.GetGenericTypeDefinition(), out userDataDescriptor3))
								{
									if (userDataDescriptor3 is IGeneratorUserDataDescriptor)
									{
										userDataDescriptor3 = ((IGeneratorUserDataDescriptor)userDataDescriptor3).Generate(type);
									}
									if (userDataDescriptor3 != null)
									{
										list.Add(userDataDescriptor3);
									}
								}
							}
						}
						if (list.Count == 1)
						{
							result = list[0];
						}
						else if (list.Count == 0)
						{
							result = null;
						}
						else
						{
							result = new CompositeUserDataDescriptor(list, type);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		private static bool FrameworkIsAssignableFrom(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005EAC RID: 24236 RVA: 0x002687B0 File Offset: 0x002669B0
		public static bool IsTypeBlacklisted(Type t)
		{
			return Framework.Do.IsValueType(t) && Framework.Do.GetInterfaces(t).Contains(typeof(IEnumerator));
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x002687E0 File Offset: 0x002669E0
		public static IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> RegisteredTypes
		{
			get
			{
				object obj = TypeDescriptorRegistry.s_Lock;
				IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> result;
				lock (obj)
				{
					result = TypeDescriptorRegistry.s_TypeRegistry.ToArray<KeyValuePair<Type, IUserDataDescriptor>>();
				}
				return result;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x00268828 File Offset: 0x00266A28
		public static IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> RegisteredTypesHistory
		{
			get
			{
				object obj = TypeDescriptorRegistry.s_Lock;
				IEnumerable<KeyValuePair<Type, IUserDataDescriptor>> result;
				lock (obj)
				{
					result = TypeDescriptorRegistry.s_TypeRegistryHistory.ToArray<KeyValuePair<Type, IUserDataDescriptor>>();
				}
				return result;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x00268870 File Offset: 0x00266A70
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x00268877 File Offset: 0x00266A77
		internal static IRegistrationPolicy RegistrationPolicy { get; set; }

		// Token: 0x0400545A RID: 21594
		private static object s_Lock = new object();

		// Token: 0x0400545B RID: 21595
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistry = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x0400545C RID: 21596
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistryHistory = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x0400545D RID: 21597
		private static InteropAccessMode s_DefaultAccessMode;
	}
}
