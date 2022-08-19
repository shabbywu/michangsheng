using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C9C RID: 3228
	public static class LuaTypeExtensions
	{
		// Token: 0x06005A18 RID: 23064 RVA: 0x002573AF File Offset: 0x002555AF
		public static bool CanHaveTypeMetatables(this DataType type)
		{
			return type < DataType.Table;
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x002573B8 File Offset: 0x002555B8
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

		// Token: 0x06005A1A RID: 23066 RVA: 0x0025744D File Offset: 0x0025564D
		public static string ToLuaDebuggerString(this DataType type)
		{
			return type.ToString().ToLowerInvariant();
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x00257464 File Offset: 0x00255664
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

		// Token: 0x04005271 RID: 21105
		internal const DataType MaxMetaTypes = DataType.Table;

		// Token: 0x04005272 RID: 21106
		internal const DataType MaxConvertibleTypes = DataType.ClrFunction;
	}
}
