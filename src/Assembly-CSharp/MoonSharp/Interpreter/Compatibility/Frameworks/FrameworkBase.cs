using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020011B1 RID: 4529
	public abstract class FrameworkBase
	{
		// Token: 0x06006ED7 RID: 28375
		public abstract bool StringContainsChar(string str, char chr);

		// Token: 0x06006ED8 RID: 28376
		public abstract bool IsValueType(Type t);

		// Token: 0x06006ED9 RID: 28377
		public abstract Assembly GetAssembly(Type t);

		// Token: 0x06006EDA RID: 28378
		public abstract Type GetBaseType(Type t);

		// Token: 0x06006EDB RID: 28379
		public abstract bool IsGenericType(Type t);

		// Token: 0x06006EDC RID: 28380
		public abstract bool IsGenericTypeDefinition(Type t);

		// Token: 0x06006EDD RID: 28381
		public abstract bool IsEnum(Type t);

		// Token: 0x06006EDE RID: 28382
		public abstract bool IsNestedPublic(Type t);

		// Token: 0x06006EDF RID: 28383
		public abstract bool IsAbstract(Type t);

		// Token: 0x06006EE0 RID: 28384
		public abstract bool IsInterface(Type t);

		// Token: 0x06006EE1 RID: 28385
		public abstract Attribute[] GetCustomAttributes(Type t, bool inherit);

		// Token: 0x06006EE2 RID: 28386
		public abstract Attribute[] GetCustomAttributes(Type t, Type at, bool inherit);

		// Token: 0x06006EE3 RID: 28387
		public abstract Type[] GetInterfaces(Type t);

		// Token: 0x06006EE4 RID: 28388
		public abstract bool IsInstanceOfType(Type t, object o);

		// Token: 0x06006EE5 RID: 28389
		public abstract MethodInfo GetAddMethod(EventInfo ei);

		// Token: 0x06006EE6 RID: 28390
		public abstract MethodInfo GetRemoveMethod(EventInfo ei);

		// Token: 0x06006EE7 RID: 28391
		public abstract MethodInfo GetGetMethod(PropertyInfo pi);

		// Token: 0x06006EE8 RID: 28392
		public abstract MethodInfo GetSetMethod(PropertyInfo pi);

		// Token: 0x06006EE9 RID: 28393
		public abstract Type GetInterface(Type type, string name);

		// Token: 0x06006EEA RID: 28394
		public abstract PropertyInfo[] GetProperties(Type type);

		// Token: 0x06006EEB RID: 28395
		public abstract PropertyInfo GetProperty(Type type, string name);

		// Token: 0x06006EEC RID: 28396
		public abstract Type[] GetNestedTypes(Type type);

		// Token: 0x06006EED RID: 28397
		public abstract EventInfo[] GetEvents(Type type);

		// Token: 0x06006EEE RID: 28398
		public abstract ConstructorInfo[] GetConstructors(Type type);

		// Token: 0x06006EEF RID: 28399
		public abstract Type[] GetAssemblyTypes(Assembly asm);

		// Token: 0x06006EF0 RID: 28400
		public abstract MethodInfo[] GetMethods(Type type);

		// Token: 0x06006EF1 RID: 28401
		public abstract FieldInfo[] GetFields(Type t);

		// Token: 0x06006EF2 RID: 28402
		public abstract MethodInfo GetMethod(Type type, string name);

		// Token: 0x06006EF3 RID: 28403
		public abstract Type[] GetGenericArguments(Type t);

		// Token: 0x06006EF4 RID: 28404
		public abstract bool IsAssignableFrom(Type current, Type toCompare);

		// Token: 0x06006EF5 RID: 28405
		public abstract bool IsDbNull(object o);

		// Token: 0x06006EF6 RID: 28406
		public abstract MethodInfo GetMethod(Type resourcesType, string v, Type[] type);
	}
}
