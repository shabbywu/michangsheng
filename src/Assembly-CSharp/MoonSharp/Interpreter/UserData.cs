using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.RegistrationPolicies;
using MoonSharp.Interpreter.Interop.StandardDescriptors;
using MoonSharp.Interpreter.Interop.UserDataRegistries;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA9 RID: 3241
	public class UserData : RefIdObject
	{
		// Token: 0x06005AC3 RID: 23235 RVA: 0x00258F40 File Offset: 0x00257140
		private UserData()
		{
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06005AC4 RID: 23236 RVA: 0x00258F48 File Offset: 0x00257148
		// (set) Token: 0x06005AC5 RID: 23237 RVA: 0x00258F50 File Offset: 0x00257150
		public DynValue UserValue { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06005AC6 RID: 23238 RVA: 0x00258F59 File Offset: 0x00257159
		// (set) Token: 0x06005AC7 RID: 23239 RVA: 0x00258F61 File Offset: 0x00257161
		public object Object { get; private set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x00258F6A File Offset: 0x0025716A
		// (set) Token: 0x06005AC9 RID: 23241 RVA: 0x00258F72 File Offset: 0x00257172
		public IUserDataDescriptor Descriptor { get; private set; }

		// Token: 0x06005ACA RID: 23242 RVA: 0x00258F7B File Offset: 0x0025717B
		static UserData()
		{
			UserData.RegisterType<EventFacade>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<AnonWrapper>(InteropAccessMode.HideMembers, null);
			UserData.RegisterType<EnumerableWrapper>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<JsonNull>(InteropAccessMode.Reflection, null);
			UserData.DefaultAccessMode = InteropAccessMode.LazyOptimized;
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x00258FAD File Offset: 0x002571AD
		public static IUserDataDescriptor RegisterType<T>(InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), accessMode, friendlyName, null);
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x00258FC1 File Offset: 0x002571C1
		public static IUserDataDescriptor RegisterType(Type type, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, accessMode, friendlyName, null);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x00258FCC File Offset: 0x002571CC
		public static IUserDataDescriptor RegisterProxyType(IProxyFactory proxyFactory, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterProxyType_Impl(proxyFactory, accessMode, friendlyName);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x00258FD6 File Offset: 0x002571D6
		public static IUserDataDescriptor RegisterProxyType<TProxy, TTarget>(Func<TTarget, TProxy> wrapDelegate, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null) where TProxy : class where TTarget : class
		{
			return UserData.RegisterProxyType(new DelegateProxyFactory<TProxy, TTarget>(wrapDelegate), accessMode, friendlyName);
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x00258FE5 File Offset: 0x002571E5
		public static IUserDataDescriptor RegisterType<T>(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x00258FF9 File Offset: 0x002571F9
		public static IUserDataDescriptor RegisterType(Type type, IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x00259004 File Offset: 0x00257204
		public static IUserDataDescriptor RegisterType(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(customDescriptor.Type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x00259014 File Offset: 0x00257214
		public static void RegisterAssembly(Assembly asm = null, bool includeExtensionTypes = false)
		{
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}
			TypeDescriptorRegistry.RegisterAssembly(asm, includeExtensionTypes);
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0025902D File Offset: 0x0025722D
		public static bool IsTypeRegistered(Type t)
		{
			return TypeDescriptorRegistry.IsTypeRegistered(t);
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x00259035 File Offset: 0x00257235
		public static bool IsTypeRegistered<T>()
		{
			return TypeDescriptorRegistry.IsTypeRegistered(typeof(T));
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00259046 File Offset: 0x00257246
		public static void UnregisterType<T>()
		{
			TypeDescriptorRegistry.UnregisterType(typeof(T));
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x00259057 File Offset: 0x00257257
		public static void UnregisterType(Type t)
		{
			TypeDescriptorRegistry.UnregisterType(t);
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0025905F File Offset: 0x0025725F
		public static DynValue Create(object o, IUserDataDescriptor descr)
		{
			return DynValue.NewUserData(new UserData
			{
				Descriptor = descr,
				Object = o
			});
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0025907C File Offset: 0x0025727C
		public static DynValue Create(object o)
		{
			IUserDataDescriptor descriptorForObject = UserData.GetDescriptorForObject(o);
			if (descriptorForObject != null)
			{
				return UserData.Create(o, descriptorForObject);
			}
			if (o is Type)
			{
				return UserData.CreateStatic((Type)o);
			}
			return null;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x002590B0 File Offset: 0x002572B0
		public static DynValue CreateStatic(IUserDataDescriptor descr)
		{
			if (descr == null)
			{
				return null;
			}
			return DynValue.NewUserData(new UserData
			{
				Descriptor = descr,
				Object = null
			});
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x002590CF File Offset: 0x002572CF
		public static DynValue CreateStatic(Type t)
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(t, false));
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x002590DD File Offset: 0x002572DD
		public static DynValue CreateStatic<T>()
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(typeof(T), false));
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x002590F4 File Offset: 0x002572F4
		// (set) Token: 0x06005ADD RID: 23261 RVA: 0x002590FB File Offset: 0x002572FB
		public static IRegistrationPolicy RegistrationPolicy
		{
			get
			{
				return TypeDescriptorRegistry.RegistrationPolicy;
			}
			set
			{
				TypeDescriptorRegistry.RegistrationPolicy = value;
			}
		} = InteropRegistrationPolicy.Default;

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x00259103 File Offset: 0x00257303
		// (set) Token: 0x06005ADF RID: 23263 RVA: 0x0025910A File Offset: 0x0025730A
		public static InteropAccessMode DefaultAccessMode
		{
			get
			{
				return TypeDescriptorRegistry.DefaultAccessMode;
			}
			set
			{
				TypeDescriptorRegistry.DefaultAccessMode = value;
			}
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x00259112 File Offset: 0x00257312
		public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
		{
			ExtensionMethodsRegistry.RegisterExtensionType(type, mode);
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x0025911B File Offset: 0x0025731B
		public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsByNameAndType(name, extendedType);
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x00259124 File Offset: 0x00257324
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsChangeVersion();
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x0025912B File Offset: 0x0025732B
		public static IUserDataDescriptor GetDescriptorForType<T>(bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(typeof(T), searchInterfaces);
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x0025913D File Offset: 0x0025733D
		public static IUserDataDescriptor GetDescriptorForType(Type type, bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(type, searchInterfaces);
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x00259146 File Offset: 0x00257346
		public static IUserDataDescriptor GetDescriptorForObject(object o)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(o.GetType(), true);
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x00259154 File Offset: 0x00257354
		public static Table GetDescriptionOfRegisteredTypes(bool useHistoricalData = false)
		{
			DynValue dynValue = DynValue.NewPrimeTable();
			foreach (KeyValuePair<Type, IUserDataDescriptor> keyValuePair in (useHistoricalData ? TypeDescriptorRegistry.RegisteredTypesHistory : TypeDescriptorRegistry.RegisteredTypes))
			{
				IWireableDescriptor wireableDescriptor = keyValuePair.Value as IWireableDescriptor;
				if (wireableDescriptor != null)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(keyValuePair.Key.FullName, dynValue2);
					wireableDescriptor.PrepareForWiring(dynValue2.Table);
				}
			}
			return dynValue.Table;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x002591EC File Offset: 0x002573EC
		public static IEnumerable<Type> GetRegisteredTypes(bool useHistoricalData = false)
		{
			return from p in useHistoricalData ? TypeDescriptorRegistry.RegisteredTypesHistory : TypeDescriptorRegistry.RegisteredTypes
			select p.Value.Type;
		}
	}
}
