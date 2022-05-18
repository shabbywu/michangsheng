using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001068 RID: 4200
	public static class LuaTypeExtensions
	{
		// Token: 0x060064FB RID: 25851 RVA: 0x000456C3 File Offset: 0x000438C3
		public static bool CanHaveTypeMetatables(this DataType type)
		{
			return type < DataType.Table;
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x00282374 File Offset: 0x00280574
		public static string ToErrorTypeString(this DataType type)
		{
			switch (type)
			{
			case DataType.Nil:
				return "nil";
			case DataType.Void:
				return "no value";
			case DataType.Boolean:
				return "boolean";
			case DataType.Number:
				return "number";
			case DataType.String:
				return "string";
			case DataType.Function:
				return "function";
			case DataType.Table:
				return "table";
			case DataType.UserData:
				return "userdata";
			case DataType.Thread:
				return "coroutine";
			case DataType.ClrFunction:
				return "function";
			}
			return string.Format("internal<{0}>", type.ToLuaDebuggerString());
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x000456C9 File Offset: 0x000438C9
		public static string ToLuaDebuggerString(this DataType type)
		{
			return type.ToString().ToLowerInvariant();
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x0028240C File Offset: 0x0028060C
		public static string ToLuaTypeString(this DataType type)
		{
			switch (type)
			{
			case DataType.Nil:
			case DataType.Void:
				return "nil";
			case DataType.Boolean:
				return "boolean";
			case DataType.Number:
				return "number";
			case DataType.String:
				return "string";
			case DataType.Function:
				return "function";
			case DataType.Table:
				return "table";
			case DataType.UserData:
				return "userdata";
			case DataType.Thread:
				return "thread";
			case DataType.ClrFunction:
				return "function";
			}
			throw new ScriptRuntimeException("Unexpected LuaType {0}", new object[]
			{
				type
			});
		}

		// Token: 0x04005E3A RID: 24122
		internal const DataType MaxMetaTypes = DataType.Table;

		// Token: 0x04005E3B RID: 24123
		internal const DataType MaxConvertibleTypes = DataType.ClrFunction;
	}
}
