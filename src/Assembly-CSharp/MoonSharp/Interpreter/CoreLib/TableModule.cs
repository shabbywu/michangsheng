using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200119F RID: 4511
	[MoonSharpModule(Namespace = "table")]
	public class TableModule
	{
		// Token: 0x06006E66 RID: 28262 RVA: 0x0029D5D4 File Offset: 0x0029B7D4
		[MoonSharpModuleMethod]
		public static DynValue unpack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "unpack", DataType.Table, false);
			DynValue dynValue2 = args.AsType(1, "unpack", DataType.Number, true);
			DynValue dynValue3 = args.AsType(2, "unpack", DataType.Number, true);
			int num = dynValue2.IsNil() ? 1 : ((int)dynValue2.Number);
			int num2 = dynValue3.IsNil() ? TableModule.GetTableLength(executionContext, dynValue) : ((int)dynValue3.Number);
			Table table = dynValue.Table;
			DynValue[] array = new DynValue[num2 - num + 1];
			int num3 = 0;
			for (int i = num; i <= num2; i++)
			{
				array[num3++] = table.Get(i);
			}
			return DynValue.NewTuple(array);
		}

		// Token: 0x06006E67 RID: 28263 RVA: 0x0029D680 File Offset: 0x0029B880
		[MoonSharpModuleMethod]
		public static DynValue pack(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			Table table = new Table(executionContext.GetScript());
			DynValue result = DynValue.NewTable(table);
			for (int i = 0; i < args.Count; i++)
			{
				table.Set(i + 1, args[i]);
			}
			table.Set("n", DynValue.NewNumber((double)args.Count));
			return result;
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x0029D6DC File Offset: 0x0029B8DC
		[MoonSharpModuleMethod]
		public static DynValue sort(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "sort", DataType.Table, false);
			DynValue lt = args[1];
			if (lt.Type != DataType.Function && lt.Type != DataType.ClrFunction && lt.IsNotNil())
			{
				args.AsType(1, "sort", DataType.Function, true);
			}
			int tableLength = TableModule.GetTableLength(executionContext, dynValue);
			List<DynValue> list = new List<DynValue>();
			for (int i = 1; i <= tableLength; i++)
			{
				list.Add(dynValue.Table.Get(i));
			}
			try
			{
				list.Sort((DynValue a, DynValue b) => TableModule.SortComparer(executionContext, a, b, lt));
			}
			catch (InvalidOperationException ex)
			{
				if (ex.InnerException is ScriptRuntimeException)
				{
					throw ex.InnerException;
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				dynValue.Table.Set(j + 1, list[j]);
			}
			return dynValue;
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x0029D7F0 File Offset: 0x0029B9F0
		private static int SortComparer(ScriptExecutionContext executionContext, DynValue a, DynValue b, DynValue lt)
		{
			if (lt != null && !lt.IsNil())
			{
				return TableModule.LuaComparerToClrComparer(executionContext.GetScript().Call(lt, new DynValue[]
				{
					a,
					b
				}), executionContext.GetScript().Call(lt, new DynValue[]
				{
					b,
					a
				}));
			}
			lt = executionContext.GetBinaryMetamethod(a, b, "__lt");
			if (lt != null && !lt.IsNil())
			{
				return TableModule.LuaComparerToClrComparer(executionContext.GetScript().Call(lt, new DynValue[]
				{
					a,
					b
				}), executionContext.GetScript().Call(lt, new DynValue[]
				{
					b,
					a
				}));
			}
			if (a.Type == DataType.Number && b.Type == DataType.Number)
			{
				return a.Number.CompareTo(b.Number);
			}
			if (a.Type == DataType.String && b.Type == DataType.String)
			{
				return a.String.CompareTo(b.String);
			}
			throw ScriptRuntimeException.CompareInvalidType(a, b);
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x0029D8EC File Offset: 0x0029BAEC
		private static int LuaComparerToClrComparer(DynValue dynValue1, DynValue dynValue2)
		{
			bool flag = dynValue1.CastToBool();
			bool flag2 = dynValue2.CastToBool();
			if (flag && !flag2)
			{
				return -1;
			}
			if (flag2 && !flag)
			{
				return 1;
			}
			if (flag || flag2)
			{
				throw new ScriptRuntimeException("invalid order function for sorting");
			}
			return 0;
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x0029D928 File Offset: 0x0029BB28
		[MoonSharpModuleMethod]
		public static DynValue insert(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "table.insert", DataType.Table, false);
			DynValue dynValue2 = args[1];
			DynValue dynValue3 = args[2];
			if (args.Count > 3)
			{
				throw new ScriptRuntimeException("wrong number of arguments to 'insert'");
			}
			int tableLength = TableModule.GetTableLength(executionContext, dynValue);
			Table table = dynValue.Table;
			if (dynValue3.IsNil())
			{
				dynValue3 = dynValue2;
				dynValue2 = DynValue.NewNumber((double)(tableLength + 1));
			}
			if (dynValue2.Type != DataType.Number)
			{
				throw ScriptRuntimeException.BadArgument(1, "table.insert", DataType.Number, dynValue2.Type, false);
			}
			int num = (int)dynValue2.Number;
			if (num > tableLength + 1 || num < 1)
			{
				throw new ScriptRuntimeException("bad argument #2 to 'insert' (position out of bounds)");
			}
			for (int i = tableLength; i >= num; i--)
			{
				table.Set(i + 1, table.Get(i));
			}
			table.Set(num, dynValue3);
			return dynValue;
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x0029D9FC File Offset: 0x0029BBFC
		[MoonSharpModuleMethod]
		public static DynValue remove(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "table.remove", DataType.Table, false);
			DynValue dynValue2 = args.AsType(1, "table.remove", DataType.Number, true);
			DynValue result = DynValue.Nil;
			if (args.Count > 2)
			{
				throw new ScriptRuntimeException("wrong number of arguments to 'remove'");
			}
			int tableLength = TableModule.GetTableLength(executionContext, dynValue);
			Table table = dynValue.Table;
			int num = dynValue2.IsNil() ? tableLength : ((int)dynValue2.Number);
			if (num >= tableLength + 1 || (num < 1 && tableLength > 0))
			{
				throw new ScriptRuntimeException("bad argument #1 to 'remove' (position out of bounds)");
			}
			for (int i = num; i <= tableLength; i++)
			{
				if (i == num)
				{
					result = table.Get(i);
				}
				table.Set(i, table.Get(i + 1));
			}
			return result;
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x0029DAB8 File Offset: 0x0029BCB8
		[MoonSharpModuleMethod]
		public static DynValue concat(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "concat", DataType.Table, false);
			DynValue dynValue2 = args.AsType(1, "concat", DataType.String, true);
			DynValue dynValue3 = args.AsType(2, "concat", DataType.Number, true);
			DynValue dynValue4 = args.AsType(3, "concat", DataType.Number, true);
			Table table = dynValue.Table;
			string value = dynValue2.IsNil() ? "" : dynValue2.String;
			int num = dynValue3.IsNilOrNan() ? 1 : ((int)dynValue3.Number);
			int num2;
			if (dynValue4.IsNilOrNan())
			{
				num2 = TableModule.GetTableLength(executionContext, dynValue);
			}
			else
			{
				num2 = (int)dynValue4.Number;
			}
			if (num2 < num)
			{
				return DynValue.NewString(string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = num; i <= num2; i++)
			{
				DynValue dynValue5 = table.Get(i);
				if (dynValue5.Type != DataType.Number && dynValue5.Type != DataType.String)
				{
					throw new ScriptRuntimeException("invalid value ({1}) at index {0} in table for 'concat'", new object[]
					{
						i,
						dynValue5.Type.ToLuaTypeString()
					});
				}
				string value2 = dynValue5.ToPrintString();
				if (i != num)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(value2);
			}
			return DynValue.NewString(stringBuilder.ToString());
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x0029DBF4 File Offset: 0x0029BDF4
		private static int GetTableLength(ScriptExecutionContext executionContext, DynValue vlist)
		{
			DynValue metamethod = executionContext.GetMetamethod(vlist, "__len");
			if (metamethod == null)
			{
				return vlist.Table.Length;
			}
			double? num = executionContext.GetScript().Call(metamethod, new DynValue[]
			{
				vlist
			}).CastToNumber();
			if (num == null)
			{
				throw new ScriptRuntimeException("object length is not a number");
			}
			return (int)num.Value;
		}
	}
}
