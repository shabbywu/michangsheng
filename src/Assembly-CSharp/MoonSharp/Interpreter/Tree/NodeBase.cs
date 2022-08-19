using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CCE RID: 3278
	internal abstract class NodeBase
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x0025D150 File Offset: 0x0025B350
		// (set) Token: 0x06005C01 RID: 23553 RVA: 0x0025D158 File Offset: 0x0025B358
		public Script Script { get; private set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06005C02 RID: 23554 RVA: 0x0025D161 File Offset: 0x0025B361
		// (set) Token: 0x06005C03 RID: 23555 RVA: 0x0025D169 File Offset: 0x0025B369
		private protected ScriptLoadingContext LoadingContext { protected get; private set; }

		// Token: 0x06005C04 RID: 23556 RVA: 0x0025D172 File Offset: 0x0025B372
		public NodeBase(ScriptLoadingContext lcontext)
		{
			this.Script = lcontext.Script;
		}

		// Token: 0x06005C05 RID: 23557
		public abstract void Compile(ByteCode bc);

		// Token: 0x06005C06 RID: 23558 RVA: 0x0025D186 File Offset: 0x0025B386
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

		// Token: 0x06005C07 RID: 23559 RVA: 0x0025D1B4 File Offset: 0x0025B3B4
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

		// Token: 0x06005C08 RID: 23560 RVA: 0x0025D1EC File Offset: 0x0025B3EC
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

		// Token: 0x06005C09 RID: 23561 RVA: 0x0025D22C File Offset: 0x0025B42C
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

		// Token: 0x06005C0A RID: 23562 RVA: 0x0025D274 File Offset: 0x0025B474
		protected static void CheckTokenTypeNotNext(ScriptLoadingContext lcontext, TokenType tokenType)
		{
			Token token = lcontext.Lexer.Current;
			if (token.Type != tokenType)
			{
				NodeBase.UnexpectedTokenType(token);
			}
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x0025D2A0 File Offset: 0x0025B4A0
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
