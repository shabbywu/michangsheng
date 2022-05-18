using System;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001195 RID: 4501
	[MoonSharpModule(Namespace = "json")]
	public class JsonModule
	{
		// Token: 0x06006DE0 RID: 28128 RVA: 0x0029BC44 File Offset: 0x00299E44
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

		// Token: 0x06006DE1 RID: 28129 RVA: 0x0029BC90 File Offset: 0x00299E90
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

		// Token: 0x06006DE2 RID: 28130 RVA: 0x0029BCD4 File Offset: 0x00299ED4
		[MoonSharpModuleMethod]
		public static DynValue isnull(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args[0];
			return DynValue.NewBoolean(JsonNull.IsJsonNull(dynValue) || dynValue.IsNil());
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x0004AD14 File Offset: 0x00048F14
		[MoonSharpModuleMethod]
		public static DynValue @null(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return JsonNull.Create();
		}
	}
}
