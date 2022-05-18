using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200118C RID: 4492
	[MoonSharpModule(Namespace = "bit32")]
	public class Bit32Module
	{
		// Token: 0x06006D8D RID: 28045 RVA: 0x0004A9FC File Offset: 0x00048BFC
		private static uint ToUInt32(DynValue v)
		{
			return (uint)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x0004AA21 File Offset: 0x00048C21
		private static int ToInt32(DynValue v)
		{
			return (int)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x0004AA46 File Offset: 0x00048C46
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

		// Token: 0x06006D90 RID: 28048 RVA: 0x0029A688 File Offset: 0x00298888
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

		// Token: 0x06006D91 RID: 28049 RVA: 0x0029A6D0 File Offset: 0x002988D0
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

		// Token: 0x06006D92 RID: 28050 RVA: 0x0029A748 File Offset: 0x00298948
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

		// Token: 0x06006D93 RID: 28051 RVA: 0x0029A7E0 File Offset: 0x002989E0
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

		// Token: 0x06006D94 RID: 28052 RVA: 0x0029A84C File Offset: 0x00298A4C
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

		// Token: 0x06006D95 RID: 28053 RVA: 0x0029A8A0 File Offset: 0x00298AA0
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

		// Token: 0x06006D96 RID: 28054 RVA: 0x0029A8F4 File Offset: 0x00298AF4
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

		// Token: 0x06006D97 RID: 28055 RVA: 0x0004AA65 File Offset: 0x00048C65
		[MoonSharpModuleMethod]
		public static DynValue band(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("band", args, (uint x, uint y) => x & y));
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x0004AA98 File Offset: 0x00048C98
		[MoonSharpModuleMethod]
		public static DynValue btest(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewBoolean(Bit32Module.Bitwise("btest", args, (uint x, uint y) => x & y) > 0U);
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x0004AACC File Offset: 0x00048CCC
		[MoonSharpModuleMethod]
		public static DynValue bor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bor", args, (uint x, uint y) => x | y));
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0004AAFF File Offset: 0x00048CFF
		[MoonSharpModuleMethod]
		public static DynValue bnot(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(~Bit32Module.ToUInt32(args.AsType(0, "bnot", DataType.Number, false)));
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x0004AB1C File Offset: 0x00048D1C
		[MoonSharpModuleMethod]
		public static DynValue bxor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber(Bit32Module.Bitwise("bxor", args, (uint x, uint y) => x ^ y));
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x0029A948 File Offset: 0x00298B48
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

		// Token: 0x06006D9D RID: 28061 RVA: 0x0029A9B4 File Offset: 0x00298BB4
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

		// Token: 0x04006230 RID: 25136
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
