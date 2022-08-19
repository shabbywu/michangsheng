using System;
using System.Collections;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Serialization
{
	// Token: 0x02000CEC RID: 3308
	public static class ObjectValueConverter
	{
		// Token: 0x06005C9D RID: 23709 RVA: 0x00260CDC File Offset: 0x0025EEDC
		public static DynValue SerializeObjectToDynValue(Script script, object o, DynValue valueForNulls = null)
		{
			if (o == null)
			{
				return valueForNulls ?? DynValue.Nil;
			}
			DynValue dynValue = ClrToScriptConversions.TryObjectToTrivialDynValue(script, o);
			if (dynValue != null)
			{
				return dynValue;
			}
			if (o is Enum)
			{
				return DynValue.NewNumber(NumericConversions.TypeToDouble(Enum.GetUnderlyingType(o.GetType()), o));
			}
			Table table = new Table(script);
			IEnumerable enumerable = o as IEnumerable;
			if (enumerable != null)
			{
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object o2 = enumerator.Current;
						table.Append(ObjectValueConverter.SerializeObjectToDynValue(script, o2, valueForNulls));
					}
					goto IL_F3;
				}
			}
			Type type = o.GetType();
			foreach (PropertyInfo propertyInfo in Framework.Do.GetProperties(type))
			{
				MethodInfo getMethod = Framework.Do.GetGetMethod(propertyInfo);
				object o3 = getMethod.Invoke(getMethod.IsStatic ? null : o, null);
				table.Set(propertyInfo.Name, ObjectValueConverter.SerializeObjectToDynValue(script, o3, valueForNulls));
			}
			IL_F3:
			return DynValue.NewTable(table);
		}
	}
}
