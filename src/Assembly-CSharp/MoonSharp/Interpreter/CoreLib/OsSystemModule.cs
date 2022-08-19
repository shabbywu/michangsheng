using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7E RID: 3454
	[MoonSharpModule(Namespace = "os")]
	public class OsSystemModule
	{
		// Token: 0x06006210 RID: 25104 RVA: 0x00276320 File Offset: 0x00274520
		[MoonSharpModuleMethod]
		public static DynValue execute(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "execute", DataType.String, true);
			if (dynValue.IsNil())
			{
				return DynValue.NewBoolean(true);
			}
			DynValue result;
			try
			{
				int num = Script.GlobalOptions.Platform.OS_Execute(dynValue.String);
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString("exit"),
					DynValue.NewNumber((double)num)
				});
			}
			catch (Exception)
			{
				result = DynValue.Nil;
			}
			return result;
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x002763AC File Offset: 0x002745AC
		[MoonSharpModuleMethod]
		public static DynValue exit(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "exit", DataType.Number, true);
			int exitCode = 0;
			if (dynValue.IsNotNil())
			{
				exitCode = (int)dynValue.Number;
			}
			Script.GlobalOptions.Platform.OS_ExitFast(exitCode);
			throw new InvalidOperationException("Unreachable code.. reached.");
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x002763F4 File Offset: 0x002745F4
		[MoonSharpModuleMethod]
		public static DynValue getenv(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "getenv", DataType.String, false);
			string environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable(dynValue.String);
			if (environmentVariable == null)
			{
				return DynValue.Nil;
			}
			return DynValue.NewString(environmentVariable);
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x00276438 File Offset: 0x00274638
		[MoonSharpModuleMethod]
		public static DynValue remove(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "remove", DataType.String, false).String;
			DynValue result;
			try
			{
				if (Script.GlobalOptions.Platform.OS_FileExists(@string))
				{
					Script.GlobalOptions.Platform.OS_FileDelete(@string);
					result = DynValue.True;
				}
				else
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", new object[]
						{
							@string
						}),
						DynValue.NewNumber(-1.0)
					});
				}
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message),
					DynValue.NewNumber(-1.0)
				});
			}
			return result;
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x00276508 File Offset: 0x00274708
		[MoonSharpModuleMethod]
		public static DynValue rename(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string @string = args.AsType(0, "rename", DataType.String, false).String;
			string string2 = args.AsType(1, "rename", DataType.String, false).String;
			DynValue result;
			try
			{
				if (!Script.GlobalOptions.Platform.OS_FileExists(@string))
				{
					result = DynValue.NewTuple(new DynValue[]
					{
						DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", new object[]
						{
							@string
						}),
						DynValue.NewNumber(-1.0)
					});
				}
				else
				{
					Script.GlobalOptions.Platform.OS_FileMove(@string, string2);
					result = DynValue.True;
				}
			}
			catch (Exception ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.Message),
					DynValue.NewNumber(-1.0)
				});
			}
			return result;
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x002765F0 File Offset: 0x002747F0
		[MoonSharpModuleMethod]
		public static DynValue setlocale(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString("n/a");
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x002765FC File Offset: 0x002747FC
		[MoonSharpModuleMethod]
		public static DynValue tmpname(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(Script.GlobalOptions.Platform.IO_OS_GetTempFilename());
		}
	}
}
