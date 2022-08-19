using System;
using System.Text;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.REPL;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D76 RID: 3446
	[MoonSharpModule(Namespace = "debug")]
	public class DebugModule
	{
		// Token: 0x060061A8 RID: 25000 RVA: 0x002744C4 File Offset: 0x002726C4
		[MoonSharpModuleMethod]
		public static DynValue debug(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Script script = executionContext.GetScript();
			if (script.Options.DebugInput == null)
			{
				throw new ScriptRuntimeException("debug.debug not supported on this platform/configuration");
			}
			ReplInterpreter replInterpreter = new ReplInterpreter(script)
			{
				HandleDynamicExprs = false,
				HandleClassicExprsSyntax = true
			};
			for (;;)
			{
				string input = script.Options.DebugInput(replInterpreter.ClassicPrompt + " ");
				try
				{
					DynValue dynValue = replInterpreter.Evaluate(input);
					if (dynValue != null && dynValue.Type != DataType.Void)
					{
						script.Options.DebugPrint(string.Format("{0}", dynValue));
					}
				}
				catch (InterpreterException ex)
				{
					script.Options.DebugPrint(string.Format("{0}", ex.DecoratedMessage ?? ex.Message));
				}
				catch (Exception ex2)
				{
					script.Options.DebugPrint(string.Format("{0}", ex2.Message));
				}
			}
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x002745D0 File Offset: 0x002727D0
		[MoonSharpModuleMethod]
		public static DynValue getuservalue(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			if (dynValue.Type != DataType.UserData)
			{
				return DynValue.Nil;
			}
			return dynValue.UserData.UserValue ?? DynValue.Nil;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00274608 File Offset: 0x00272808
		[MoonSharpModuleMethod]
		public static DynValue setuservalue(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "setuservalue", DataType.UserData, false);
			DynValue userValue = args.AsType(0, "setuservalue", DataType.Table, true);
			return dynValue.UserData.UserValue = userValue;
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x00274640 File Offset: 0x00272840
		[MoonSharpModuleMethod]
		public static DynValue getregistry(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewTable(executionContext.GetScript().Registry);
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x00274654 File Offset: 0x00272854
		[MoonSharpModuleMethod]
		public static DynValue getmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			Script script = executionContext.GetScript();
			if (dynValue.Type.CanHaveTypeMetatables())
			{
				return DynValue.NewTable(script.GetTypeMetatable(dynValue.Type));
			}
			if (dynValue.Type == DataType.Table)
			{
				return DynValue.NewTable(dynValue.Table.MetaTable);
			}
			return DynValue.Nil;
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x002746B0 File Offset: 0x002728B0
		[MoonSharpModuleMethod]
		public static DynValue setmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue dynValue2 = args.AsType(1, "setmetatable", DataType.Table, true);
			Table table = dynValue2.IsNil() ? null : dynValue2.Table;
			Script script = executionContext.GetScript();
			if (dynValue.Type.CanHaveTypeMetatables())
			{
				script.SetTypeMetatable(dynValue.Type, table);
			}
			else
			{
				if (dynValue.Type != DataType.Table)
				{
					throw new ScriptRuntimeException("cannot debug.setmetatable on type {0}", new object[]
					{
						dynValue.Type.ToErrorTypeString()
					});
				}
				dynValue.Table.MetaTable = table;
			}
			return dynValue;
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x00274740 File Offset: 0x00272940
		[MoonSharpModuleMethod]
		public static DynValue getupvalue(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			int num = (int)args.AsType(1, "getupvalue", DataType.Number, false).Number - 1;
			if (args[0].Type == DataType.ClrFunction)
			{
				return DynValue.Nil;
			}
			ClosureContext closureContext = args.AsType(0, "getupvalue", DataType.Function, false).Function.ClosureContext;
			if (num < 0 || num >= closureContext.Count)
			{
				return DynValue.Nil;
			}
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewString(closureContext.Symbols[num]),
				closureContext[num]
			});
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x002747CC File Offset: 0x002729CC
		[MoonSharpModuleMethod]
		public static DynValue upvalueid(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			int num = (int)args.AsType(1, "getupvalue", DataType.Number, false).Number - 1;
			if (args[0].Type == DataType.ClrFunction)
			{
				return DynValue.Nil;
			}
			ClosureContext closureContext = args.AsType(0, "getupvalue", DataType.Function, false).Function.ClosureContext;
			if (num < 0 || num >= closureContext.Count)
			{
				return DynValue.Nil;
			}
			return DynValue.NewNumber((double)closureContext[num].ReferenceID);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x00274844 File Offset: 0x00272A44
		[MoonSharpModuleMethod]
		public static DynValue setupvalue(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			int num = (int)args.AsType(1, "setupvalue", DataType.Number, false).Number - 1;
			if (args[0].Type == DataType.ClrFunction)
			{
				return DynValue.Nil;
			}
			ClosureContext closureContext = args.AsType(0, "setupvalue", DataType.Function, false).Function.ClosureContext;
			if (num < 0 || num >= closureContext.Count)
			{
				return DynValue.Nil;
			}
			closureContext[num].Assign(args[2]);
			return DynValue.NewString(closureContext.Symbols[num]);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x002748CC File Offset: 0x00272ACC
		[MoonSharpModuleMethod]
		public static DynValue upvaluejoin(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "upvaluejoin", DataType.Function, false);
			DynValue dynValue2 = args.AsType(2, "upvaluejoin", DataType.Function, false);
			int num = args.AsInt(1, "upvaluejoin") - 1;
			int num2 = args.AsInt(3, "upvaluejoin") - 1;
			Closure function = dynValue.Function;
			Closure function2 = dynValue2.Function;
			if (num < 0 || num >= function.ClosureContext.Count)
			{
				throw ScriptRuntimeException.BadArgument(1, "upvaluejoin", "invalid upvalue index");
			}
			if (num2 < 0 || num2 >= function2.ClosureContext.Count)
			{
				throw ScriptRuntimeException.BadArgument(3, "upvaluejoin", "invalid upvalue index");
			}
			function2.ClosureContext[num2] = function.ClosureContext[num];
			return DynValue.Void;
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x00274988 File Offset: 0x00272B88
		[MoonSharpModuleMethod]
		public static DynValue traceback(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DynValue dynValue = args[0];
			DynValue dynValue2 = args[1];
			double num = 1.0;
			Coroutine coroutine = executionContext.GetCallingCoroutine();
			if (dynValue.Type == DataType.Thread)
			{
				coroutine = dynValue.Coroutine;
				dynValue = args[1];
				dynValue2 = args[2];
				num = 0.0;
			}
			if (dynValue.IsNotNil() && dynValue.Type != DataType.String && dynValue.Type != DataType.Number)
			{
				return dynValue;
			}
			string text = dynValue.CastToString();
			int val = (int)(dynValue2.CastToNumber() ?? num);
			WatchItem[] stackTrace = coroutine.GetStackTrace(Math.Max(0, val), null);
			if (text != null)
			{
				stringBuilder.AppendLine(text);
			}
			stringBuilder.AppendLine("stack traceback:");
			foreach (WatchItem watchItem in stackTrace)
			{
				string arg;
				if (watchItem.Name == null)
				{
					if (watchItem.RetAddress < 0)
					{
						arg = "main chunk";
					}
					else
					{
						arg = "?";
					}
				}
				else
				{
					arg = "function '" + watchItem.Name + "'";
				}
				string arg2 = (watchItem.Location != null) ? watchItem.Location.FormatLocation(executionContext.GetScript(), false) : "[clr]";
				stringBuilder.AppendFormat("\t{0}: in {1}\n", arg2, arg);
			}
			return DynValue.NewString(stringBuilder);
		}
	}
}
