using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Diagnostics;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Tree.Fast_Interface
{
	// Token: 0x020010B9 RID: 4281
	internal static class Loader_Fast
	{
		// Token: 0x06006764 RID: 26468 RVA: 0x00288604 File Offset: 0x00286804
		internal static DynamicExprExpression LoadDynamicExpr(Script script, SourceCode source)
		{
			ScriptLoadingContext scriptLoadingContext = Loader_Fast.CreateLoadingContext(script, source);
			DynamicExprExpression result;
			try
			{
				scriptLoadingContext.IsDynamicExpression = true;
				scriptLoadingContext.Anonymous = true;
				Expression exp;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					exp = Expression.Expr(scriptLoadingContext);
				}
				result = new DynamicExprExpression(exp, scriptLoadingContext);
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x00047203 File Offset: 0x00045403
		private static ScriptLoadingContext CreateLoadingContext(Script script, SourceCode source)
		{
			return new ScriptLoadingContext(script)
			{
				Scope = new BuildTimeScope(),
				Source = source,
				Lexer = new Lexer(source.SourceID, source.Code, true)
			};
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x0028867C File Offset: 0x0028687C
		internal static int LoadChunk(Script script, SourceCode source, ByteCode bytecode)
		{
			ScriptLoadingContext lcontext = Loader_Fast.CreateLoadingContext(script, source);
			int result;
			try
			{
				Statement statement;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					statement = new ChunkStatement(lcontext);
				}
				int num = -1;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.Compilation))
				{
					using (bytecode.EnterSource(null))
					{
						bytecode.Emit_Nop(string.Format("Begin chunk {0}", source.Name));
						num = bytecode.GetJumpPointForLastInstruction();
						statement.Compile(bytecode);
						bytecode.Emit_Nop(string.Format("End chunk {0}", source.Name));
					}
				}
				result = num;
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x00288768 File Offset: 0x00286968
		internal static int LoadFunction(Script script, SourceCode source, ByteCode bytecode, bool usesGlobalEnv)
		{
			ScriptLoadingContext lcontext = Loader_Fast.CreateLoadingContext(script, source);
			int result;
			try
			{
				FunctionDefinitionExpression functionDefinitionExpression;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.AstCreation))
				{
					functionDefinitionExpression = new FunctionDefinitionExpression(lcontext, usesGlobalEnv);
				}
				int num = -1;
				using (script.PerformanceStats.StartStopwatch(PerformanceCounter.Compilation))
				{
					using (bytecode.EnterSource(null))
					{
						bytecode.Emit_Nop(string.Format("Begin function {0}", source.Name));
						num = functionDefinitionExpression.CompileBody(bytecode, source.Name);
						bytecode.Emit_Nop(string.Format("End function {0}", source.Name));
					}
				}
				result = num;
			}
			catch (SyntaxErrorException ex)
			{
				ex.DecorateMessage(script);
				ex.Rethrow();
				throw;
			}
			return result;
		}
	}
}
