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
	// Token: 0x02001077 RID: 4215
	public class UserData : RefIdObject
	{
		// Token: 0x060065B2 RID: 26034 RVA: 0x0004604C File Offset: 0x0004424C
		private UserData()
		{
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060065B3 RID: 26035 RVA: 0x00046054 File Offset: 0x00044254
		// (set) Token: 0x060065B4 RID: 26036 RVA: 0x0004605C File Offset: 0x0004425C
		public DynValue UserValue { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00046065 File Offset: 0x00044265
		// (set) Token: 0x060065B6 RID: 26038 RVA: 0x0004606D File Offset: 0x0004426D
		public object Object { get; private set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x00046076 File Offset: 0x00044276
		// (set) Token: 0x060065B8 RID: 26040 RVA: 0x0004607E File Offset: 0x0004427E
		public IUserDataDescriptor Descriptor { get; private set; }

		// Token: 0x060065B9 RID: 26041 RVA: 0x00046087 File Offset: 0x00044287
		static UserData()
		{
			UserData.RegisterType<EventFacade>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<AnonWrapper>(InteropAccessMode.HideMembers, null);
			UserData.RegisterType<EnumerableWrapper>(InteropAccessMode.NoReflectionAllowed, null);
			UserData.RegisterType<JsonNull>(InteropAccessMode.Reflection, null);
			UserData.DefaultAccessMode = InteropAccessMode.LazyOptimized;
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x000460B9 File Offset: 0x000442B9
		public static IUserDataDescriptor RegisterType<T>(InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), accessMode, friendlyName, null);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x000460CD File Offset: 0x000442CD
		public static IUserDataDescriptor RegisterType(Type type, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, accessMode, friendlyName, null);
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x000460D8 File Offset: 0x000442D8
		public static IUserDataDescriptor RegisterProxyType(IProxyFactory proxyFactory, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null)
		{
			return TypeDescriptorRegistry.RegisterProxyType_Impl(proxyFactory, accessMode, friendlyName);
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x000460E2 File Offset: 0x000442E2
		public static IUserDataDescriptor RegisterProxyType<TProxy, TTarget>(Func<TTarget, TProxy> wrapDelegate, InteropAccessMode accessMode = InteropAccessMode.Default, string friendlyName = null) where TProxy : class where TTarget : class
		{
			return UserData.RegisterProxyType(new DelegateProxyFactory<TProxy, TTarget>(wrapDelegate), accessMode, friendlyName);
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x000460F1 File Offset: 0x000442F1
		public static IUserDataDescriptor RegisterType<T>(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(typeof(T), InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x00046105 File Offset: 0x00044305
		public static IUserDataDescriptor RegisterType(Type type, IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x00046110 File Offset: 0x00044310
		public static IUserDataDescriptor RegisterType(IUserDataDescriptor customDescriptor)
		{
			return TypeDescriptorRegistry.RegisterType_Impl(customDescriptor.Type, InteropAccessMode.Default, null, customDescriptor);
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x00046120 File Offset: 0x00044320
		public static void RegisterAssembly(Assembly asm = null, bool includeExtensionTypes = false)
		{
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}
			TypeDescriptorRegistry.RegisterAssembly(asm, includeExtensionTypes);
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x00046139 File Offset: 0x00044339
		public static bool IsTypeRegistered(Type t)
		{
			return TypeDescriptorRegistry.IsTypeRegistered(t);
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00046141 File Offset: 0x00044341
		public static bool IsTypeRegistered<T>()
		{
			return TypeDescriptorRegistry.IsTypeRegistered(typeof(T));
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x00046152 File Offset: 0x00044352
		public static void UnregisterType<T>()
		{
			TypeDescriptorRegistry.UnregisterType(typeof(T));
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x00046163 File Offset: 0x00044363
		public static void UnregisterType(Type t)
		{
			TypeDescriptorRegistry.UnregisterType(t);
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x0004616B File Offset: 0x0004436B
		public static DynValue Create(object o, IUserDataDescriptor descr)
		{
			return DynValue.NewUserData(new UserData
			{
				Descriptor = descr,
				Object = o
			});
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x002835D4 File Offset: 0x002817D4
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

		// Token: 0x060065C8 RID: 26056 RVA: 0x00046185 File Offset: 0x00044385
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

		// Token: 0x060065C9 RID: 26057 RVA: 0x000461A4 File Offset: 0x000443A4
		public static DynValue CreateStatic(Type t)
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(t, false));
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x000461B2 File Offset: 0x000443B2
		public static DynValue CreateStatic<T>()
		{
			return UserData.CreateStatic(UserData.GetDescriptorForType(typeof(T), false));
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060065CB RID: 26059 RVA: 0x000461C9 File Offset: 0x000443C9
		// (set) Token: 0x060065CC RID: 26060 RVA: 0x000461D0 File Offset: 0x000443D0
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

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x000461D8 File Offset: 0x000443D8
		// (set) Token: 0x060065CE RID: 26062 RVA: 0x000461DF File Offset: 0x000443DF
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

		// Token: 0x060065CF RID: 26063 RVA: 0x000461E7 File Offset: 0x000443E7
		public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
		{
			ExtensionMethodsRegistry.RegisterExtensionType(type, mode);
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x000461F0 File Offset: 0x000443F0
		public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsByNameAndType(name, extendedType);
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x000461F9 File Offset: 0x000443F9
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.GetExtensionMethodsChangeVersion();
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x00046200 File Offset: 0x00044400
		public static IUserDataDescriptor GetDescriptorForType<T>(bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(typeof(T), searchInterfaces);
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x00046212 File Offset: 0x00044412
		public static IUserDataDescriptor GetDescriptorForType(Type type, bool searchInterfaces)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(type, searchInterfaces);
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x0004621B File Offset: 0x0004441B
		public static IUserDataDescriptor GetDescriptorForObject(object o)
		{
			return TypeDescriptorRegistry.GetDescriptorForType(o.GetType(), true);
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x00283608 File Offset: 0x00281808
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

		// Token: 0x060065D6 RID: 26070 RVA: 0x00046229 File Offset: 0x00044429
		public static IEnumerable<Type> GetRegisteredTypes(bool useHistoricalData = false)
		{
			return from p in useHistoricalData ? TypeDescriptorRegistry.RegisteredTypesHistory : TypeDescriptorRegistry.RegisteredTypes
			select p.Value.Type;
		}
	}
}
