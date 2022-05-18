using System;
using System.Text;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x0200109E RID: 4254
	internal class Lexer
	{
		// Token: 0x060066D6 RID: 26326 RVA: 0x00285198 File Offset: 0x00283398
		public Lexer(int sourceID, string scriptContent, bool autoSkipComments)
		{
			this.m_Code = scriptContent;
			this.m_SourceId = sourceID;
			if (this.m_Code.Length > 0 && this.m_Code[0] == '﻿')
			{
				this.m_Code = this.m_Code.Substring(1);
			}
			this.m_AutoSkipComments = autoSkipComments;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x00046D67 File Offset: 0x00044F67
		public Token Current
		{
			get
			{
				if (this.m_Current == null)
				{
					this.Next();
				}
				return this.m_Current;
			}
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x00285204 File Offset: 0x00283404
		private Token FetchNewToken()
		{
			Token token;
			do
			{
				token = this.ReadToken();
			}
			while ((token.Type == TokenType.Comment || token.Type == TokenType.HashBang) && this.m_AutoSkipComments);
			return token;
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x00046D7D File Offset: 0x00044F7D
		public void Next()
		{
			this.m_Current = this.FetchNewToken();
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x00285234 File Offset: 0x00283434
		public Token PeekNext()
		{
			int cursor = this.m_Cursor;
			Token current = this.m_Current;
			int line = this.m_Line;
			int col = this.m_Col;
			this.Next();
			Token result = this.Current;
			this.m_Cursor = cursor;
			this.m_Current = current;
			this.m_Line = line;
			this.m_Col = col;
			return result;
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x00285288 File Offset: 0x00283488
		private void CursorNext()
		{
			if (this.CursorNotEof())
			{
				if (this.CursorChar() == '\n')
				{
					this.m_Col = 0;
					this.m_Line++;
				}
				else
				{
					this.m_Col++;
				}
				this.m_Cursor++;
			}
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x00046D8B File Offset: 0x00044F8B
		private char CursorChar()
		{
			if (this.m_Cursor < this.m_Code.Length)
			{
				return this.m_Code[this.m_Cursor];
			}
			return '\0';
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x00046DB3 File Offset: 0x00044FB3
		private char CursorCharNext()
		{
			this.CursorNext();
			return this.CursorChar();
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x002852DC File Offset: 0x002834DC
		private bool CursorMatches(string pattern)
		{
			for (int i = 0; i < pattern.Length; i++)
			{
				int num = this.m_Cursor + i;
				if (num >= this.m_Code.Length)
				{
					return false;
				}
				if (this.m_Code[num] != pattern[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x00046DC1 File Offset: 0x00044FC1
		private bool CursorNotEof()
		{
			return this.m_Cursor < this.m_Code.Length;
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x00046DD6 File Offset: 0x00044FD6
		private bool IsWhiteSpace(char c)
		{
			return char.IsWhiteSpace(c);
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00046DDE File Offset: 0x00044FDE
		private void SkipWhiteSpace()
		{
			while (this.CursorNotEof() && this.IsWhiteSpace(this.CursorChar()))
			{
				this.CursorNext();
			}
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x0028532C File Offset: 0x0028352C
		private Token ReadToken()
		{
			this.SkipWhiteSpace();
			int line = this.m_Line;
			int col = this.m_Col;
			if (!this.CursorNotEof())
			{
				return this.CreateToken(TokenType.Eof, line, col, "<eof>");
			}
			char c = this.CursorChar();
			if (c <= '>')
			{
				if (c == '\0')
				{
					throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, line, col, null), "unexpected symbol near '{0}'", new object[]
					{
						this.CursorChar()
					})
					{
						IsPrematureStreamTermination = true
					};
				}
				switch (c)
				{
				case '!':
					break;
				case '"':
				case '\'':
					return this.ReadSimpleStringToken(line, col);
				case '#':
					if (this.m_Cursor == 0 && this.m_Code.Length > 1 && this.m_Code[1] == '!')
					{
						return this.ReadHashBang(line, col);
					}
					return this.CreateSingleCharToken(TokenType.Op_Len, line, col);
				case '$':
					return this.PotentiallyDoubleCharOperator('{', TokenType.Op_Dollar, TokenType.Brk_Open_Curly_Shared, line, col);
				case '%':
					return this.CreateSingleCharToken(TokenType.Op_Mod, line, col);
				case '&':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					goto IL_34A;
				case '(':
					return this.CreateSingleCharToken(TokenType.Brk_Open_Round, line, col);
				case ')':
					return this.CreateSingleCharToken(TokenType.Brk_Close_Round, line, col);
				case '*':
					return this.CreateSingleCharToken(TokenType.Op_Mul, line, col);
				case '+':
					return this.CreateSingleCharToken(TokenType.Op_Add, line, col);
				case ',':
					return this.CreateSingleCharToken(TokenType.Comma, line, col);
				case '-':
					if (this.CursorCharNext() == '-')
					{
						return this.ReadComment(line, col);
					}
					return this.CreateToken(TokenType.Op_MinusOrSub, line, col, "-");
				case '.':
				{
					char c2 = this.CursorCharNext();
					if (c2 == '.')
					{
						return this.PotentiallyDoubleCharOperator('.', TokenType.Op_Concat, TokenType.VarArgs, line, col);
					}
					if (LexerUtils.CharIsDigit(c2))
					{
						return this.ReadNumberToken(line, col, true);
					}
					return this.CreateToken(TokenType.Dot, line, col, ".");
				}
				case '/':
					return this.CreateSingleCharToken(TokenType.Op_Div, line, col);
				case ':':
					return this.PotentiallyDoubleCharOperator(':', TokenType.Colon, TokenType.DoubleColon, line, col);
				case ';':
					this.CursorCharNext();
					return this.CreateToken(TokenType.SemiColon, line, col, ";");
				case '<':
					return this.PotentiallyDoubleCharOperator('=', TokenType.Op_LessThan, TokenType.Op_LessThanEqual, line, col);
				case '=':
					return this.PotentiallyDoubleCharOperator('=', TokenType.Op_Assignment, TokenType.Op_Equal, line, col);
				case '>':
					return this.PotentiallyDoubleCharOperator('=', TokenType.Op_GreaterThan, TokenType.Op_GreaterThanEqual, line, col);
				default:
					goto IL_34A;
				}
			}
			else
			{
				switch (c)
				{
				case '[':
				{
					char c3 = this.CursorCharNext();
					if (c3 == '=' || c3 == '[')
					{
						string text = this.ReadLongString(line, col, null, "string");
						return this.CreateToken(TokenType.String_Long, line, col, text);
					}
					return this.CreateToken(TokenType.Brk_Open_Square, line, col, "[");
				}
				case '\\':
					goto IL_34A;
				case ']':
					return this.CreateSingleCharToken(TokenType.Brk_Close_Square, line, col);
				case '^':
					return this.CreateSingleCharToken(TokenType.Op_Pwr, line, col);
				default:
					switch (c)
					{
					case '{':
						return this.CreateSingleCharToken(TokenType.Brk_Open_Curly, line, col);
					case '|':
						this.CursorCharNext();
						return this.CreateToken(TokenType.Lambda, line, col, "|");
					case '}':
						return this.CreateSingleCharToken(TokenType.Brk_Close_Curly, line, col);
					case '~':
						break;
					default:
						goto IL_34A;
					}
					break;
				}
			}
			if (this.CursorCharNext() != '=')
			{
				throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, line, col, null), "unexpected symbol near '{0}'", new object[]
				{
					c
				});
			}
			this.CursorCharNext();
			return this.CreateToken(TokenType.Op_NotEqual, line, col, "~=");
			IL_34A:
			if (char.IsLetter(c) || c == '_')
			{
				string name = this.ReadNameToken();
				return this.CreateNameToken(name, line, col);
			}
			if (LexerUtils.CharIsDigit(c))
			{
				return this.ReadNumberToken(line, col, false);
			}
			throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, line, col, null), "unexpected symbol near '{0}'", new object[]
			{
				this.CursorChar()
			});
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x002856E0 File Offset: 0x002838E0
		private string ReadLongString(int fromLine, int fromCol, string startpattern, string subtypeforerrors)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			string text = "]";
			if (startpattern == null)
			{
				char c = this.CursorChar();
				while (c != '\0' && this.CursorNotEof())
				{
					if (c == '=')
					{
						text += "=";
						c = this.CursorCharNext();
					}
					else
					{
						if (c == '[')
						{
							text += "]";
							goto IL_BF;
						}
						throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, fromLine, fromCol, null), "invalid long {0} delimiter near '{1}'", new object[]
						{
							subtypeforerrors,
							c
						})
						{
							IsPrematureStreamTermination = true
						};
					}
				}
				throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, fromLine, fromCol, null), "unfinished long {0} near '<eof>'", new object[]
				{
					subtypeforerrors
				})
				{
					IsPrematureStreamTermination = true
				};
			}
			text = startpattern.Replace('[', ']');
			IL_BF:
			char c2 = this.CursorCharNext();
			for (;;)
			{
				if (c2 != '\r')
				{
					if (c2 == '\0' || !this.CursorNotEof())
					{
						break;
					}
					if (c2 == ']' && this.CursorMatches(text))
					{
						goto Block_8;
					}
					stringBuilder.Append(c2);
				}
				c2 = this.CursorCharNext();
			}
			throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, fromLine, fromCol, null), "unfinished long {0} near '{1}'", new object[]
			{
				subtypeforerrors,
				stringBuilder.ToString()
			})
			{
				IsPrematureStreamTermination = true
			};
			Block_8:
			for (int i = 0; i < text.Length; i++)
			{
				this.CursorCharNext();
			}
			return LexerUtils.AdjustLuaLongString(stringBuilder.ToString());
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x00285840 File Offset: 0x00283A40
		private Token ReadNumberToken(int fromLine, int fromCol, bool leadingDot)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (leadingDot)
			{
				stringBuilder.Append("0.");
			}
			else if (this.CursorChar() == '0')
			{
				stringBuilder.Append(this.CursorChar());
				char c = this.CursorCharNext();
				if (c == 'x' || c == 'X')
				{
					flag = true;
					stringBuilder.Append(this.CursorChar());
					this.CursorCharNext();
				}
			}
			char c2 = this.CursorChar();
			while (this.CursorNotEof())
			{
				if (flag4 && (c2 == '+' || c2 == '-'))
				{
					flag4 = false;
					stringBuilder.Append(c2);
				}
				else if (LexerUtils.CharIsDigit(c2))
				{
					stringBuilder.Append(c2);
				}
				else if (c2 == '.' && !flag2)
				{
					flag2 = true;
					stringBuilder.Append(c2);
				}
				else if (LexerUtils.CharIsHexDigit(c2) && flag && !flag3)
				{
					stringBuilder.Append(c2);
				}
				else
				{
					if (c2 != 'e' && c2 != 'E' && (!flag || (c2 != 'p' && c2 != 'P')))
					{
						break;
					}
					stringBuilder.Append(c2);
					flag3 = true;
					flag4 = true;
					flag2 = true;
				}
				c2 = this.CursorCharNext();
			}
			TokenType tokenType = TokenType.Number;
			if (flag && (flag2 || flag3))
			{
				tokenType = TokenType.Number_HexFloat;
			}
			else if (flag)
			{
				tokenType = TokenType.Number_Hex;
			}
			string text = stringBuilder.ToString();
			return this.CreateToken(tokenType, fromLine, fromCol, text);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0028598C File Offset: 0x00283B8C
		private Token CreateSingleCharToken(TokenType tokenType, int fromLine, int fromCol)
		{
			char c = this.CursorChar();
			this.CursorCharNext();
			return this.CreateToken(tokenType, fromLine, fromCol, c.ToString());
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x002859B8 File Offset: 0x00283BB8
		private Token ReadHashBang(int fromLine, int fromCol)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			char c = this.CursorChar();
			while (this.CursorNotEof())
			{
				if (c == '\n')
				{
					this.CursorCharNext();
					return this.CreateToken(TokenType.HashBang, fromLine, fromCol, stringBuilder.ToString());
				}
				if (c != '\r')
				{
					stringBuilder.Append(c);
				}
				c = this.CursorCharNext();
			}
			return this.CreateToken(TokenType.HashBang, fromLine, fromCol, stringBuilder.ToString());
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x00285A20 File Offset: 0x00283C20
		private Token ReadComment(int fromLine, int fromCol)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			bool flag = false;
			char c = this.CursorCharNext();
			while (this.CursorNotEof())
			{
				if (c == '[' && !flag && stringBuilder.Length > 0)
				{
					stringBuilder.Append('[');
					string text = this.ReadLongString(fromLine, fromCol, stringBuilder.ToString(), "comment");
					return this.CreateToken(TokenType.Comment, fromLine, fromCol, text);
				}
				if (c == '\n')
				{
					this.CursorCharNext();
					return this.CreateToken(TokenType.Comment, fromLine, fromCol, stringBuilder.ToString());
				}
				if (c != '\r')
				{
					if (c != '[' && c != '=')
					{
						flag = true;
					}
					stringBuilder.Append(c);
				}
				c = this.CursorCharNext();
			}
			return this.CreateToken(TokenType.Comment, fromLine, fromCol, stringBuilder.ToString());
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x00285AD4 File Offset: 0x00283CD4
		private Token ReadSimpleStringToken(int fromLine, int fromCol)
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			char c = this.CursorChar();
			char c2 = this.CursorCharNext();
			IL_DF:
			while (this.CursorNotEof())
			{
				while (c2 == '\\')
				{
					stringBuilder.Append(c2);
					c2 = this.CursorCharNext();
					stringBuilder.Append(c2);
					if (c2 == '\r')
					{
						c2 = this.CursorCharNext();
						if (c2 != '\n')
						{
							continue;
						}
						stringBuilder.Append(c2);
					}
					else if (c2 == 'z')
					{
						c2 = this.CursorCharNext();
						if (char.IsWhiteSpace(c2))
						{
							this.SkipWhiteSpace();
						}
						c2 = this.CursorChar();
						continue;
					}
					IL_D8:
					c2 = this.CursorCharNext();
					goto IL_DF;
				}
				if (c2 == '\n' || c2 == '\r')
				{
					throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, fromLine, fromCol, null), "unfinished string near '{0}'", new object[]
					{
						stringBuilder.ToString()
					});
				}
				if (c2 == c)
				{
					this.CursorCharNext();
					Token token = this.CreateToken(TokenType.String, fromLine, fromCol, null);
					token.Text = LexerUtils.UnescapeLuaString(token, stringBuilder.ToString());
					return token;
				}
				stringBuilder.Append(c2);
				goto IL_D8;
			}
			throw new SyntaxErrorException(this.CreateToken(TokenType.Invalid, fromLine, fromCol, null), "unfinished string near '{0}'", new object[]
			{
				stringBuilder.ToString()
			})
			{
				IsPrematureStreamTermination = true
			};
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x00285BF8 File Offset: 0x00283DF8
		private Token PotentiallyDoubleCharOperator(char expectedSecondChar, TokenType singleCharToken, TokenType doubleCharToken, int fromLine, int fromCol)
		{
			string text = this.CursorChar().ToString();
			this.CursorCharNext();
			if (this.CursorChar() == expectedSecondChar)
			{
				this.CursorCharNext();
				return this.CreateToken(doubleCharToken, fromLine, fromCol, text + expectedSecondChar.ToString());
			}
			return this.CreateToken(singleCharToken, fromLine, fromCol, text);
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x00285C50 File Offset: 0x00283E50
		private Token CreateNameToken(string name, int fromLine, int fromCol)
		{
			TokenType? reservedTokenType = Token.GetReservedTokenType(name);
			if (reservedTokenType != null)
			{
				return this.CreateToken(reservedTokenType.Value, fromLine, fromCol, name);
			}
			return this.CreateToken(TokenType.Name, fromLine, fromCol, name);
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x00285C88 File Offset: 0x00283E88
		private Token CreateToken(TokenType tokenType, int fromLine, int fromCol, string text = null)
		{
			Token token = new Token(tokenType, this.m_SourceId, fromLine, fromCol, this.m_Line, this.m_Col, this.m_PrevLineTo, this.m_PrevColTo);
			token.Text = text;
			this.m_PrevLineTo = this.m_Line;
			this.m_PrevColTo = this.m_Col;
			return token;
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x00285CDC File Offset: 0x00283EDC
		private string ReadNameToken()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			char c = this.CursorChar();
			while (this.CursorNotEof() && (char.IsLetterOrDigit(c) || c == '_'))
			{
				stringBuilder.Append(c);
				c = this.CursorCharNext();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005EDF RID: 24287
		private Token m_Current;

		// Token: 0x04005EE0 RID: 24288
		private string m_Code;

		// Token: 0x04005EE1 RID: 24289
		private int m_PrevLineTo;

		// Token: 0x04005EE2 RID: 24290
		private int m_PrevColTo = 1;

		// Token: 0x04005EE3 RID: 24291
		private int m_Cursor;

		// Token: 0x04005EE4 RID: 24292
		private int m_Line = 1;

		// Token: 0x04005EE5 RID: 24293
		private int m_Col;

		// Token: 0x04005EE6 RID: 24294
		private int m_SourceId;

		// Token: 0x04005EE7 RID: 24295
		private bool m_AutoSkipComments;
	}
}
