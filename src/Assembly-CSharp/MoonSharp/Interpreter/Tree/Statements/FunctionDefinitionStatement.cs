using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD8 RID: 3288
	internal class FunctionDefinitionStatement : Statement
	{
		// Token: 0x06005C23 RID: 23587 RVA: 0x0025E0CC File Offset: 0x0025C2CC
		public FunctionDefinitionStatement(ScriptLoadingContext lcontext, bool local, Token localToken) : base(lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Function);
			token = (localToken ?? token);
			this.m_Local = local;
			if (this.m_Local)
			{
				Token token2 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				this.m_FuncSymbol = lcontext.Scope.TryDefineLocal(token2.Text);
				this.m_FriendlyName = string.Format("{0} (local)", token2.Text);
				this.m_SourceRef = token.GetSourceRef(token2, true);
			}
			else
			{
				Token token3 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				string text = token3.Text;
				this.m_SourceRef = token.GetSourceRef(token3, true);
				this.m_FuncSymbol = lcontext.Scope.Find(text);
				this.m_FriendlyName = text;
				if (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
				{
					this.m_TableAccessors = new List<string>();
					while (lcontext.Lexer.Current.Type != TokenType.Brk_Open_Round)
					{
						Token token4 = lcontext.Lexer.Current;
						if (token4.Type != TokenType.Colon && token4.Type != TokenType.Dot)
						{
							NodeBase.UnexpectedTokenType(token4);
						}
						lcontext.Lexer.Next();
						Token token5 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
						this.m_FriendlyName = this.m_FriendlyName + token4.Text + token5.Text;
						this.m_SourceRef = token.GetSourceRef(token5, true);
						if (token4.Type == TokenType.Colon)
						{
							this.m_MethodName = token5.Text;
							this.m_IsMethodCallingConvention = true;
							break;
						}
						this.m_TableAccessors.Add(token5.Text);
					}
					if (this.m_MethodName == null && this.m_TableAccessors.Count > 0)
					{
						this.m_MethodName = this.m_TableAccessors[this.m_TableAccessors.Count - 1];
						this.m_TableAccessors.RemoveAt(this.m_TableAccessors.Count - 1);
					}
				}
			}
			this.m_FuncDef = new FunctionDefinitionExpression(lcontext, this.m_IsMethodCallingConvention, false);
			lcontext.Source.Refs.Add(this.m_SourceRef);
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x0025E2D0 File Offset: 0x0025C4D0
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_SourceRef))
			{
				if (this.m_Local)
				{
					bc.Emit_Literal(DynValue.Nil);
					bc.Emit_Store(this.m_FuncSymbol, 0, 0);
					this.m_FuncDef.Compile(bc, () => this.SetFunction(bc, 2), this.m_FriendlyName);
				}
				else if (this.m_MethodName == null)
				{
					this.m_FuncDef.Compile(bc, () => this.SetFunction(bc, 1), this.m_FriendlyName);
				}
				else
				{
					this.m_FuncDef.Compile(bc, () => this.SetMethod(bc), this.m_FriendlyName);
				}
			}
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x0025E3C4 File Offset: 0x0025C5C4
		private int SetMethod(ByteCode bc)
		{
			int num = 0;
			num += bc.Emit_Load(this.m_FuncSymbol);
			foreach (string str in this.m_TableAccessors)
			{
				bc.Emit_Index(DynValue.NewString(str), true, false);
				num++;
			}
			bc.Emit_IndexSet(0, 0, DynValue.NewString(this.m_MethodName), true, false);
			return 1 + num;
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x0025E450 File Offset: 0x0025C650
		private int SetFunction(ByteCode bc, int numPop)
		{
			int num = bc.Emit_Store(this.m_FuncSymbol, 0, 0);
			bc.Emit_Pop(numPop);
			return num + 1;
		}

		// Token: 0x0400536A RID: 21354
		private SymbolRef m_FuncSymbol;

		// Token: 0x0400536B RID: 21355
		private SourceRef m_SourceRef;

		// Token: 0x0400536C RID: 21356
		private bool m_Local;

		// Token: 0x0400536D RID: 21357
		private bool m_IsMethodCallingConvention;

		// Token: 0x0400536E RID: 21358
		private string m_MethodName;

		// Token: 0x0400536F RID: 21359
		private string m_FriendlyName;

		// Token: 0x04005370 RID: 21360
		private List<string> m_TableAccessors;

		// Token: 0x04005371 RID: 21361
		private FunctionDefinitionExpression m_FuncDef;
	}
}
