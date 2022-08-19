using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D74 RID: 3444
	[MoonSharpModule(Namespace = "bit32")]
	public class Bit32Module
	{
		// Token: 0x0600618D RID: 24973 RVA: 0x00273D1A File Offset: 0x00271F1A
		private static uint ToUInt32(DynValue v)
		{
			return (uint)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x00273D3F File Offset: 0x00271F3F
		private static int ToInt32(DynValue v)
		{
			return (int)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x00273D64 File Offset: 0x00271F64
		private static uint NBitMask(int bits)
		{
			if (bits <= 0)
			{
				return 0U;
			}
			if (bits >= 32)
			{
				return Bit32Module.MASKS[31];
			}
			return Bit32Module.MASKS[bits - 1];
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x00273D84 File Offset: 0x00271F84
		public static uint Bitwise(string funcName, CallbackArguments args, Func<uint, uint, uint> accumFunc)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, funcName, DataType.Number, false));
			for (int i = 1; i < args.Count; i++)
			{
				uint arg = Bit32Module.ToUInt32(args.AsType(i, funcName, DataType.Number, false));
				num = accumFunc(num, arg);
			}
			return num;
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x00273DCC File Offset: 0x00271FCC
		[MoonSharpModuleMethod]
		public static DynValue extract(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "extract", DataType.Number, false));
			DynValue dynValue = args.AsType(1, "extract", DataType.Number, false);
			DynValue dynValue2 = args.AsType(2, "extract", DataType.Number, true);
			int num2 = (int)dynValue.Number;
			int num3 = dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number);
			Bit32Module.ValidatePosWidth("extract", 2, num2, num3);
			return DynValue.NewNumber(num >> (num2 & 31) & Bit32Module.NBitMask(num3));
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x00273E44 File Offset: 0x00272044
		[MoonSharpModuleMethod]
		public static DynValue replace(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "replace", DataType.Number, false));
			uint num2 = Bit32Module.ToUInt32(args.AsType(1, "replace", DataType.Number, false));
			DynValue dynValue = args.AsType(2, "replace", DataType.Number, false);
			DynValue dynValue2 = args.AsType(3, "replace", DataType.Number, true);
			int num3 = (int)dynValue.Number;
			int num4 = dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number);
			Bit32Module.ValidatePosWidth("replace", 3, num3, num4);
			uint num5 = Bit32Module.NBitMask(num4) << num3;
			uint num6 = num & ~num5;
			num2 &= num5;
			return DynValue.NewNumber(num6 | num2);
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x00273EDC File Offset: 0x002720DC
		private static void ValidatePosWidth(string func, int argPos, int pos, int width)
		{
			if (pos > 31 || pos + width > 31)
			{
				throw new ScriptRuntimeException("trying to access non-existent bits");
			}
			if (pos < 0)
			{
				throw new ScriptRuntimeException("bad argument #{1} to '{0}' (field cannot be negative)", new object[]
				{
					func,
					argPos
				});
			}
			if (width <= 0)
			{
				throw new ScriptRuntimeException("bad argument #{1} to '{0}' (width must be positive)", new object[]
				{
					func,
					argPos + 1
				});
			}
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x00273F48 File Offset: 0x00272148
		[MoonSharpModuleMethod]
		public static DynValue arshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			int num = Bit32Module.ToInt32(args.AsType(0, "arshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "arshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num <<= -num2;
			}
			else
			{
				num >>= num2;
			}
			return DynValue.NewNumber((double)num);
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x00273F9C File Offset: 0x0027219C
		[MoonSharpModuleMethod]
		public static DynValue rshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "rshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "rshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num <<= -num2;
			}
			else
			{
				num >>= num2;
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x00273FF0 File Offset: 0x002721F0
		[MoonSharpModuleMethod]
		public static DynValue lshift(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "lshift", DataType.Number, false));
			int num2 = (int)args.AsType(1, "lshift", DataType.Number, false).Number;
			if (num2 < 0)
			{
				num >>= -num2;
			}
			else
			{
				num <<= num2;
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x00274043 File Offset: 0x00272243
		[MoonSharpModuleMethod]
		public static DynValue band(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("band", args, (uint x, uint y) => x & y));
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x00274076 File Offset: 0x00272276
		[MoonSharpModuleMethod]
		public static DynValue btest(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewBoolean(Bit32Module.Bitwise("btest", args, (uint x, uint y) => x & y) > 0U);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x002740AA File Offset: 0x002722AA
		[MoonSharpModuleMethod]
		public static DynValue bor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bor", args, (uint x, uint y) => x | y));
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x002740DD File Offset: 0x002722DD
		[MoonSharpModuleMethod]
		public static DynValue bnot(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(~Bit32Module.ToUInt32(args.AsType(0, "bnot", DataType.Number, false)));
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x002740FA File Offset: 0x002722FA
		[MoonSharpModuleMethod]
		public static DynValue bxor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bxor", args, (uint x, uint y) => x ^ y));
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x00274130 File Offset: 0x00272330
		[MoonSharpModuleMethod]
		public static DynValue lrotate(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "lrotate", DataType.Number, false));
			int num2 = (int)args.AsType(1, "lrotate", DataType.Number, false).Number % 32;
			if (num2 < 0)
			{
				num = (num >> -num2 | num << 32 + num2);
			}
			else
			{
				num = (num << num2 | num >> 32 - num2);
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0027419C File Offset: 0x0027239C
		[MoonSharpModuleMethod]
		public static DynValue rrotate(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			uint num = Bit32Module.ToUInt32(args.AsType(0, "rrotate", DataType.Number, false));
			int num2 = (int)args.AsType(1, "rrotate", DataType.Number, false).Number % 32;
			if (num2 < 0)
			{
				num = (num << -num2 | num >> 32 + num2);
			}
			else
			{
				num = (num >> num2 | num << 32 - num2);
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x0400558D RID: 21901
		private static readonly uint[] MASKS = new uint[]
		{
			1U,
			3U,
			7U,
			15U,
			31U,
			63U,
			127U,
			255U,
			511U,
			1023U,
			2047U,
			4095U,
			8191U,
			16383U,
			32767U,
			65535U,
			131071U,
			262143U,
			524287U,
			1048575U,
			2097151U,
			4194303U,
			8388607U,
			16777215U,
			33554431U,
			67108863U,
			134217727U,
			268435455U,
			536870911U,
			1073741823U,
			2147483647U,
			uint.MaxValue
		};
	}
}
