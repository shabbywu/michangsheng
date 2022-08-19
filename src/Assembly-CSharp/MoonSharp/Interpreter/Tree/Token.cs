using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CCA RID: 3274
	internal class Token
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06005BEE RID: 23534 RVA: 0x0025CAB9 File Offset: 0x0025ACB9
		// (set) Token: 0x06005BEF RID: 23535 RVA: 0x0025CAC1 File Offset: 0x0025ACC1
		public string Text { get; set; }

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0025CACC File Offset: 0x0025ACCC
		public Token(TokenType type, int sourceId, int fromLine, int fromCol, int toLine, int toCol, int prevLine, int prevCol)
		{
			this.Type = type;
			this.SourceId = sourceId;
			this.FromLine = fromLine;
			this.FromCol = fromCol;
			this.ToCol = toCol;
			this.ToLine = toLine;
			this.PrevCol = prevCol;
			this.PrevLine = prevLine;
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0025CB1C File Offset: 0x0025AD1C
		public override string ToString()
		{
			string arg = (this.Type.ToString() + "                                                      ").Substring(0, 16);
			string text = string.Format("{0}:{1}-{2}:{3}", new object[]
			{
				this.FromLine,
				this.FromCol,
				this.ToLine,
				this.ToCol
			});
			text = (text + "                                                      ").Substring(0, 10);
			return string.Format("{0}  - {1} - '{2}'", arg, text, this.Text ?? "");
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0025CBC8 File Offset: 0x0025ADC8
		public static TokenType? GetReservedTokenType(string reservedWord)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(reservedWord);
			if (num <= 1646057492U)
			{
				if (num <= 699505802U)
				{
					if (num <= 228849900U)
					{
						if (num != 184981848U)
						{
							if (num == 228849900U)
							{
								if (reservedWord == "nil")
								{
									return new TokenType?(TokenType.Nil);
								}
							}
						}
						else if (reservedWord == "false")
						{
							return new TokenType?(TokenType.False);
						}
					}
					else if (num != 231090382U)
					{
						if (num != 254395046U)
						{
							if (num == 699505802U)
							{
								if (reservedWord == "not")
								{
									return new TokenType?(TokenType.Not);
								}
							}
						}
						else if (reservedWord == "and")
						{
							return new TokenType?(TokenType.And);
						}
					}
					else if (reservedWord == "while")
					{
						return new TokenType?(TokenType.While);
					}
				}
				else if (num <= 1303515621U)
				{
					if (num != 959999494U)
					{
						if (num != 1094220446U)
						{
							if (num == 1303515621U)
							{
								if (reservedWord == "true")
								{
									return new TokenType?(TokenType.True);
								}
							}
						}
						else if (reservedWord == "in")
						{
							return new TokenType?(TokenType.In);
						}
					}
					else if (reservedWord == "if")
					{
						return new TokenType?(TokenType.If);
					}
				}
				else if (num != 1414876295U)
				{
					if (num != 1563699588U)
					{
						if (num == 1646057492U)
						{
							if (reservedWord == "do")
							{
								return new TokenType?(TokenType.Do);
							}
						}
					}
					else if (reservedWord == "or")
					{
						return new TokenType?(TokenType.Or);
					}
				}
				else if (reservedWord == "elseif")
				{
					return new TokenType?(TokenType.ElseIf);
				}
			}
			else if (num <= 2901640080U)
			{
				if (num <= 2246981567U)
				{
					if (num != 1787721130U)
					{
						if (num == 2246981567U)
						{
							if (reservedWord == "return")
							{
								return new TokenType?(TokenType.Return);
							}
						}
					}
					else if (reservedWord == "end")
					{
						return new TokenType?(TokenType.End);
					}
				}
				else if (num != 2621662984U)
				{
					if (num != 2664841801U)
					{
						if (num == 2901640080U)
						{
							if (reservedWord == "for")
							{
								return new TokenType?(TokenType.For);
							}
						}
					}
					else if (reservedWord == "function")
					{
						return new TokenType?(TokenType.Function);
					}
				}
				else if (reservedWord == "local")
				{
					return new TokenType?(TokenType.Local);
				}
			}
			else if (num <= 3378807160U)
			{
				if (num != 3132432719U)
				{
					if (num != 3183434736U)
					{
						if (num == 3378807160U)
						{
							if (reservedWord == "break")
							{
								return new TokenType?(TokenType.Break);
							}
						}
					}
					else if (reservedWord == "else")
					{
						return new TokenType?(TokenType.Else);
					}
				}
				else if (reservedWord == "until")
				{
					return new TokenType?(TokenType.Until);
				}
			}
			else if (num != 3650857002U)
			{
				if (num != 3844270454U)
				{
					if (num == 4121104358U)
					{
						if (reservedWord == "goto")
						{
							return new TokenType?(TokenType.Goto);
						}
					}
				}
				else if (reservedWord == "then")
				{
					return new TokenType?(TokenType.Then);
				}
			}
			else if (reservedWord == "repeat")
			{
				return new TokenType?(TokenType.Repeat);
			}
			return null;
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0025CFAC File Offset: 0x0025B1AC
		public double GetNumberValue()
		{
			if (this.Type == TokenType.Number)
			{
				return LexerUtils.ParseNumber(this);
			}
			if (this.Type == TokenType.Number_Hex)
			{
				return LexerUtils.ParseHexInteger(this);
			}
			if (this.Type == TokenType.Number_HexFloat)
			{
				return LexerUtils.ParseHexFloat(this);
			}
			throw new NotSupportedException("GetNumberValue is supported only on numeric tokens");
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x0025CFEC File Offset: 0x0025B1EC
		public bool IsEndOfBlock()
		{
			TokenType type = this.Type;
			return type == TokenType.Eof || type - TokenType.Else <= 2 || type == TokenType.Until;
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x0025D011 File Offset: 0x0025B211
		public bool IsUnaryOperator()
		{
			return this.Type == TokenType.Op_MinusOrSub || this.Type == TokenType.Not || this.Type == TokenType.Op_Len;
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x0025D034 File Offset: 0x0025B234
		public bool IsBinaryOperator()
		{
			TokenType type = this.Type;
			if (type != TokenType.And)
			{
				switch (type)
				{
				case TokenType.Or:
				case TokenType.Op_Equal:
				case TokenType.Op_LessThan:
				case TokenType.Op_LessThanEqual:
				case TokenType.Op_GreaterThanEqual:
				case TokenType.Op_GreaterThan:
				case TokenType.Op_NotEqual:
				case TokenType.Op_Concat:
					return true;
				case TokenType.Repeat:
				case TokenType.Return:
				case TokenType.Then:
				case TokenType.True:
				case TokenType.Until:
				case TokenType.While:
				case TokenType.Op_Assignment:
					break;
				default:
					if (type - TokenType.Op_Pwr <= 5)
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x0025D09B File Offset: 0x0025B29B
		internal SourceRef GetSourceRef(bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, this.ToCol, this.FromLine, this.ToLine, isStepStop);
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x0025D0C1 File Offset: 0x0025B2C1
		internal SourceRef GetSourceRef(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.ToCol, this.FromLine, to.ToLine, isStepStop);
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x0025D0E7 File Offset: 0x0025B2E7
		internal SourceRef GetSourceRefUpTo(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.PrevCol, this.FromLine, to.PrevLine, isStepStop);
		}

		// Token: 0x04005305 RID: 21253
		public readonly int SourceId;

		// Token: 0x04005306 RID: 21254
		public readonly int FromCol;

		// Token: 0x04005307 RID: 21255
		public readonly int ToCol;

		// Token: 0x04005308 RID: 21256
		public readonly int FromLine;

		// Token: 0x04005309 RID: 21257
		public readonly int ToLine;

		// Token: 0x0400530A RID: 21258
		public readonly int PrevCol;

		// Token: 0x0400530B RID: 21259
		public readonly int PrevLine;

		// Token: 0x0400530C RID: 21260
		public readonly TokenType Type;
	}
}
