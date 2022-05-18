using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200119E RID: 4510
	[MoonSharpModule]
	public class TableIteratorsModule
	{
		// Token: 0x06006E61 RID: 28257 RVA: 0x0029D444 File Offset: 0x0029B644
		[MoonSharpModuleMethod]
		public static DynValue ipairs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue result;
			if ((result = executionContext.GetMetamethodTailCall(dynValue, "__ipairs", args.GetArray(0))) == null)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(TableIteratorsModule.__next_i), null),
					dynValue,
					DynValue.NewNumber(0.0)
				});
			}
			return result;
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x0029D4A8 File Offset: 0x0029B6A8
		[MoonSharpModuleMethod]
		public static DynValue pairs(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			DynValue result;
			if ((result = executionContext.GetMetamethodTailCall(dynValue, "__pairs", args.GetArray(0))) == null)
			{
				result = DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewCallback(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(TableIteratorsModule.next), null),
					dynValue
				});
			}
			return result;
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x0029D4F8 File Offset: 0x0029B6F8
		[MoonSharpModuleMethod]
		public static DynValue next(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "next", DataType.Table, false);
			DynValue v = args[1];
			TablePair? tablePair = dynValue.Table.NextKey(v);
			if (tablePair != null)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					tablePair.Value.Key,
					tablePair.Value.Value
				});
			}
			throw new ScriptRuntimeException("invalid key to 'next'");
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x0029D56C File Offset: 0x0029B76C
		public static DynValue __next_i(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "!!next_i!!", DataType.Table, false);
			int num = (int)args.AsType(1, "!!next_i!!", DataType.Number, false).Number + 1;
			DynValue dynValue2 = dynValue.Table.Get(num);
			if (dynValue2.Type != DataType.Nil)
			{
				return DynValue.NewTuple(new DynValue[]
				{
					DynValue.NewNumber((double)num),
					dynValue2
				});
			}
			return DynValue.Nil;
		}
	}
}
