using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02000D3B RID: 3387
	internal static class NumericConversions
	{
		// Token: 0x06005F6B RID: 24427 RVA: 0x0026AD6C File Offset: 0x00268F6C
		static NumericConversions()
		{
			NumericConversions.NumericTypes = new HashSet<Type>(NumericConversions.NumericTypesOrdered);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x0026AE28 File Offset: 0x00269028
		internal static object DoubleToType(Type type, double d)
		{
			type = (Nullable.GetUnderlyingType(type) ?? type);
			if (type == typeof(double))
			{
				return d;
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)d;
			}
			if (type == typeof(byte))
			{
				return (byte)d;
			}
			if (type == typeof(short))
			{
				return (short)d;
			}
			if (type == typeof(ushort))
			{
				return (ushort)d;
			}
			if (type == typeof(int))
			{
				return (int)d;
			}
			if (type == typeof(uint))
			{
				return (uint)d;
			}
			if (type == typeof(long))
			{
				return (long)d;
			}
			if (type == typeof(ulong))
			{
				return (ulong)d;
			}
			if (type == typeof(float))
			{
				return (float)d;
			}
			if (type == typeof(decimal))
			{
				return (decimal)d;
			}
			return d;
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x0026AF6C File Offset: 0x0026916C
		internal static double TypeToDouble(Type type, object d)
		{
			if (type == typeof(double))
			{
				return (double)d;
			}
			if (type == typeof(sbyte))
			{
				return (double)((sbyte)d);
			}
			if (type == typeof(byte))
			{
				return (double)((byte)d);
			}
			if (type == typeof(short))
			{
				return (double)((short)d);
			}
			if (type == typeof(ushort))
			{
				return (double)((ushort)d);
			}
			if (type == typeof(int))
			{
				return (double)((int)d);
			}
			if (type == typeof(uint))
			{
				return (uint)d;
			}
			if (type == typeof(long))
			{
				return (double)((long)d);
			}
			if (type == typeof(ulong))
			{
				return (ulong)d;
			}
			if (type == typeof(float))
			{
				return (double)((float)d);
			}
			if (type == typeof(decimal))
			{
				return (double)((decimal)d);
			}
			return (double)d;
		}

		// Token: 0x0400547B RID: 21627
		internal static readonly HashSet<Type> NumericTypes;

		// Token: 0x0400547C RID: 21628
		internal static readonly Type[] NumericTypesOrdered = new Type[]
		{
			typeof(double),
			typeof(decimal),
			typeof(float),
			typeof(long),
			typeof(int),
			typeof(short),
			typeof(sbyte),
			typeof(ulong),
			typeof(uint),
			typeof(ushort),
			typeof(byte)
		};
	}
}
