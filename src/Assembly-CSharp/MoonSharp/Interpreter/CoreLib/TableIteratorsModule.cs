using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D81 RID: 3457
	[MoonSharpModule]
	public class TableIteratorsModule
	{
		// Token: 0x0600623A RID: 25146 RVA: 0x002773B0 File Offset: 0x002755B0
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

		// Token: 0x0600623B RID: 25147 RVA: 0x00277414 File Offset: 0x00275614
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

		// Token: 0x0600623C RID: 25148 RVA: 0x00277464 File Offset: 0x00275664
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

		// Token: 0x0600623D RID: 25149 RVA: 0x002774D8 File Offset: 0x002756D8
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
