using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010AF RID: 4271
	internal class FunctionDefinitionStatement : Statement
	{
		// Token: 0x0600672F RID: 26415 RVA: 0x00287864 File Offset: 0x00285A64
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

		// Token: 0x06006730 RID: 26416 RVA: 0x00287A68 File Offset: 0x00285C68
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

		// Token: 0x06006731 RID: 26417 RVA: 0x00287B5C File Offset: 0x00285D5C
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

		// Token: 0x06006732 RID: 26418 RVA: 0x00047060 File Offset: 0x00045260
		private int SetFunction(ByteCode bc, int numPop)
		{
			int num = bc.Emit_Store(this.m_FuncSymbol, 0, 0);
			bc.Emit_Pop(numPop);
			return num + 1;
		}

		// Token: 0x04005F4E RID: 24398
		private SymbolRef m_FuncSymbol;

		// Token: 0x04005F4F RID: 24399
		private SourceRef m_SourceRef;

		// Token: 0x04005F50 RID: 24400
		private bool m_Local;

		// Token: 0x04005F51 RID: 24401
		private bool m_IsMethodCallingConvention;

		// Token: 0x04005F52 RID: 24402
		private string m_MethodName;

		// Token: 0x04005F53 RID: 24403
		private string m_FriendlyName;

		// Token: 0x04005F54 RID: 24404
		private List<string> m_TableAccessors;

		// Token: 0x04005F55 RID: 24405
		private FunctionDefinitionExpression m_FuncDef;
	}
}
