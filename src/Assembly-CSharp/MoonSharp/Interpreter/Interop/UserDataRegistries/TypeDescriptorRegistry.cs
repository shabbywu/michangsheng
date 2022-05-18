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
	// Token: 0x02001130 RID: 4400
	internal static class TypeDescriptorRegistry
	{
		// Token: 0x06006A68 RID: 27240 RVA: 0x0029082C File Offset: 0x0028EA2C
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

		// Token: 0x06006A69 RID: 27241 RVA: 0x002909C4 File Offset: 0x0028EBC4
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

		// Token: 0x06006A6A RID: 27242 RVA: 0x00290A0C File Offset: 0x0028EC0C
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

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06006A6B RID: 27243 RVA: 0x000488EC File Offset: 0x00046AEC
		// (set) Token: 0x06006A6C RID: 27244 RVA: 0x000488F3 File Offset: 0x00046AF3
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

		// Token: 0x06006A6D RID: 27245 RVA: 0x00290A68 File Offset: 0x0028EC68
		internal static IUserDataDescriptor RegisterProxyType_Impl(IProxyFactory proxyFactory, InteropAccessMode accessMode, string friendlyName)
		{
			IUserDataDescriptor proxyDescriptor = TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.ProxyType, accessMode, friendlyName, null);
			return TypeDescriptorRegistry.RegisterType_Impl(proxyFactory.TargetType, accessMode, friendlyName, new ProxyUserDataDescriptor(proxyFactory, proxyDescriptor, friendlyName));
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x00290A9C File Offset: 0x0028EC9C
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

		// Token: 0x06006A6F RID: 27247 RVA: 0x00290BEC File Offset: 0x0028EDEC
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

		// Token: 0x06006A70 RID: 27248 RVA: 0x00290C34 File Offset: 0x0028EE34
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

		// Token: 0x06006A71 RID: 27249 RVA: 0x00290C74 File Offset: 0x0028EE74
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

		// Token: 0x06006A72 RID: 27250 RVA: 0x0001C722 File Offset: 0x0001A922
		private static bool FrameworkIsAssignableFrom(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x0004890A File Offset: 0x00046B0A
		public static bool IsTypeBlacklisted(Type t)
		{
			return Framework.Do.IsValueType(t) && Framework.Do.GetInterfaces(t).Contains(typeof(IEnumerator));
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06006A74 RID: 27252 RVA: 0x00290E80 File Offset: 0x0028F080
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

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06006A75 RID: 27253 RVA: 0x00290EC8 File Offset: 0x0028F0C8
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

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x00048938 File Offset: 0x00046B38
		// (set) Token: 0x06006A77 RID: 27255 RVA: 0x0004893F File Offset: 0x00046B3F
		internal static IRegistrationPolicy RegistrationPolicy { get; set; }

		// Token: 0x040060B3 RID: 24755
		private static object s_Lock = new object();

		// Token: 0x040060B4 RID: 24756
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistry = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x040060B5 RID: 24757
		private static Dictionary<Type, IUserDataDescriptor> s_TypeRegistryHistory = new Dictionary<Type, IUserDataDescriptor>();

		// Token: 0x040060B6 RID: 24758
		private static InteropAccessMode s_DefaultAccessMode;
	}
}
