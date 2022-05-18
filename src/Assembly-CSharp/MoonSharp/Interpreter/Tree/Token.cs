using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020010A0 RID: 4256
	internal class Token
	{
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060066F7 RID: 26359 RVA: 0x00046E48 File Offset: 0x00045048
		// (set) Token: 0x060066F8 RID: 26360 RVA: 0x00046E50 File Offset: 0x00045050
		public string Text { get; set; }

		// Token: 0x060066F9 RID: 26361 RVA: 0x00286454 File Offset: 0x00284654
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

		// Token: 0x060066FA RID: 26362 RVA: 0x002864A4 File Offset: 0x002846A4
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

		// Token: 0x060066FB RID: 26363 RVA: 0x00286550 File Offset: 0x00284750
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

		// Token: 0x060066FC RID: 26364 RVA: 0x00046E59 File Offset: 0x00045059
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

		// Token: 0x060066FD RID: 26365 RVA: 0x00286934 File Offset: 0x00284B34
		public bool IsEndOfBlock()
		{
			TokenType type = this.Type;
			return type == TokenType.Eof || type - TokenType.Else <= 2 || type == TokenType.Until;
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x00046E98 File Offset: 0x00045098
		public bool IsUnaryOperator()
		{
			return this.Type == TokenType.Op_MinusOrSub || this.Type == TokenType.Not || this.Type == TokenType.Op_Len;
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x0028695C File Offset: 0x00284B5C
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

		// Token: 0x06006700 RID: 26368 RVA: 0x00046EBA File Offset: 0x000450BA
		internal SourceRef GetSourceRef(bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, this.ToCol, this.FromLine, this.ToLine, isStepStop);
		}

		// Token: 0x06006701 RID: 26369 RVA: 0x00046EE0 File Offset: 0x000450E0
		internal SourceRef GetSourceRef(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.ToCol, this.FromLine, to.ToLine, isStepStop);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x00046F06 File Offset: 0x00045106
		internal SourceRef GetSourceRefUpTo(Token to, bool isStepStop = true)
		{
			return new SourceRef(this.SourceId, this.FromCol, to.PrevCol, this.FromLine, to.PrevLine, isStepStop);
		}

		// Token: 0x04005EE8 RID: 24296
		public readonly int SourceId;

		// Token: 0x04005EE9 RID: 24297
		public readonly int FromCol;

		// Token: 0x04005EEA RID: 24298
		public readonly int ToCol;

		// Token: 0x04005EEB RID: 24299
		public readonly int FromLine;

		// Token: 0x04005EEC RID: 24300
		public readonly int ToLine;

		// Token: 0x04005EED RID: 24301
		public readonly int PrevCol;

		// Token: 0x04005EEE RID: 24302
		public readonly int PrevLine;

		// Token: 0x04005EEF RID: 24303
		public readonly TokenType Type;
	}
}
