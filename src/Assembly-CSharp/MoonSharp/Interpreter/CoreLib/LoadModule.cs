using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001196 RID: 4502
	[MoonSharpModule]
	public class LoadModule
	{
		// Token: 0x06006DE5 RID: 28133 RVA: 0x0029BD00 File Offset: 0x00299F00
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

		// Token: 0x06006DE6 RID: 28134 RVA: 0x0004AD1B File Offset: 0x00048F1B
		[MoonSharpModuleMethod]
		public static DynValue load(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, null);
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x0004AD25 File Offset: 0x00048F25
		[MoonSharpModuleMethod]
		public static DynValue loadsafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.load_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x0029BD80 File Offset: 0x00299F80
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

		// Token: 0x06006DE9 RID: 28137 RVA: 0x0004AD34 File Offset: 0x00048F34
		[MoonSharpModuleMethod]
		public static DynValue loadfile(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, null);
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x0004AD3E File Offset: 0x00048F3E
		[MoonSharpModuleMethod]
		public static DynValue loadfilesafe(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return LoadModule.loadfile_impl(executionContext, args, LoadModule.GetSafeDefaultEnv(executionContext));
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x0029BECC File Offset: 0x0029A0CC
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

		// Token: 0x06006DEC RID: 28140 RVA: 0x0004AD4D File Offset: 0x00048F4D
		private static Table GetSafeDefaultEnv(ScriptExecutionContext executionContext)
		{
			Table currentGlobalEnv = executionContext.CurrentGlobalEnv;
			if (currentGlobalEnv == null)
			{
				throw new ScriptRuntimeException("current environment cannot be backtracked.");
			}
			return currentGlobalEnv;
		}

		// Token: 0x06006DED RID: 28141 RVA: 0x0029BF60 File Offset: 0x0029A160
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

		// Token: 0x06006DEE RID: 28142 RVA: 0x0029BFB4 File Offset: 0x0029A1B4
		[MoonSharpModuleMethod]
		public static DynValue __require_clr_impl(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Script script = executionContext.GetScript();
			DynValue dynValue = args.AsType(0, "__require_clr_impl", DataType.String, false);
			return script.RequireModule(dynValue.String, null);
		}

		// Token: 0x04006239 RID: 25145
		[MoonSharpModuleMethod]
		public const string require = "\r\nfunction(modulename)\r\n\tif (package == nil) then package = { }; end\r\n\tif (package.loaded == nil) then package.loaded = { }; end\r\n\r\n\tlocal m = package.loaded[modulename];\r\n\r\n\tif (m ~= nil) then\r\n\t\treturn m;\r\n\tend\r\n\r\n\tlocal func = __require_clr_impl(modulename);\r\n\r\n\tlocal res = func(modulename);\r\n\r\n\tif (res == nil) then\r\n\t\tres = true;\r\n\tend\r\n\r\n\tpackage.loaded[modulename] = res;\r\n\r\n\treturn res;\r\nend";
	}
}
