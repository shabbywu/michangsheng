using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001192 RID: 4498
	[MoonSharpModule]
	public class ErrorHandlingModule
	{
		// Token: 0x06006DBF RID: 28095 RVA: 0x0004ABBC File Offset: 0x00048DBC
		[MoonSharpModuleMethod]
		public static DynValue pcall(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.SetErrorHandlerStrategy("pcall", executionContext, args, null);
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x0029B3A8 File Offset: 0x002995A8
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

		// Token: 0x06006DC1 RID: 28097 RVA: 0x0029B5C4 File Offset: 0x002997C4
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

		// Token: 0x06006DC2 RID: 28098 RVA: 0x0004ABCB File Offset: 0x00048DCB
		public static DynValue pcall_continuation(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(true, args);
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x0004ABD4 File Offset: 0x00048DD4
		public static DynValue pcall_onerror(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ErrorHandlingModule.MakeReturnTuple(false, args);
		}

		// Token: 0x06006DC4 RID: 28100 RVA: 0x0029B60C File Offset: 0x0029980C
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
