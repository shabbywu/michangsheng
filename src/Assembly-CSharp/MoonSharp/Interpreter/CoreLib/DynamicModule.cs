using System;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D77 RID: 3447
	[MoonSharpModule(Namespace = "dynamic")]
	public class DynamicModule
	{
		// Token: 0x060061B4 RID: 25012 RVA: 0x00274AEB File Offset: 0x00272CEB
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			UserData.RegisterType<DynamicModule.DynamicExprWrapper>(InteropAccessMode.HideMembers, null);
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x00274AF8 File Offset: 0x00272CF8
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

		// Token: 0x060061B6 RID: 25014 RVA: 0x00274B98 File Offset: 0x00272D98
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

		// Token: 0x0200168E RID: 5774
		private class DynamicExprWrapper
		{
			// Token: 0x04007305 RID: 29445
			public DynamicExpression Expr;
		}
	}
}
