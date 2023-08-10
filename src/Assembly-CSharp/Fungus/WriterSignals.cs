namespace Fungus;

public static class WriterSignals
{
	public delegate void TextTagTokenHandler(Writer writer, TextTagToken token, int index, int maxIndex);

	public delegate void WriterStateHandler(Writer writer, WriterState writerState);

	public delegate void WriterInputHandler(Writer writer);

	public delegate void WriterGlyphHandler(Writer writer);

	public static event TextTagTokenHandler OnTextTagToken;

	public static event WriterStateHandler OnWriterState;

	public static event WriterInputHandler OnWriterInput;

	public static event WriterGlyphHandler OnWriterGlyph;

	public static void DoTextTagToken(Writer writer, TextTagToken token, int index, int maxIndex)
	{
		if (WriterSignals.OnTextTagToken != null)
		{
			WriterSignals.OnTextTagToken(writer, token, index, maxIndex);
		}
	}

	public static void DoWriterState(Writer writer, WriterState writerState)
	{
		if (WriterSignals.OnWriterState != null)
		{
			WriterSignals.OnWriterState(writer, writerState);
		}
	}

	public static void DoWriterInput(Writer writer)
	{
		if (WriterSignals.OnWriterInput != null)
		{
			WriterSignals.OnWriterInput(writer);
		}
	}

	public static void DoWriterGlyph(Writer writer)
	{
		if (WriterSignals.OnWriterGlyph != null)
		{
			WriterSignals.OnWriterGlyph(writer);
		}
	}
}
