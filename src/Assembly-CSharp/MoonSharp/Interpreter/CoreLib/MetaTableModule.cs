using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7D RID: 3453
	[MoonSharpModule]
	public class MetaTableModule
	{
		// Token: 0x06006209 RID: 25097 RVA: 0x00276168 File Offset: 0x00274368
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

		// Token: 0x0600620A RID: 25098 RVA: 0x002761C0 File Offset: 0x002743C0
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

		// Token: 0x0600620B RID: 25099 RVA: 0x00276234 File Offset: 0x00274434
		[MoonSharpModuleMethod]
		public static DynValue rawget(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawget", DataType.Table, false);
			DynValue key = args[1];
			return dynValue.Table.Get(key);
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x00276264 File Offset: 0x00274464
		[MoonSharpModuleMethod]
		public static DynValue rawset(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rawset", DataType.Table, false);
			DynValue key = args[1];
			dynValue.Table.Set(key, args[2]);
			return dynValue;
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0027629C File Offset: 0x0027449C
		[MoonSharpModuleMethod]
		public static DynValue rawequal(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			object obj = args[0];
			DynValue obj2 = args[1];
			return DynValue.NewBoolean(obj.Equals(obj2));
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x002762C4 File Offset: 0x002744C4
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
