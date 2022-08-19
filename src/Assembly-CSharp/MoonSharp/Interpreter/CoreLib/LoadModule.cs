using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7B RID: 3451
	[MoonSharpModule]
	public class LoadModule
	{
		// Token: 0x060061DB RID: 25051 RVA: 0x002756A4 File Offset: 0x002738A4
		public static void MoonSharpInit(Table globalTable, Table ioTable)
		{
			DynValue dynValue = globalTable.Get("package");
			if (dynValue.IsNil())
			{
				dynValue = DynValue.NewTable(globalTable.OwnerScript);
				globalTable["package"] = dynValue;
			}
			else if (dynValue.Type != DataType.Table)
			{
				throw new InternalErrorException("'package' global variable was found and it is not a table");
			}
			string str = Path.DirectorySeparatorChar.ToString() + "\n;\n?\n!\n-\n";
			dynValue.Table.Set("config", DynValue.NewString(str));
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x00275721 File Offset: 0x00273921
		[MoonSharpModuleMethod]
		public static DynValue load(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, null);
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0027572B File Offset: 0x0027392B
		[MoonSharpModuleMethod]
		public static DynValue loadsafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x0027573C File Offset: 0x0027393C
		public static DynValue load_impl(ScriptExecutionContext executionContext, CallbackArguments args, Table defaultEnv)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args[0];
				string text = "";
				if (dynValue.Type == DataType.Function)
				{
					DynValue dynValue2;
					for (;;)
					{
						dynValue2 = executionContext.GetScript().Call(dynValue);
						if (dynValue2.Type != DataType.String || dynValue2.String.Length <= 0)
						{
							break;
						}
						text += dynValue2.String;
					}
					if (!dynValue2.IsNil())
					{
						return DynValue.NewTuple(new DynValue[]
						{
							DynValue.Nil,
							DynValue.NewString("reader function must return a string")
						});
					}
				}
				else if (dynValue.Type == DataType.String)
				{
					text = dynValue.String;
				}
				else
				{
					args.AsType(0, "load", DataType.Function, false);
				}
				DynValue dynValue3 = args.AsType(1, "load", DataType.String, true);
				DynValue dynValue4 = args.AsType(3, "load", DataType.Table, true);
				result = script.LoadString(text, (!dynValue4.IsNil()) ? dynValue4.Table : defaultEnv, (!dynValue3.IsNil()) ? dynValue3.String : "=(load)");
			}
			catch (SyntaxErrorException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.DecoratedMessage ?? ex.Message)
				});
			}
			return result;
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00275888 File Offset: 0x00273A88
		[MoonSharpModuleMethod]
		public static DynValue loadfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, null);
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x00275892 File Offset: 0x00273A92
		[MoonSharpModuleMethod]
		public static DynValue loadfilesafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x002758A4 File Offset: 0x00273AA4
		private static DynValue loadfile_impl(ScriptExecutionContext executionContext, CallbackArguments args, Table defaultEnv)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args.AsType(0, "loadfile", DataType.String, false);
				DynValue dynValue2 = args.AsType(2, "loadfile", DataType.Table, true);
				result = script.LoadFile(dynValue.String, dynValue2.IsNil() ? defaultEnv : dynValue2.Table, null);
			}
			catch (SyntaxErrorException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.Nil,
					DynValue.NewString(ex.DecoratedMessage ?? ex.Message)
				});
			}
			return result;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x00275938 File Offset: 0x00273B38
		private static Table GetSafeDefaultEnv(ScriptExecutionContext executionContext)
		{
			Table currentGlobalEnv = executionContext.CurrentGlobalEnv;
			if (currentGlobalEnv == null)
			{
				throw new ScriptRuntimeException("current environment cannot be backtracked.");
			}
			return currentGlobalEnv;
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x00275950 File Offset: 0x00273B50
		[MoonSharpModuleMethod]
		public static DynValue dofile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				Script script = executionContext.GetScript();
				DynValue dynValue = args.AsType(0, "dofile", DataType.String, false);
				result = DynValue.NewTailCallReq(script.LoadFile(dynValue.String, null, null), Array.Empty<DynValue>());
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x002759A4 File Offset: 0x00273BA4
		[MoonSharpModuleMethod]
		public static DynValue __require_clr_impl(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Script script = executionContext.GetScript();
			DynValue dynValue = args.AsType(0, "__require_clr_impl", DataType.String, false);
			return script.RequireModule(dynValue.String, null);
		}

		// Token: 0x0400558E RID: 21902
		[MoonSharpModuleMethod]
		public const string require = "\r\nfunction(modulename)\r\n\tif (package == nil) then package = { }; end\r\n\tif (package.loaded == nil) then package.loaded = { }; end\r\n\r\n\tlocal m = package.loaded[modulename];\r\n\r\n\tif (m ~= nil) then\r\n\t\treturn m;\r\n\tend\r\n\r\n\tlocal func = __require_clr_impl(modulename);\r\n\r\n\tlocal res = func(modulename);\r\n\r\n\tif (res == nil) then\r\n\t\tres = true;\r\n\tend\r\n\r\n\tpackage.loaded[modulename] = res;\r\n\r\n\treturn res;\r\nend";
	}
}
