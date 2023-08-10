using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ideafixxxer.CsvParser;

public class CsvParser
{
	private abstract class ParserState
	{
		public static readonly LineStartState LineStartState = new LineStartState();

		public static readonly ValueStartState ValueStartState = new ValueStartState();

		public static readonly ValueState ValueState = new ValueState();

		public static readonly QuotedValueState QuotedValueState = new QuotedValueState();

		public static readonly QuoteState QuoteState = new QuoteState();

		public abstract ParserState AnyChar(char ch, ParserContext context);

		public abstract ParserState Comma(ParserContext context);

		public abstract ParserState Quote(ParserContext context);

		public abstract ParserState EndOfLine(ParserContext context);
	}

	private class LineStartState : ParserState
	{
		public override ParserState AnyChar(char ch, ParserContext context)
		{
			context.AddChar(ch);
			return ParserState.ValueState;
		}

		public override ParserState Comma(ParserContext context)
		{
			context.AddValue();
			return ParserState.ValueStartState;
		}

		public override ParserState Quote(ParserContext context)
		{
			return ParserState.QuotedValueState;
		}

		public override ParserState EndOfLine(ParserContext context)
		{
			context.AddLine();
			return ParserState.LineStartState;
		}
	}

	private class ValueStartState : LineStartState
	{
		public override ParserState EndOfLine(ParserContext context)
		{
			context.AddValue();
			context.AddLine();
			return ParserState.LineStartState;
		}
	}

	private class ValueState : ParserState
	{
		public override ParserState AnyChar(char ch, ParserContext context)
		{
			context.AddChar(ch);
			return ParserState.ValueState;
		}

		public override ParserState Comma(ParserContext context)
		{
			context.AddValue();
			return ParserState.ValueStartState;
		}

		public override ParserState Quote(ParserContext context)
		{
			context.AddChar('"');
			return ParserState.ValueState;
		}

		public override ParserState EndOfLine(ParserContext context)
		{
			context.AddValue();
			context.AddLine();
			return ParserState.LineStartState;
		}
	}

	private class QuotedValueState : ParserState
	{
		public override ParserState AnyChar(char ch, ParserContext context)
		{
			context.AddChar(ch);
			return ParserState.QuotedValueState;
		}

		public override ParserState Comma(ParserContext context)
		{
			context.AddChar(',');
			return ParserState.QuotedValueState;
		}

		public override ParserState Quote(ParserContext context)
		{
			return ParserState.QuoteState;
		}

		public override ParserState EndOfLine(ParserContext context)
		{
			context.AddChar('\r');
			context.AddChar('\n');
			return ParserState.QuotedValueState;
		}
	}

	private class QuoteState : ParserState
	{
		public override ParserState AnyChar(char ch, ParserContext context)
		{
			context.AddChar(ch);
			return ParserState.QuotedValueState;
		}

		public override ParserState Comma(ParserContext context)
		{
			context.AddValue();
			return ParserState.ValueStartState;
		}

		public override ParserState Quote(ParserContext context)
		{
			context.AddChar('"');
			return ParserState.QuotedValueState;
		}

		public override ParserState EndOfLine(ParserContext context)
		{
			context.AddValue();
			context.AddLine();
			return ParserState.LineStartState;
		}
	}

	private class ParserContext
	{
		private readonly StringBuilder _currentValue = new StringBuilder();

		private readonly List<string[]> _lines = new List<string[]>();

		private readonly List<string> _currentLine = new List<string>();

		public void AddChar(char ch)
		{
			_currentValue.Append(ch);
		}

		public void AddValue()
		{
			_currentLine.Add(_currentValue.ToString());
			_currentValue.Remove(0, _currentValue.Length);
		}

		public void AddLine()
		{
			_lines.Add(_currentLine.ToArray());
			_currentLine.Clear();
		}

		public List<string[]> GetAllLines()
		{
			if (_currentValue.Length > 0)
			{
				AddValue();
			}
			if (_currentLine.Count > 0)
			{
				AddLine();
			}
			return _lines;
		}
	}

	private const char CommaCharacter = ',';

	private const char QuoteCharacter = '"';

	public string[][] Parse(string csvData)
	{
		ParserContext parserContext = new ParserContext();
		string[] array = Regex.Split(csvData, "\n|\r\n");
		ParserState parserState = ParserState.LineStartState;
		foreach (string text in array)
		{
			if (text.Length != 0)
			{
				foreach (char c in text)
				{
					parserState = c switch
					{
						',' => parserState.Comma(parserContext), 
						'"' => parserState.Quote(parserContext), 
						_ => parserState.AnyChar(c, parserContext), 
					};
				}
				parserState = parserState.EndOfLine(parserContext);
			}
		}
		return parserContext.GetAllLines().ToArray();
	}
}
