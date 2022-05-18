using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020010A4 RID: 4260
	internal abstract class NodeBase
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x00046F6F File Offset: 0x0004516F
		// (set) Token: 0x0600670A RID: 26378 RVA: 0x00046F77 File Offset: 0x00045177
		public Script Script { get; private set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x00046F80 File Offset: 0x00045180
		// (set) Token: 0x0600670C RID: 26380 RVA: 0x00046F88 File Offset: 0x00045188
		private protected ScriptLoadingContext LoadingContext { protected get; private set; }

		// Token: 0x0600670D RID: 26381 RVA: 0x00046F91 File Offset: 0x00045191
		public NodeBase(ScriptLoadingContext lcontext)
		{
			this.Script = lcontext.Script;
		}

		// Token: 0x0600670E RID: 26382
		public abstract void Compile(ByteCode bc);

		// Token: 0x0600670F RID: 26383 RVA: 0x00046FA5 File Offset: 0x000451A5
		protected static Token UnexpectedTokenType(Token t)
		{
			throw new SyntaxErrorException(t, "unexpected symbol near '{0}'", new object[]
			{
				t.Text
			})
			{
				IsPrematureStreamTermination = (t.Type == TokenType.Eof)
			};
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x002869C4 File Offset: 0x00284BC4
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x002869FC File Offset: 0x00284BFC
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType1, TokenType tokenType2)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType1 && token.Type != tokenType2)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x00286A3C File Offset: 0x00284C3C
		protected static Token CheckTokenType(ScriptLoadingContext lcontext, TokenType tokenType1, TokenType tokenType2, TokenType tokenType3)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType1 && token.Type != tokenType2 && token.Type != tokenType3)
			{
				return NodeBase.UnexpectedTokenType(token);
			}
			lcontext.Lexer.Next();
			return token;
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x00286A84 File Offset: 0x00284C84
		protected static void CheckTokenTypeNotNext(ScriptLoadingContext lcontext, TokenType tokenType)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType)
			{
				NodeBase.UnexpectedTokenType(token);
			}
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x00286AB0 File Offset: 0x00284CB0
		protected static Token CheckMatch(ScriptLoadingContext lcontext, Token originalToken, TokenType expectedTokenType, string expectedTokenText)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != expectedTokenType)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "'{0}' expected (to close '{1}' at line {2}) near '{3}'", new object[]
				{
					expectedTokenText,
					originalToken.Text,
					originalToken.FromLine,
					token.Text
				})
				{
					IsPrematureStreamTermination = (token.Type == TokenType.Eof)
				};
			}
			lcontext.Lexer.Next();
			return token;
		}
	}
}
