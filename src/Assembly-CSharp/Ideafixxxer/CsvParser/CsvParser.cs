using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ideafixxxer.CsvParser
{
	// Token: 0x020011E5 RID: 4581
	public class CsvParser
	{
		// Token: 0x06007045 RID: 28741 RVA: 0x002A22C8 File Offset: 0x002A04C8
		public string[][] Parse(string csvData)
		{
			CsvParser.ParserContext parserContext = new CsvParser.ParserContext();
			string[] array = Regex.Split(csvData, "\n|\r\n");
			CsvParser.ParserState parserState = CsvParser.ParserState.LineStartState;
			foreach (string text in array)
			{
				if (text.Length != 0)
				{
					foreach (char c in text)
					{
						if (c != '"')
						{
							if (c == ',')
							{
								parserState = parserState.Comma(parserContext);
							}
							else
							{
								parserState = parserState.AnyChar(c, parserContext);
							}
						}
						else
						{
							parserState = parserState.Quote(parserContext);
						}
					}
					parserState = parserState.EndOfLine(parserContext);
				}
			}
			return parserContext.GetAllLines().ToArray();
		}

		// Token: 0x040062F2 RID: 25330
		private const char CommaCharacter = ',';

		// Token: 0x040062F3 RID: 25331
		private const char QuoteCharacter = '"';

		// Token: 0x020011E6 RID: 4582
		private abstract class ParserState
		{
			// Token: 0x06007047 RID: 28743
			public abstract CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context);

			// Token: 0x06007048 RID: 28744
			public abstract CsvParser.ParserState Comma(CsvParser.ParserContext context);

			// Token: 0x06007049 RID: 28745
			public abstract CsvParser.ParserState Quote(CsvParser.ParserContext context);

			// Token: 0x0600704A RID: 28746
			public abstract CsvParser.ParserState EndOfLine(CsvParser.ParserContext context);

			// Token: 0x040062F4 RID: 25332
			public static readonly CsvParser.LineStartState LineStartState = new CsvParser.LineStartState();

			// Token: 0x040062F5 RID: 25333
			public static readonly CsvParser.ValueStartState ValueStartState = new CsvParser.ValueStartState();

			// Token: 0x040062F6 RID: 25334
			public static readonly CsvParser.ValueState ValueState = new CsvParser.ValueState();

			// Token: 0x040062F7 RID: 25335
			public static readonly CsvParser.QuotedValueState QuotedValueState = new CsvParser.QuotedValueState();

			// Token: 0x040062F8 RID: 25336
			public static readonly CsvParser.QuoteState QuoteState = new CsvParser.QuoteState();
		}

		// Token: 0x020011E7 RID: 4583
		private class LineStartState : CsvParser.ParserState
		{
			// Token: 0x0600704D RID: 28749 RVA: 0x0004C43D File Offset: 0x0004A63D
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x0600704E RID: 28750 RVA: 0x0004C44B File Offset: 0x0004A64B
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x0600704F RID: 28751 RVA: 0x0004C458 File Offset: 0x0004A658
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x06007050 RID: 28752 RVA: 0x0004C45F File Offset: 0x0004A65F
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020011E8 RID: 4584
		private class ValueStartState : CsvParser.LineStartState
		{
			// Token: 0x06007052 RID: 28754 RVA: 0x0004C474 File Offset: 0x0004A674
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020011E9 RID: 4585
		private class ValueState : CsvParser.ParserState
		{
			// Token: 0x06007054 RID: 28756 RVA: 0x0004C43D File Offset: 0x0004A63D
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x06007055 RID: 28757 RVA: 0x0004C44B File Offset: 0x0004A64B
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x06007056 RID: 28758 RVA: 0x0004C48F File Offset: 0x0004A68F
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				context.AddChar('"');
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x06007057 RID: 28759 RVA: 0x0004C474 File Offset: 0x0004A674
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020011EA RID: 4586
		private class QuotedValueState : CsvParser.ParserState
		{
			// Token: 0x06007059 RID: 28761 RVA: 0x0004C49E File Offset: 0x0004A69E
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x0600705A RID: 28762 RVA: 0x0004C4AC File Offset: 0x0004A6AC
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddChar(',');
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x0600705B RID: 28763 RVA: 0x0004C4BB File Offset: 0x0004A6BB
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				return CsvParser.ParserState.QuoteState;
			}

			// Token: 0x0600705C RID: 28764 RVA: 0x0004C4C2 File Offset: 0x0004A6C2
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddChar('\r');
				context.AddChar('\n');
				return CsvParser.ParserState.QuotedValueState;
			}
		}

		// Token: 0x020011EB RID: 4587
		private class QuoteState : CsvParser.ParserState
		{
			// Token: 0x0600705E RID: 28766 RVA: 0x0004C49E File Offset: 0x0004A69E
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x0600705F RID: 28767 RVA: 0x0004C44B File Offset: 0x0004A64B
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x06007060 RID: 28768 RVA: 0x0004C4D9 File Offset: 0x0004A6D9
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				context.AddChar('"');
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x06007061 RID: 28769 RVA: 0x0004C474 File Offset: 0x0004A674
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020011EC RID: 4588
		private class ParserContext
		{
			// Token: 0x06007063 RID: 28771 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
			public void AddChar(char ch)
			{
				this._currentValue.Append(ch);
			}

			// Token: 0x06007064 RID: 28772 RVA: 0x0004C4F7 File Offset: 0x0004A6F7
			public void AddValue()
			{
				this._currentLine.Add(this._currentValue.ToString());
				this._currentValue.Remove(0, this._currentValue.Length);
			}

			// Token: 0x06007065 RID: 28773 RVA: 0x0004C527 File Offset: 0x0004A727
			public void AddLine()
			{
				this._lines.Add(this._currentLine.ToArray());
				this._currentLine.Clear();
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x0004C54A File Offset: 0x0004A74A
			public List<string[]> GetAllLines()
			{
				if (this._currentValue.Length > 0)
				{
					this.AddValue();
				}
				if (this._currentLine.Count > 0)
				{
					this.AddLine();
				}
				return this._lines;
			}

			// Token: 0x040062F9 RID: 25337
			private readonly StringBuilder _currentValue = new StringBuilder();

			// Token: 0x040062FA RID: 25338
			private readonly List<string[]> _lines = new List<string[]>();

			// Token: 0x040062FB RID: 25339
			private readonly List<string> _currentLine = new List<string>();
		}
	}
}
