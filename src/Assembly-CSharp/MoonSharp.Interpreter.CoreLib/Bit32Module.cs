using System;

namespace MoonSharp.Interpreter.CoreLib;

[MoonSharpModule(Namespace = "bit32")]
public class Bit32Module
{
	private static readonly uint[] MASKS = new uint[32]
	{
		1u, 3u, 7u, 15u, 31u, 63u, 127u, 255u, 511u, 1023u,
		2047u, 4095u, 8191u, 16383u, 32767u, 65535u, 131071u, 262143u, 524287u, 1048575u,
		2097151u, 4194303u, 8388607u, 16777215u, 33554431u, 67108863u, 134217727u, 268435455u, 536870911u, 1073741823u,
		2147483647u, 4294967295u
	};

	private static uint ToUInt32(DynValue v)
	{
		return (uint)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
	}

	private static int ToInt32(DynValue v)
	{
		return (int)Math.IEEERemainder(v.Number, Math.Pow(2.0, 32.0));
	}

	private static uint NBitMask(int bits)
	{
		if (bits <= 0)
		{
			return 0u;
		}
		if (bits >= 32)
		{
			return MASKS[31];
		}
		return MASKS[bits - 1];
	}

	public static uint Bitwise(string funcName, CallbackArguments args, Func<uint, uint, uint> accumFunc)
	{
		uint num = ToUInt32(args.AsType(0, funcName, DataType.Number));
		for (int i = 1; i < args.Count; i++)
		{
			uint arg = ToUInt32(args.AsType(i, funcName, DataType.Number));
			num = accumFunc(num, arg);
		}
		return num;
	}

	[MoonSharpModuleMethod]
	public static DynValue extract(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "extract", DataType.Number));
		DynValue dynValue = args.AsType(1, "extract", DataType.Number);
		DynValue dynValue2 = args.AsType(2, "extract", DataType.Number, allowNil: true);
		int num2 = (int)dynValue.Number;
		int num3 = (dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number));
		ValidatePosWidth("extract", 2, num2, num3);
		return DynValue.NewNumber((num >> num2) & NBitMask(num3));
	}

	[MoonSharpModuleMethod]
	public static DynValue replace(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "replace", DataType.Number));
		uint num2 = ToUInt32(args.AsType(1, "replace", DataType.Number));
		DynValue dynValue = args.AsType(2, "replace", DataType.Number);
		DynValue dynValue2 = args.AsType(3, "replace", DataType.Number, allowNil: true);
		int num3 = (int)dynValue.Number;
		int num4 = (dynValue2.IsNilOrNan() ? 1 : ((int)dynValue2.Number));
		ValidatePosWidth("replace", 3, num3, num4);
		uint num5 = NBitMask(num4) << num3;
		uint num6 = num & ~num5;
		num2 &= num5;
		return DynValue.NewNumber(num6 | num2);
	}

	private static void ValidatePosWidth(string func, int argPos, int pos, int width)
	{
		if (pos > 31 || pos + width > 31)
		{
			throw new ScriptRuntimeException("trying to access non-existent bits");
		}
		if (pos < 0)
		{
			throw new ScriptRuntimeException("bad argument #{1} to '{0}' (field cannot be negative)", func, argPos);
		}
		if (width <= 0)
		{
			throw new ScriptRuntimeException("bad argument #{1} to '{0}' (width must be positive)", func, argPos + 1);
		}
	}

	[MoonSharpModuleMethod]
	public static DynValue arshift(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		int num = ToInt32(args.AsType(0, "arshift", DataType.Number));
		int num2 = (int)args.AsType(1, "arshift", DataType.Number).Number;
		num = ((num2 >= 0) ? (num >> num2) : (num << -num2));
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue rshift(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "rshift", DataType.Number));
		int num2 = (int)args.AsType(1, "rshift", DataType.Number).Number;
		num = ((num2 >= 0) ? (num >> num2) : (num << -num2));
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue lshift(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "lshift", DataType.Number));
		int num2 = (int)args.AsType(1, "lshift", DataType.Number).Number;
		num = ((num2 >= 0) ? (num << num2) : (num >> -num2));
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue band(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return DynValue.NewNumber(Bitwise("band", args, (uint x, uint y) => x & y));
	}

	[MoonSharpModuleMethod]
	public static DynValue btest(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return DynValue.NewBoolean(Bitwise("btest", args, (uint x, uint y) => x & y) != 0);
	}

	[MoonSharpModuleMethod]
	public static DynValue bor(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return DynValue.NewNumber(Bitwise("bor", args, (uint x, uint y) => x | y));
	}

	[MoonSharpModuleMethod]
	public static DynValue bnot(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return DynValue.NewNumber(~ToUInt32(args.AsType(0, "bnot", DataType.Number)));
	}

	[MoonSharpModuleMethod]
	public static DynValue bxor(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return DynValue.NewNumber(Bitwise("bxor", args, (uint x, uint y) => x ^ y));
	}

	[MoonSharpModuleMethod]
	public static DynValue lrotate(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "lrotate", DataType.Number));
		int num2 = (int)args.AsType(1, "lrotate", DataType.Number).Number % 32;
		num = ((num2 >= 0) ? ((num << num2) | (num >> 32 - num2)) : ((num >> -num2) | (num << 32 + num2)));
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue rrotate(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		uint num = ToUInt32(args.AsType(0, "rrotate", DataType.Number));
		int num2 = (int)args.AsType(1, "rrotate", DataType.Number).Number % 32;
		num = ((num2 >= 0) ? ((num >> num2) | (num << 32 - num2)) : ((num << -num2) | (num >> 32 + num2)));
		return DynValue.NewNumber(num);
	}
}
