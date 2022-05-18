using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02001190 RID: 4496
	[MoonSharpModule(Namespace = "dynamic")]
	public class DynamicModule
	{
		// Token: 0x06006DBA RID: 28090 RVA: 0x0004ABB2 File Offset: 0x00048DB2
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			UserData.RegisterType<DynamicModule.DynamicExprWrapper>(InteropAccessMode.HideMembers, null);
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x0029B2AC File Offset: 0x002994AC
		[MoonSharpModuleMethod]
		public static DynValue eval(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				if (args[0].Type == DataType.UserData)
				{
					UserData userData = args[0].UserData;
					if (!(userData.Object is DynamicModule.DynamicExprWrapper))
					{
						throw ScriptRuntimeException.BadArgument(0, "dynamic.eval", "A userdata was passed, but was not a previously prepared expression.");
					}
					result = ((DynamicModule.DynamicExprWrapper)userData.Object).Expr.Evaluate(executionContext);
				}
				else
				{
					DynValue dynValue = args.AsType(0, "dynamic.eval", DataType.String, false);
					result = executionContext.GetScript().CreateDynamicExpression(dynValue.String).Evaluate(executionContext);
				}
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x0029B34C File Offset: 0x0029954C
		[MoonSharpModuleMethod]
		public static DynValue prepare(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue result;
			try
			{
				DynValue dynValue = args.AsType(0, "dynamic.prepare", DataType.String, false);
				DynamicExpression expr = executionContext.GetScript().CreateDynamicExpression(dynValue.String);
				result = UserData.Create(new DynamicModule.DynamicExprWrapper
				{
					Expr = expr
				});
			}
			catch (SyntaxErrorException ex)
			{
				throw new ScriptRuntimeException(ex);
			}
			return result;
		}

		// Token: 0x02001191 RID: 4497
		private class DynamicExprWrapper
		{
			// Token: 0x04006236 RID: 25142
			public DynamicExpression Expr;
		}
	}
}
