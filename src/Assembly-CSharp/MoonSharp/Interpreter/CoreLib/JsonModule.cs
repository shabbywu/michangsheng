using System;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7A RID: 3450
	[MoonSharpModule(Namespace = "json")]
	public class JsonModule
	{
		// Token: 0x060061D6 RID: 25046 RVA: 0x002755E0 File Offset: 0x002737E0
		[MoonSharpModuleMethod]
		public static DynValue parse(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				result = DynValue.NewTable(JsonTableConverter.JsonToTable(args.AsType(0, "parse", DataType.String, false).String, executionContext.GetScript()));
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0027562C File Offset: 0x0027382C
		[MoonSharpModuleMethod]
		public static DynValue serialize(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				result = DynValue.NewString(args.AsType(0, "serialize", DataType.Table, false).Table.TableToJson());
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x00275670 File Offset: 0x00273870
		[MoonSharpModuleMethod]
		public static DynValue isnull(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			return DynValue.NewBoolean(JsonNull.IsJsonNull(dynValue) || dynValue.IsNil());
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x0027569B File Offset: 0x0027389B
		[MoonSharpModuleMethod]
		public static DynValue @null(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return JsonNull.Create();
		}
	}
}
