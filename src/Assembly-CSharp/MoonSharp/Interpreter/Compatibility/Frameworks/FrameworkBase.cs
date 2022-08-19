using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x02000D8E RID: 3470
	public abstract class FrameworkBase
	{
		// Token: 0x060062A6 RID: 25254
		public abstract bool StringContainsChar(string str, char chr);

		// Token: 0x060062A7 RID: 25255
		public abstract bool IsValueType(Type t);

		// Token: 0x060062A8 RID: 25256
		public abstract Assembly GetAssembly(Type t);

		// Token: 0x060062A9 RID: 25257
		public abstract Type GetBaseType(Type t);

		// Token: 0x060062AA RID: 25258
		public abstract bool IsGenericType(Type t);

		// Token: 0x060062AB RID: 25259
		public abstract bool IsGenericTypeDefinition(Type t);

		// Token: 0x060062AC RID: 25260
		public abstract bool IsEnum(Type t);

		// Token: 0x060062AD RID: 25261
		public abstract bool IsNestedPublic(Type t);

		// Token: 0x060062AE RID: 25262
		public abstract bool IsAbstract(Type t);

		// Token: 0x060062AF RID: 25263
		public abstract bool IsInterface(Type t);

		// Token: 0x060062B0 RID: 25264
		public abstract Attribute[] GetCustomAttributes(Type t, bool inherit);

		// Token: 0x060062B1 RID: 25265
		public abstract Attribute[] GetCustomAttributes(Type t, Type at, bool inherit);

		// Token: 0x060062B2 RID: 25266
		public abstract Type[] GetInterfaces(Type t);

		// Token: 0x060062B3 RID: 25267
		public abstract bool IsInstanceOfType(Type t, object o);

		// Token: 0x060062B4 RID: 25268
		public abstract MethodInfo GetAddMethod(EventInfo ei);

		// Token: 0x060062B5 RID: 25269
		public abstract MethodInfo GetRemoveMethod(EventInfo ei);

		// Token: 0x060062B6 RID: 25270
		public abstract MethodInfo GetGetMethod(PropertyInfo pi);

		// Token: 0x060062B7 RID: 25271
		public abstract MethodInfo GetSetMethod(PropertyInfo pi);

		// Token: 0x060062B8 RID: 25272
		public abstract Type GetInterface(Type type, string name);

		// Token: 0x060062B9 RID: 25273
		public abstract PropertyInfo[] GetProperties(Type type);

		// Token: 0x060062BA RID: 25274
		public abstract PropertyInfo GetProperty(Type type, string name);

		// Token: 0x060062BB RID: 25275
		public abstract Type[] GetNestedTypes(Type type);

		// Token: 0x060062BC RID: 25276
		public abstract EventInfo[] GetEvents(Type type);

		// Token: 0x060062BD RID: 25277
		public abstract ConstructorInfo[] GetConstructors(Type type);

		// Token: 0x060062BE RID: 25278
		public abstract Type[] GetAssemblyTypes(Assembly asm);

		// Token: 0x060062BF RID: 25279
		public abstract MethodInfo[] GetMethods(Type type);

		// Token: 0x060062C0 RID: 25280
		public abstract FieldInfo[] GetFields(Type t);

		// Token: 0x060062C1 RID: 25281
		public abstract MethodInfo GetMethod(Type type, string name);

		// Token: 0x060062C2 RID: 25282
		public abstract Type[] GetGenericArguments(Type t);

		// Token: 0x060062C3 RID: 25283
		public abstract bool IsAssignableFrom(Type current, Type toCompare);

		// Token: 0x060062C4 RID: 25284
		public abstract bool IsDbNull(object o);

		// Token: 0x060062C5 RID: 25285
		public abstract MethodInfo GetMethod(Type resourcesType, string v, Type[] type);
	}
}
