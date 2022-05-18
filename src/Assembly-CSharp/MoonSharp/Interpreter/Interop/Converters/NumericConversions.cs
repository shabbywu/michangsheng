using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02001143 RID: 4419
	internal static class NumericConversions
	{
		// Token: 0x06006B3D RID: 27453 RVA: 0x00292C34 File Offset: 0x00290E34
		static NumericConversions()
		{
			NumericConversions.NumericTypes = new HashSet<Type>(NumericConversions.NumericTypesOrdered);
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x00292CF0 File Offset: 0x00290EF0
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

		// Token: 0x06006B3F RID: 27455 RVA: 0x00292E34 File Offset: 0x00291034
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

		// Token: 0x040060DD RID: 24797
		internal static readonly HashSet<Type> NumericTypes;

		// Token: 0x040060DE RID: 24798
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
