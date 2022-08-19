using System;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7C RID: 3452
	[MoonSharpModule(Namespace = "math")]
	public class MathModule
	{
		// Token: 0x060061E6 RID: 25062 RVA: 0x002759D2 File Offset: 0x00273BD2
		private static Random GetRandom(Script s)
		{
			return (s.Registry.Get("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM").UserData.Object as AnonWrapper<Random>).Value;
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x002759F8 File Offset: 0x00273BF8
		private static void SetRandom(Script s, Random random)
		{
			DynValue value = UserData.Create(new AnonWrapper<Random>(random));
			s.Registry.Set("F61E3AA7247D4D1EB7A45430B0C8C9BB_MATH_RANDOM", value);
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x00275A22 File Offset: 0x00273C22
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			MathModule.SetRandom(globalTable.OwnerScript, new Random());
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x00275A34 File Offset: 0x00273C34
		private static DynValue exec1(CallbackArguments args, string funcName, Func<double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number));
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x00275A60 File Offset: 0x00273C60
		private static DynValue exec2(CallbackArguments args, string funcName, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, false);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.Number));
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x00275A9C File Offset: 0x00273C9C
		private static DynValue exec2n(CallbackArguments args, string funcName, double defVal, Func<double, double, double> func)
		{
			DynValue dynValue = args.AsType(0, funcName, DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, funcName, DataType.Number, true);
			return DynValue.NewNumber(func(dynValue.Number, dynValue2.IsNil() ? defVal : dynValue2.Number));
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x00275AE4 File Offset: 0x00273CE4
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

		// Token: 0x060061ED RID: 25069 RVA: 0x00275B54 File Offset: 0x00273D54
		[MoonSharpModuleMethod]
		public static DynValue abs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "abs", (double d) => Math.Abs(d));
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x00275B80 File Offset: 0x00273D80
		[MoonSharpModuleMethod]
		public static DynValue acos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "acos", (double d) => Math.Acos(d));
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x00275BAC File Offset: 0x00273DAC
		[MoonSharpModuleMethod]
		public static DynValue asin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "asin", (double d) => Math.Asin(d));
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x00275BD8 File Offset: 0x00273DD8
		[MoonSharpModuleMethod]
		public static DynValue atan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "atan", (double d) => Math.Atan(d));
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x00275C04 File Offset: 0x00273E04
		[MoonSharpModuleMethod]
		public static DynValue atan2(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "atan2", (double d1, double d2) => Math.Atan2(d1, d2));
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x00275C30 File Offset: 0x00273E30
		[MoonSharpModuleMethod]
		public static DynValue ceil(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "ceil", (double d) => Math.Ceiling(d));
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x00275C5C File Offset: 0x00273E5C
		[MoonSharpModuleMethod]
		public static DynValue cos(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cos", (double d) => Math.Cos(d));
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x00275C88 File Offset: 0x00273E88
		[MoonSharpModuleMethod]
		public static DynValue cosh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "cosh", (double d) => Math.Cosh(d));
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x00275CB4 File Offset: 0x00273EB4
		[MoonSharpModuleMethod]
		public static DynValue deg(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "deg", (double d) => d * 180.0 / 3.141592653589793);
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x00275CE0 File Offset: 0x00273EE0
		[MoonSharpModuleMethod]
		public static DynValue exp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "exp", (double d) => Math.Exp(d));
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x00275D0C File Offset: 0x00273F0C
		[MoonSharpModuleMethod]
		public static DynValue floor(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "floor", (double d) => Math.Floor(d));
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x00275D38 File Offset: 0x00273F38
		[MoonSharpModuleMethod]
		public static DynValue fmod(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "fmod", (double d1, double d2) => Math.IEEERemainder(d1, d2));
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x00275D64 File Offset: 0x00273F64
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

		// Token: 0x060061FA RID: 25082 RVA: 0x00275E56 File Offset: 0x00274056
		[MoonSharpModuleMethod]
		public static DynValue ldexp(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "ldexp", (double d1, double d2) => d1 * Math.Pow(2.0, d2));
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x00275E82 File Offset: 0x00274082
		[MoonSharpModuleMethod]
		public static DynValue log(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2n(args, "log", 2.718281828459045, (double d1, double d2) => Math.Log(d1, d2));
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x00275EB7 File Offset: 0x002740B7
		[MoonSharpModuleMethod]
		public static DynValue max(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "max", (double d1, double d2) => Math.Max(d1, d2));
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x00275EE3 File Offset: 0x002740E3
		[MoonSharpModuleMethod]
		public static DynValue min(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.execaccum(args, "min", (double d1, double d2) => Math.Min(d1, d2));
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x00275F10 File Offset: 0x00274110
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

		// Token: 0x060061FF RID: 25087 RVA: 0x00275F64 File Offset: 0x00274164
		[MoonSharpModuleMethod]
		public static DynValue pow(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec2(args, "pow", (double d1, double d2) => Math.Pow(d1, d2));
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x00275F90 File Offset: 0x00274190
		[MoonSharpModuleMethod]
		public static DynValue rad(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "rad", (double d) => d * 3.141592653589793 / 180.0);
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x00275FBC File Offset: 0x002741BC
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

		// Token: 0x06006202 RID: 25090 RVA: 0x00276054 File Offset: 0x00274254
		[MoonSharpModuleMethod]
		public static DynValue randomseed(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "randomseed", DataType.Number, false);
			MathModule.SetRandom(executionContext.GetScript(), new Random((int)dynValue.Number));
			return DynValue.Nil;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x0027608C File Offset: 0x0027428C
		[MoonSharpModuleMethod]
		public static DynValue sin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sin", (double d) => Math.Sin(d));
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x002760B8 File Offset: 0x002742B8
		[MoonSharpModuleMethod]
		public static DynValue sinh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sinh", (double d) => Math.Sinh(d));
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x002760E4 File Offset: 0x002742E4
		[MoonSharpModuleMethod]
		public static DynValue sqrt(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "sqrt", (double d) => Math.Sqrt(d));
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x00276110 File Offset: 0x00274310
		[MoonSharpModuleMethod]
		public static DynValue tan(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tan", (double d) => Math.Tan(d));
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x0027613C File Offset: 0x0027433C
		[MoonSharpModuleMethod]
		public static DynValue tanh(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return MathModule.exec1(args, "tanh", (double d) => Math.Tanh(d));
		}

		// Token: 0x0400558F RID: 21903
		[MoonSharpModuleConstant]
		public const double pi = 3.141592653589793;

		// Token: 0x04005590 RID: 21904
		[MoonSharpModuleConstant]
		public const double huge = 1.7976931348623157E+308;
	}
}
