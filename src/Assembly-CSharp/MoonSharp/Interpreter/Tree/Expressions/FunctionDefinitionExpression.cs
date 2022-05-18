using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C2 RID: 4290
	internal class FunctionDefinitionExpression : Expression, IClosureBuilder
	{
		// Token: 0x0600678B RID: 26507 RVA: 0x0004735E File Offset: 0x0004555E
		public FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool usesGlobalEnv) : this(lcontext, false, usesGlobalEnv, false)
		{
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x0004736A File Offset: 0x0004556A
		public FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool pushSelfParam, bool isLambda) : this(lcontext, pushSelfParam, false, isLambda)
		{
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x002893A8 File Offset: 0x002875A8
		private FunctionDefinitionExpression(ScriptLoadingContext lcontext, bool pushSelfParam, bool usesGlobalEnv, bool isLambda) : base(lcontext)
		{
			this.m_UsesGlobalEnv = usesGlobalEnv;
			if (usesGlobalEnv)
			{
				NodeBase.CheckTokenType(lcontext, TokenType.Function);
			}
			Token token = NodeBase.CheckTokenType(lcontext, isLambda ? TokenType.Lambda : TokenType.Brk_Open_Round);
			List<string> paramnames = this.BuildParamList(lcontext, pushSelfParam, token, isLambda);
			this.m_Begin = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			lcontext.Scope.PushFunction(this, this.m_HasVarArgs);
			if (this.m_UsesGlobalEnv)
			{
				this.m_Env = lcontext.Scope.DefineLocal("_ENV");
			}
			else
			{
				lcontext.Scope.ForceEnvUpValue();
			}
			this.m_ParamNames = this.DefineArguments(paramnames, lcontext);
			if (isLambda)
			{
				this.m_Statement = this.CreateLambdaBody(lcontext);
			}
			else
			{
				this.m_Statement = this.CreateBody(lcontext);
			}
			this.m_StackFrame = lcontext.Scope.PopFunction();
			lcontext.Source.Refs.Add(this.m_Begin);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x002894BC File Offset: 0x002876BC
		private Statement CreateLambdaBody(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			Expression e = Expression.Expr(lcontext);
			Token to = lcontext.Lexer.Current;
			SourceRef sourceRefUpTo = token.GetSourceRefUpTo(to, true);
			return new ReturnStatement(lcontext, e, sourceRefUpTo);
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x002894F8 File Offset: 0x002876F8
		private Statement CreateBody(ScriptLoadingContext lcontext)
		{
			Statement result = new CompositeStatement(lcontext);
			if (lcontext.Lexer.Current.Type != TokenType.End)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "'end' expected near '{0}'", new object[]
				{
					lcontext.Lexer.Current.Text
				})
				{
					IsPrematureStreamTermination = (lcontext.Lexer.Current.Type == TokenType.Eof)
				};
			}
			this.m_End = lcontext.Lexer.Current.GetSourceRef(true);
			lcontext.Lexer.Next();
			return result;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x0028958C File Offset: 0x0028778C
		private List<string> BuildParamList(ScriptLoadingContext lcontext, bool pushSelfParam, Token openBracketToken, bool isLambda)
		{
			TokenType tokenType = isLambda ? TokenType.Lambda : TokenType.Brk_Close_Round;
			List<string> list = new List<string>();
			if (pushSelfParam)
			{
				list.Add("self");
			}
			while (lcontext.Lexer.Current.Type != tokenType)
			{
				Token token = lcontext.Lexer.Current;
				if (token.Type == TokenType.Name)
				{
					list.Add(token.Text);
				}
				else if (token.Type == TokenType.VarArgs)
				{
					this.m_HasVarArgs = true;
					list.Add("...");
				}
				else
				{
					NodeBase.UnexpectedTokenType(token);
				}
				lcontext.Lexer.Next();
				token = lcontext.Lexer.Current;
				if (token.Type != TokenType.Comma)
				{
					NodeBase.CheckMatch(lcontext, openBracketToken, tokenType, isLambda ? "|" : ")");
					break;
				}
				lcontext.Lexer.Next();
			}
			if (lcontext.Lexer.Current.Type == tokenType)
			{
				lcontext.Lexer.Next();
			}
			return list;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x00289688 File Offset: 0x00287888
		private SymbolRef[] DefineArguments(List<string> paramnames, ScriptLoadingContext lcontext)
		{
			HashSet<string> hashSet = new HashSet<string>();
			SymbolRef[] array = new SymbolRef[paramnames.Count];
			for (int i = paramnames.Count - 1; i >= 0; i--)
			{
				if (!hashSet.Add(paramnames[i]))
				{
					paramnames[i] = paramnames[i] + "@" + i.ToString();
				}
				array[i] = lcontext.Scope.DefineLocal(paramnames[i]);
			}
			return array;
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x00289700 File Offset: 0x00287900
		public SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol)
		{
			for (int i = 0; i < this.m_Closure.Count; i++)
			{
				if (this.m_Closure[i].i_Name == symbol.i_Name)
				{
					return SymbolRef.Upvalue(symbol.i_Name, i);
				}
			}
			this.m_Closure.Add(symbol);
			if (this.m_ClosureInstruction != null)
			{
				this.m_ClosureInstruction.SymbolList = this.m_Closure.ToArray();
			}
			return SymbolRef.Upvalue(symbol.i_Name, this.m_Closure.Count - 1);
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x00047376 File Offset: 0x00045576
		public override DynValue Eval(ScriptExecutionContext context)
		{
			throw new DynamicExpressionException("Dynamic Expressions cannot define new functions.");
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x00289790 File Offset: 0x00287990
		public int CompileBody(ByteCode bc, string friendlyName)
		{
			string funcName = friendlyName ?? ("<" + this.m_Begin.FormatLocation(bc.Script, true) + ">");
			bc.PushSourceRef(this.m_Begin);
			Instruction instruction = bc.Emit_Jump(OpCode.Jump, -1, 0);
			Instruction instruction2 = bc.Emit_Meta(funcName, OpCodeMetadataType.FunctionEntrypoint, null);
			int jumpPointForLastInstruction = bc.GetJumpPointForLastInstruction();
			bc.Emit_BeginFn(this.m_StackFrame);
			bc.LoopTracker.Loops.Push(new LoopBoundary());
			int jumpPointForLastInstruction2 = bc.GetJumpPointForLastInstruction();
			if (this.m_UsesGlobalEnv)
			{
				bc.Emit_Load(SymbolRef.Upvalue("_ENV", 0));
				bc.Emit_Store(this.m_Env, 0, 0);
				bc.Emit_Pop(1);
			}
			if (this.m_ParamNames.Length != 0)
			{
				bc.Emit_Args(this.m_ParamNames);
			}
			this.m_Statement.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_End);
			bc.Emit_Ret(0);
			bc.LoopTracker.Loops.Pop();
			instruction.NumVal = bc.GetJumpPointForNextInstruction();
			instruction2.NumVal = bc.GetJumpPointForLastInstruction() - jumpPointForLastInstruction;
			bc.PopSourceRef();
			return jumpPointForLastInstruction2;
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x002898B4 File Offset: 0x00287AB4
		public int Compile(ByteCode bc, Func<int> afterDecl, string friendlyName)
		{
			using (bc.EnterSource(this.m_Begin))
			{
				SymbolRef[] symbols = this.m_Closure.ToArray();
				this.m_ClosureInstruction = bc.Emit_Closure(symbols, bc.GetJumpPointForNextInstruction());
				int num = afterDecl();
				this.m_ClosureInstruction.NumVal += 2 + num;
			}
			return this.CompileBody(bc, friendlyName);
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x00047382 File Offset: 0x00045582
		public override void Compile(ByteCode bc)
		{
			this.Compile(bc, () => 0, null);
		}

		// Token: 0x04005FA8 RID: 24488
		private SymbolRef[] m_ParamNames;

		// Token: 0x04005FA9 RID: 24489
		private Statement m_Statement;

		// Token: 0x04005FAA RID: 24490
		private RuntimeScopeFrame m_StackFrame;

		// Token: 0x04005FAB RID: 24491
		private List<SymbolRef> m_Closure = new List<SymbolRef>();

		// Token: 0x04005FAC RID: 24492
		private bool m_HasVarArgs;

		// Token: 0x04005FAD RID: 24493
		private Instruction m_ClosureInstruction;

		// Token: 0x04005FAE RID: 24494
		private bool m_UsesGlobalEnv;

		// Token: 0x04005FAF RID: 24495
		private SymbolRef m_Env;

		// Token: 0x04005FB0 RID: 24496
		private SourceRef m_Begin;

		// Token: 0x04005FB1 RID: 24497
		private SourceRef m_End;
	}
}
