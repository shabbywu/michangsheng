using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D78 RID: 3448
	[MoonSharpModule]
	public class ErrorHandlingModule
	{
		// Token: 0x060061B8 RID: 25016 RVA: 0x00274BF4 File Offset: 0x00272DF4
		[MoonSharpModuleMethod]
		public static DynValue pcall(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.SetErrorHandlerStrategy("pcall", executionContext, args, null);
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x00274C04 File Offset: 0x00272E04
		private static DynValue SetErrorHandlerStrategy(string funcName, ScriptExecutionContext executionContext, CallbackArguments args, DynValue handlerBeforeUnwind)
		{
			DynValue function = args[0];
			DynValue[] array = new DynValue[args.Count - 1];
			for (int i = 1; i < args.Count; i++)
			{
				array[i - 1] = args[i];
			}
			if (args[0].Type == DataType.ClrFunction)
			{
				try
				{
					DynValue dynValue = args[0].Callback.Invoke(executionContext, array, false);
					if (dynValue.Type == DataType.TailCallRequest)
					{
						if (dynValue.TailCallData.Continuation != null || dynValue.TailCallData.ErrorHandler != null)
						{
							throw new ScriptRuntimeException("the function passed to {0} cannot be called directly by {0}. wrap in a script function instead.", new object[]
							{
								funcName
							});
						}
						return DynValue.NewTailCallReq(new TailCallData
						{
							Args = dynValue.TailCallData.Args,
							Function = dynValue.TailCallData.Function,
							Continuation = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_continuation), funcName),
							ErrorHandler = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_onerror), funcName),
							ErrorHandlerBeforeUnwind = handlerBeforeUnwind
						});
					}
					else
					{
						if (dynValue.Type == DataType.YieldRequest)
						{
							throw new ScriptRuntimeException("the function passed to {0} cannot be called directly by {0}. wrap in a script function instead.", new object[]
							{
								funcName
							});
						}
						return DynValue.NewTupleNested(new DynValue[]
						{
							DynValue.True,
							dynValue
						});
					}
				}
				catch (ScriptRuntimeException ex)
				{
					executionContext.PerformMessageDecorationBeforeUnwind(handlerBeforeUnwind, ex);
					return DynValue.NewTupleNested(new DynValue[]
					{
						DynValue.False,
						DynValue.NewString(ex.DecoratedMessage)
					});
				}
			}
			if (args[0].Type != DataType.Function)
			{
				return DynValue.NewTupleNested(new DynValue[]
				{
					DynValue.False,
					DynValue.NewString("attempt to " + funcName + " a non-function")
				});
			}
			return DynValue.NewTailCallReq(new TailCallData
			{
				Args = array,
				Function = function,
				Continuation = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_continuation), funcName),
				ErrorHandler = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(ErrorHandlingModule.pcall_onerror), funcName),
				ErrorHandlerBeforeUnwind = handlerBeforeUnwind
			});
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x00274E20 File Offset: 0x00273020
		private static DynValue MakeReturnTuple(bool retstatus, CallbackArguments args)
		{
			DynValue[] array = new DynValue[args.Count + 1];
			for (int i = 0; i < args.Count; i++)
			{
				array[i + 1] = args[i];
			}
			array[0] = DynValue.NewBoolean(retstatus);
			return DynValue.NewTuple(array);
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x00274E67 File Offset: 0x00273067
		public static DynValue pcall_continuation(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(true, args);
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x00274E70 File Offset: 0x00273070
		public static DynValue pcall_onerror(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(false, args);
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x00274E7C File Offset: 0x0027307C
		[MoonSharpModuleMethod]
		public static DynValue xpcall(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			List<DynValue> list = new List<DynValue>();
			for (int i = 0; i < args.Count; i++)
			{
				if (i != 1)
				{
					list.Add(args[i]);
				}
			}
			DynValue handlerBeforeUnwind = null;
			if (args[1].Type == DataType.Function || args[1].Type == DataType.ClrFunction)
			{
				handlerBeforeUnwind = args[1];
			}
			else if (args[1].Type != DataType.Nil)
			{
				args.AsType(1, "xpcall", DataType.Function, false);
			}
			return ErrorHandlingModule.SetErrorHandlerStrategy("xpcall", executionContext, new CallbackArguments(list, false), handlerBeforeUnwind);
		}
	}
}
