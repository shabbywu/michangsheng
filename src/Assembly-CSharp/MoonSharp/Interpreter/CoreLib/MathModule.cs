using System;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001197 RID: 4503
	[MoonSharpModule(Namespace = "math")]
	public class MathModule
	{
		// Token: 0x06006DF0 RID: 28144 RVA: 0x0004AD63 File Offset: 0x00048F63
		private static Random GetRandom(Script s)
		{
			return (s.Registry.Get("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM").UserData.Object as AnonWrapper<Random>).Value;
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x0029BFE4 File Offset: 0x0029A1E4
		private static void SetRandom(Script s, Random random)
		{
			DynValue value = UserData.Create(new AnonWrapper<Random>(random));
			s.Registry.Set("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM", value);
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x0004AD89 File Offset: 0x00048F89
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			MathModule.SetRandom(globalTable.OwnerScript, new Random());
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x0029C010 File Offset: 0x0029A210
		private static DynValue exec1(CallbackArguments args, string funcName, Func<double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number));
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x0029C03C File Offset: 0x0029A23C
		private static DynValue exec2(CallbackArguments args, string funcName, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.Number));
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x0029C078 File Offset: 0x0029A278
		private static DynValue exec2n(CallbackArguments args, string funcName, double defVal, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, true);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.IsNil() ? defVal : dynValue2.Number));
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x0029C0C0 File Offset: 0x0029A2C0
		private static DynValue execaccum(CallbackArguments args, string funcName, Func<double, double, double> func)
		{
			double num = double.NaN;
			if (args.Count == 0)
			{
				throw new ScriptRuntimeException("bad argument #1 to '{0}' (number expected, got no value)", new object[]
				{
					funcName
				});
			}
			for (int i = 0; i < args.Count; i++)
			{
				DynValue dynValue = args.AsType(i, funcName, DataType.Number, false);
				if (i == 0)
				{
					num = dynValue.Number;
				}
				else
				{
					num = func(num, dynValue.Number);
				}
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x0004AD9B File Offset: 0x00048F9B
		[MoonSharpModuleMethod]
		public static DynValue abs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "abs", (double d) => Math.Abs(d));
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x0004ADC7 File Offset: 0x00048FC7
		[MoonSharpModuleMethod]
		public static DynValue acos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "acos", (double d) => Math.Acos(d));
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x0004ADF3 File Offset: 0x00048FF3
		[MoonSharpModuleMethod]
		public static DynValue asin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "asin", (double d) => Math.Asin(d));
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x0004AE1F File Offset: 0x0004901F
		[MoonSharpModuleMethod]
		public static DynValue atan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "atan", (double d) => Math.Atan(d));
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x0004AE4B File Offset: 0x0004904B
		[MoonSharpModuleMethod]
		public static DynValue atan2(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "atan2", (double d1, double d2) => Math.Atan2(d1, d2));
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x0004AE77 File Offset: 0x00049077
		[MoonSharpModuleMethod]
		public static DynValue ceil(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "ceil", (double d) => Math.Ceiling(d));
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x0004AEA3 File Offset: 0x000490A3
		[MoonSharpModuleMethod]
		public static DynValue cos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cos", (double d) => Math.Cos(d));
		}

		// Token: 0x06006DFE RID: 28158 RVA: 0x0004AECF File Offset: 0x000490CF
		[MoonSharpModuleMethod]
		public static DynValue cosh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cosh", (double d) => Math.Cosh(d));
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x0004AEFB File Offset: 0x000490FB
		[MoonSharpModuleMethod]
		public static DynValue deg(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "deg", (double d) => d * 180.0 / 3.141592653589793);
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x0004AF27 File Offset: 0x00049127
		[MoonSharpModuleMethod]
		public static DynValue exp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "exp", (double d) => Math.Exp(d));
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x0004AF53 File Offset: 0x00049153
		[MoonSharpModuleMethod]
		public static DynValue floor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "floor", (double d) => Math.Floor(d));
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x0004AF7F File Offset: 0x0004917F
		[MoonSharpModuleMethod]
		public static DynValue fmod(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "fmod", (double d1, double d2) => Math.IEEERemainder(d1, d2));
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x0029C130 File Offset: 0x0029A330
		[MoonSharpModuleMethod]
		public static DynValue frexp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			long num = BitConverter.DoubleToInt64Bits(args.AsType(0, "frexp", DataType.Number, false).Number);
			bool flag = num < 0L;
			int num2 = (int)(num >> 52 & 2047L);
			long num3 = num & 4503599627370495L;
			if (num2 == 0)
			{
				num2++;
			}
			else
			{
				num3 |= 4503599627370496L;
			}
			num2 -= 1075;
			if (num3 == 0L)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewNumber(0.0),
					DynValue.NewNumber(0.0)
				});
			}
			while ((num3 & 1L) == 0L)
			{
				num3 >>= 1;
				num2++;
			}
			double num4 = (double)num3;
			double num5 = (double)num2;
			while (num4 >= 1.0)
			{
				num4 /= 2.0;
				num5 += 1.0;
			}
			if (flag)
			{
				num4 = -num4;
			}
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewNumber(num4),
				DynValue.NewNumber(num5)
			});
		}

		// Token: 0x06006E04 RID: 28164 RVA: 0x0004AFAB File Offset: 0x000491AB
		[MoonSharpModuleMethod]
		public static DynValue ldexp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "ldexp", (double d1, double d2) => d1 * Math.Pow(2.0, d2));
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x0004AFD7 File Offset: 0x000491D7
		[MoonSharpModuleMethod]
		public static DynValue log(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2n(args, "log", 2.718281828459045, (double d1, double d2) => Math.Log(d1, d2));
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x0004B00C File Offset: 0x0004920C
		[MoonSharpModuleMethod]
		public static DynValue max(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "max", (double d1, double d2) => Math.Max(d1, d2));
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x0004B038 File Offset: 0x00049238
		[MoonSharpModuleMethod]
		public static DynValue min(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "min", (double d1, double d2) => Math.Min(d1, d2));
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x0029C224 File Offset: 0x0029A424
		[MoonSharpModuleMethod]
		public static DynValue modf(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "modf", DataType.Number, false);
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewNumber(Math.Floor(dynValue.Number)),
				DynValue.NewNumber(dynValue.Number - Math.Floor(dynValue.Number))
			});
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x0004B064 File Offset: 0x00049264
		[MoonSharpModuleMethod]
		public static DynValue pow(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "pow", (double d1, double d2) => Math.Pow(d1, d2));
		}

		// Token: 0x06006E0A RID: 28170 RVA: 0x0004B090 File Offset: 0x00049290
		[MoonSharpModuleMethod]
		public static DynValue rad(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "rad", (double d) => d * 3.141592653589793 / 180.0);
		}

		// Token: 0x06006E0B RID: 28171 RVA: 0x0029C278 File Offset: 0x0029A478
		[MoonSharpModuleMethod]
		public static DynValue random(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "random", DataType.Number, true);
			DynValue dynValue2 = args.AsType(1, "random", DataType.Number, true);
			Random random = MathModule.GetRandom(executionContext.GetScript());
			double num;
			if (dynValue.IsNil() && dynValue2.IsNil())
			{
				num = random.NextDouble();
			}
			else
			{
				int num2 = dynValue2.IsNil() ? 1 : ((int)dynValue2.Number);
				int num3 = (int)dynValue.Number;
				if (num2 < num3)
				{
					num = (double)random.Next(num2, num3 + 1);
				}
				else
				{
					num = (double)random.Next(num3, num2 + 1);
				}
			}
			return DynValue.NewNumber(num);
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x0029C310 File Offset: 0x0029A510
		[MoonSharpModuleMethod]
		public static DynValue randomseed(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "randomseed", DataType.Number, false);
			MathModule.SetRandom(executionContext.GetScript(), new Random((int)dynValue.Number));
			return DynValue.Nil;
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x0004B0BC File Offset: 0x000492BC
		[MoonSharpModuleMethod]
		public static DynValue sin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sin", (double d) => Math.Sin(d));
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x0004B0E8 File Offset: 0x000492E8
		[MoonSharpModuleMethod]
		public static DynValue sinh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sinh", (double d) => Math.Sinh(d));
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x0004B114 File Offset: 0x00049314
		[MoonSharpModuleMethod]
		public static DynValue sqrt(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sqrt", (double d) => Math.Sqrt(d));
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x0004B140 File Offset: 0x00049340
		[MoonSharpModuleMethod]
		public static DynValue tan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tan", (double d) => Math.Tan(d));
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x0004B16C File Offset: 0x0004936C
		[MoonSharpModuleMethod]
		public static DynValue tanh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tanh", (double d) => Math.Tanh(d));
		}

		// Token: 0x0400623A RID: 25146
		[MoonSharpModuleConstant]
		public const double pi = 3.141592653589793;

		// Token: 0x0400623B RID: 25147
		[MoonSharpModuleConstant]
		public const double huge = 1.7976931348623157E+308;
	}
}
