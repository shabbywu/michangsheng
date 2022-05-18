using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001199 RID: 4505
	[MoonSharpModule]
	public class MetaTableModule
	{
		// Token: 0x06006E2C RID: 28204 RVA: 0x0029C348 File Offset: 0x0029A548
		[MoonSharpModuleMethod]
		public static DynValue setmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "setmetatable", DataType.Table, false);
			DynValue dynValue2 = args.AsType(1, "setmetatable", DataType.Table, true);
			if (executionContext.GetMetamethod(dynValue, "__metatable") != null)
			{
				throw new ScriptRuntimeException("cannot change a protected metatable");
			}
			dynValue.Table.MetaTable = dynValue2.Table;
			return dynValue;
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x0029C3A0 File Offset: 0x0029A5A0
		[MoonSharpModuleMethod]
		public static DynValue getmetatable(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			Table table = null;
			if (dynValue.Type.CanHaveTypeMetatables())
			{
				table = executionContext.GetScript().GetTypeMetatable(dynValue.Type);
			}
			if (dynValue.Type == DataType.Table)
			{
				table = dynValue.Table.MetaTable;
			}
			if (table == null)
			{
				return DynValue.Nil;
			}
			if (table.RawGet("__metatable") != null)
			{
				return table.Get("__metatable");
			}
			return DynValue.NewTable(table);
		}

		// Token: 0x06006E2E RID: 28206 RVA: 0x0029C414 File Offset: 0x0029A614
		[MoonSharpModuleMethod]
		public static DynValue rawget(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawget", DataType.Table, false);
			DynValue key = args[1];
			return dynValue.Table.Get(key);
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x0029C444 File Offset: 0x0029A644
		[MoonSharpModuleMethod]
		public static DynValue rawset(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawset", DataType.Table, false);
			DynValue key = args[1];
			dynValue.Table.Set(key, args[2]);
			return dynValue;
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x0029C47C File Offset: 0x0029A67C
		[MoonSharpModuleMethod]
		public static DynValue rawequal(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			object obj = args[0];
			DynValue obj2 = args[1];
			return DynValue.NewBoolean(obj.Equals(obj2));
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x0029C4A4 File Offset: 0x0029A6A4
		[MoonSharpModuleMethod]
		public static DynValue rawlen(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			if (args[0].Type != DataType.String && args[0].Type != DataType.Table)
			{
				throw ScriptRuntimeException.BadArgument(0, "rawlen", "table or string", args[0].Type.ToErrorTypeString(), false);
			}
			return args[0].GetLength();
		}
	}
}
