using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ideafixxxer.CsvParser
{
	// Token: 0x02000DB7 RID: 3511
	public class CsvParser
	{
		// Token: 0x060063FB RID: 25595 RVA: 0x0027D040 File Offset: 0x0027B240
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

		// Token: 0x04005607 RID: 22023
		private const char CommaCharacter = ',';

		// Token: 0x04005608 RID: 22024
		private const char QuoteCharacter = '"';

		// Token: 0x020016A2 RID: 5794
		private abstract class ParserState
		{
			// Token: 0x060087B4 RID: 34740
			public abstract CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context);

			// Token: 0x060087B5 RID: 34741
			public abstract CsvParser.ParserState Comma(CsvParser.ParserContext context);

			// Token: 0x060087B6 RID: 34742
			public abstract CsvParser.ParserState Quote(CsvParser.ParserContext context);

			// Token: 0x060087B7 RID: 34743
			public abstract CsvParser.ParserState EndOfLine(CsvParser.ParserContext context);

			// Token: 0x04007346 RID: 29510
			public static readonly CsvParser.LineStartState LineStartState = new CsvParser.LineStartState();

			// Token: 0x04007347 RID: 29511
			public static readonly CsvParser.ValueStartState ValueStartState = new CsvParser.ValueStartState();

			// Token: 0x04007348 RID: 29512
			public static readonly CsvParser.ValueState ValueState = new CsvParser.ValueState();

			// Token: 0x04007349 RID: 29513
			public static readonly CsvParser.QuotedValueState QuotedValueState = new CsvParser.QuotedValueState();

			// Token: 0x0400734A RID: 29514
			public static readonly CsvParser.QuoteState QuoteState = new CsvParser.QuoteState();
		}

		// Token: 0x020016A3 RID: 5795
		private class LineStartState : CsvParser.ParserState
		{
			// Token: 0x060087BA RID: 34746 RVA: 0x002E7461 File Offset: 0x002E5661
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x060087BB RID: 34747 RVA: 0x002E746F File Offset: 0x002E566F
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x060087BC RID: 34748 RVA: 0x002E747C File Offset: 0x002E567C
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x060087BD RID: 34749 RVA: 0x002E7483 File Offset: 0x002E5683
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020016A4 RID: 5796
		private class ValueStartState : CsvParser.LineStartState
		{
			// Token: 0x060087BF RID: 34751 RVA: 0x002E7498 File Offset: 0x002E5698
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020016A5 RID: 5797
		private class ValueState : CsvParser.ParserState
		{
			// Token: 0x060087C1 RID: 34753 RVA: 0x002E7461 File Offset: 0x002E5661
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x060087C2 RID: 34754 RVA: 0x002E746F File Offset: 0x002E566F
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x060087C3 RID: 34755 RVA: 0x002E74B3 File Offset: 0x002E56B3
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				context.AddChar('"');
				return CsvParser.ParserState.ValueState;
			}

			// Token: 0x060087C4 RID: 34756 RVA: 0x002E7498 File Offset: 0x002E5698
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020016A6 RID: 5798
		private class QuotedValueState : CsvParser.ParserState
		{
			// Token: 0x060087C6 RID: 34758 RVA: 0x002E74C2 File Offset: 0x002E56C2
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x060087C7 RID: 34759 RVA: 0x002E74D0 File Offset: 0x002E56D0
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddChar(',');
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x060087C8 RID: 34760 RVA: 0x002E74DF File Offset: 0x002E56DF
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				return CsvParser.ParserState.QuoteState;
			}

			// Token: 0x060087C9 RID: 34761 RVA: 0x002E74E6 File Offset: 0x002E56E6
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddChar('\r');
				context.AddChar('\n');
				return CsvParser.ParserState.QuotedValueState;
			}
		}

		// Token: 0x020016A7 RID: 5799
		private class QuoteState : CsvParser.ParserState
		{
			// Token: 0x060087CB RID: 34763 RVA: 0x002E74C2 File Offset: 0x002E56C2
			public override CsvParser.ParserState AnyChar(char ch, CsvParser.ParserContext context)
			{
				context.AddChar(ch);
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x060087CC RID: 34764 RVA: 0x002E746F File Offset: 0x002E566F
			public override CsvParser.ParserState Comma(CsvParser.ParserContext context)
			{
				context.AddValue();
				return CsvParser.ParserState.ValueStartState;
			}

			// Token: 0x060087CD RID: 34765 RVA: 0x002E74FD File Offset: 0x002E56FD
			public override CsvParser.ParserState Quote(CsvParser.ParserContext context)
			{
				context.AddChar('"');
				return CsvParser.ParserState.QuotedValueState;
			}

			// Token: 0x060087CE RID: 34766 RVA: 0x002E7498 File Offset: 0x002E5698
			public override CsvParser.ParserState EndOfLine(CsvParser.ParserContext context)
			{
				context.AddValue();
				context.AddLine();
				return CsvParser.ParserState.LineStartState;
			}
		}

		// Token: 0x020016A8 RID: 5800
		private class ParserContext
		{
			// Token: 0x060087D0 RID: 34768 RVA: 0x002E750C File Offset: 0x002E570C
			public void AddChar(char ch)
			{
				this._currentValue.Append(ch);
			}

			// Token: 0x060087D1 RID: 34769 RVA: 0x002E751B File Offset: 0x002E571B
			public void AddValue()
			{
				this._currentLine.Add(this._currentValue.ToString());
				this._currentValue.Remove(0, this._currentValue.Length);
			}

			// Token: 0x060087D2 RID: 34770 RVA: 0x002E754B File Offset: 0x002E574B
			public void AddLine()
			{
				this._lines.Add(this._currentLine.ToArray());
				this._currentLine.Clear();
			}

			// Token: 0x060087D3 RID: 34771 RVA: 0x002E756E File Offset: 0x002E576E
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

			// Token: 0x0400734B RID: 29515
			private readonly StringBuilder _currentValue = new StringBuilder();

			// Token: 0x0400734C RID: 29516
			private readonly List<string[]> _lines = new List<string[]>();

			// Token: 0x0400734D RID: 29517
			private readonly List<string> _currentLine = new List<string>();
		}
	}
}
