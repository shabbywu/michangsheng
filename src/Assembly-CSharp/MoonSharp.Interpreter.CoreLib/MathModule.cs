using System;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter.CoreLib;

[MoonSharpModule(Namespace = "math")]
public class MathModule
{
	[MoonSharpModuleConstant]
	public const double pi = Math.PI;

	[MoonSharpModuleConstant]
	public const double huge = double.MaxValue;

	private static Random GetRandom(Script s)
	{
		return (s.Registry.Get("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM").UserData.Object as AnonWrapper<Random>).Value;
	}

	private static void SetRandom(Script s, Random random)
	{
		DynValue value = UserData.Create(new AnonWrapper<Random>(random));
		s.Registry.Set("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM", value);
	}

	public static void MoonSharpInit(Table globalTable, Table ioTable)
	{
		SetRandom(globalTable.OwnerScript, new Random());
	}

	private static DynValue exec1(CallbackArguments args, string funcName, Func<double, double> func)
	{
		DynValue dynValue = args.AsType(0, funcName, DataType.Number);
		return DynValue.NewNumber(func(dynValue.Number));
	}

	private static DynValue exec2(CallbackArguments args, string funcName, Func<double, double, double> func)
	{
		DynValue dynValue = args.AsType(0, funcName, DataType.Number);
		DynValue dynValue2 = args.AsType(1, funcName, DataType.Number);
		return DynValue.NewNumber(func(dynValue.Number, dynValue2.Number));
	}

	private static DynValue exec2n(CallbackArguments args, string funcName, double defVal, Func<double, double, double> func)
	{
		DynValue dynValue = args.AsType(0, funcName, DataType.Number);
		DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, allowNil: true);
		return DynValue.NewNumber(func(dynValue.Number, dynValue2.IsNil() ? defVal : dynValue2.Number));
	}

	private static DynValue execaccum(CallbackArguments args, string funcName, Func<double, double, double> func)
	{
		double num = double.NaN;
		if (args.Count == 0)
		{
			throw new ScriptRuntimeException("bad argument #1 to '{0}' (number expected, got no value)", funcName);
		}
		for (int i = 0; i < args.Count; i++)
		{
			DynValue dynValue = args.AsType(i, funcName, DataType.Number);
			num = ((i != 0) ? func(num, dynValue.Number) : dynValue.Number);
		}
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue abs(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "abs", (double d) => Math.Abs(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue acos(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "acos", (double d) => Math.Acos(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue asin(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "asin", (double d) => Math.Asin(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue atan(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "atan", (double d) => Math.Atan(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue atan2(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec2(args, "atan2", (double d1, double d2) => Math.Atan2(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue ceil(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "ceil", (double d) => Math.Ceiling(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue cos(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "cos", (double d) => Math.Cos(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue cosh(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "cosh", (double d) => Math.Cosh(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue deg(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "deg", (double d) => d * 180.0 / Math.PI);
	}

	[MoonSharpModuleMethod]
	public static DynValue exp(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "exp", (double d) => Math.Exp(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue floor(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "floor", (double d) => Math.Floor(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue fmod(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec2(args, "fmod", (double d1, double d2) => Math.IEEERemainder(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue frexp(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		long num = BitConverter.DoubleToInt64Bits(args.AsType(0, "frexp", DataType.Number).Number);
		bool flag = num < 0;
		int num2 = (int)((num >> 52) & 0x7FF);
		long num3 = num & 0xFFFFFFFFFFFFFL;
		if (num2 == 0)
		{
			num2++;
		}
		else
		{
			num3 |= 0x10000000000000L;
		}
		num2 -= 1075;
		if (num3 == 0L)
		{
			return DynValue.NewTuple(DynValue.NewNumber(0.0), DynValue.NewNumber(0.0));
		}
		while ((num3 & 1) == 0L)
		{
			num3 >>= 1;
			num2++;
		}
		double num4 = num3;
		double num5 = num2;
		while (num4 >= 1.0)
		{
			num4 /= 2.0;
			num5 += 1.0;
		}
		if (flag)
		{
			num4 = 0.0 - num4;
		}
		return DynValue.NewTuple(DynValue.NewNumber(num4), DynValue.NewNumber(num5));
	}

	[MoonSharpModuleMethod]
	public static DynValue ldexp(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec2(args, "ldexp", (double d1, double d2) => d1 * Math.Pow(2.0, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue log(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec2n(args, "log", Math.E, (double d1, double d2) => Math.Log(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue max(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return execaccum(args, "max", (double d1, double d2) => Math.Max(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue min(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return execaccum(args, "min", (double d1, double d2) => Math.Min(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue modf(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		DynValue dynValue = args.AsType(0, "modf", DataType.Number);
		return DynValue.NewTuple(DynValue.NewNumber(Math.Floor(dynValue.Number)), DynValue.NewNumber(dynValue.Number - Math.Floor(dynValue.Number)));
	}

	[MoonSharpModuleMethod]
	public static DynValue pow(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec2(args, "pow", (double d1, double d2) => Math.Pow(d1, d2));
	}

	[MoonSharpModuleMethod]
	public static DynValue rad(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "rad", (double d) => d * Math.PI / 180.0);
	}

	[MoonSharpModuleMethod]
	public static DynValue random(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		DynValue dynValue = args.AsType(0, "random", DataType.Number, allowNil: true);
		DynValue dynValue2 = args.AsType(1, "random", DataType.Number, allowNil: true);
		Random random = GetRandom(executionContext.GetScript());
		double num;
		if (dynValue.IsNil() && dynValue2.IsNil())
		{
			num = random.NextDouble();
		}
		else
		{
			int num2 = (dynValue2.IsNil() ? 1 : ((int)dynValue2.Number));
			int num3 = (int)dynValue.Number;
			num = ((num2 >= num3) ? ((double)random.Next(num3, num2 + 1)) : ((double)random.Next(num2, num3 + 1)));
		}
		return DynValue.NewNumber(num);
	}

	[MoonSharpModuleMethod]
	public static DynValue randomseed(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		DynValue dynValue = args.AsType(0, "randomseed", DataType.Number);
		SetRandom(executionContext.GetScript(), new Random((int)dynValue.Number));
		return DynValue.Nil;
	}

	[MoonSharpModuleMethod]
	public static DynValue sin(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "sin", (double d) => Math.Sin(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue sinh(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "sinh", (double d) => Math.Sinh(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue sqrt(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "sqrt", (double d) => Math.Sqrt(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue tan(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "tan", (double d) => Math.Tan(d));
	}

	[MoonSharpModuleMethod]
	public static DynValue tanh(ScriptExecutionContext executionContext, CallbackArguments args)
	{
		return exec1(args, "tanh", (double d) => Math.Tanh(d));
	}
}
