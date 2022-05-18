using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200119A RID: 4506
	[MoonSharpModule(Namespace = "os")]
	public class OsSystemModule
	{
		// Token: 0x06006E33 RID: 28211 RVA: 0x0029C500 File Offset: 0x0029A700
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

		// Token: 0x06006E34 RID: 28212 RVA: 0x0029C58C File Offset: 0x0029A78C
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

		// Token: 0x06006E35 RID: 28213 RVA: 0x0029C5D4 File Offset: 0x0029A7D4
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

		// Token: 0x06006E36 RID: 28214 RVA: 0x0029C618 File Offset: 0x0029A818
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

		// Token: 0x06006E37 RID: 28215 RVA: 0x0029C6E8 File Offset: 0x0029A8E8
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

		// Token: 0x06006E38 RID: 28216 RVA: 0x0004B28B File Offset: 0x0004948B
		[MoonSharpModuleMethod]
		public static DynValue setlocale(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString("n/a");
		}

		// Token: 0x06006E39 RID: 28217 RVA: 0x0004B297 File Offset: 0x00049497
		[MoonSharpModuleMethod]
		public static DynValue tmpname(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(Script.GlobalOptions.Platform.IO_OS_GetTempFilename());
		}
	}
}
