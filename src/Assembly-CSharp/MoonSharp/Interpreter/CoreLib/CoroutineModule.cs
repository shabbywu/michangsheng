using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200118E RID: 4494
	[MoonSharpModule(Namespace = "coroutine")]
	public class CoroutineModule
	{
		// Token: 0x06006DA6 RID: 28070 RVA: 0x0029AA20 File Offset: 0x00298C20
		[MoonSharpModuleMethod]
		public static DynValue create(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.Function && args[0].Type != DataType.ClrFunction)
			{
				args.AsType(0, "create", DataType.Function, false);
			}
			return executionContext.GetScript().CreateCoroutine(args[0]);
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x0029AA70 File Offset: 0x00298C70
		[MoonSharpModuleMethod]
		public static DynValue wrap(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.Function && args[0].Type != DataType.ClrFunction)
			{
				args.AsType(0, "wrap", DataType.Function, false);
			}
			DynValue additionalData = CoroutineModule.create(executionContext, args);
			DynValue dynValue = DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(CoroutineModule.__wrap_wrapper), null);
			dynValue.Callback.AdditionalData = additionalData;
			return dynValue;
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x0004AB74 File Offset: 0x00048D74
		public static DynValue __wrap_wrapper(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return ((DynValue)executionContext.AdditionalData).Coroutine.Resume(args.GetArray(0));
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x0029AAD4 File Offset: 0x00298CD4
		[MoonSharpModuleMethod]
		public static DynValue resume(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "resume", DataType.Thread, false);
			DynValue result;
			try
			{
				DynValue dynValue2 = dynValue.Coroutine.Resume(args.GetArray(1));
				List<DynValue> list = new List<DynValue>();
				list.Add(DynValue.True);
				if (dynValue2.Type == DataType.Tuple)
				{
					for (int i = 0; i < dynValue2.Tuple.Length; i++)
					{
						DynValue dynValue3 = dynValue2.Tuple[i];
						if (i == dynValue2.Tuple.Length - 1 && dynValue3.Type == DataType.Tuple)
						{
							list.AddRange(dynValue3.Tuple);
						}
						else
						{
							list.Add(dynValue3);
						}
					}
				}
				else
				{
					list.Add(dynValue2);
				}
				result = DynValue.NewTuple(list.ToArray());
			}
			catch (ScriptRuntimeException ex)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.False,
					DynValue.NewString(ex.Message)
				});
			}
			return result;
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x0004AB92 File Offset: 0x00048D92
		[MoonSharpModuleMethod]
		public static DynValue yield(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewYieldReq(args.GetArray(0));
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x0029ABBC File Offset: 0x00298DBC
		[MoonSharpModuleMethod]
		public static DynValue running(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Coroutine callingCoroutine = executionContext.GetCallingCoroutine();
			return DynValue.NewTuple(new DynValue[]
			{
				DynValue.NewCoroutine(callingCoroutine),
				DynValue.NewBoolean(callingCoroutine.State == CoroutineState.Main)
			});
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x0029ABF8 File Offset: 0x00298DF8
		[MoonSharpModuleMethod]
		public static DynValue status(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "status", DataType.Thread, false);
			Coroutine callingCoroutine = executionContext.GetCallingCoroutine();
			CoroutineState state = dynValue.Coroutine.State;
			switch (state)
			{
			case CoroutineState.Main:
			case CoroutineState.Running:
				if (dynValue.Coroutine != callingCoroutine)
				{
					return DynValue.NewString("normal");
				}
				return DynValue.NewString("running");
			case CoroutineState.NotStarted:
			case CoroutineState.Suspended:
			case CoroutineState.ForceSuspended:
				return DynValue.NewString("suspended");
			case CoroutineState.Dead:
				return DynValue.NewString("dead");
			default:
				throw new InternalErrorException("Unexpected coroutine state {0}", new object[]
				{
					state
				});
			}
		}
	}
}
