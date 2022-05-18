using System;

namespace Fungus
{
	// Token: 0x0200134F RID: 4943
	public static class WriterSignals
	{
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060077F6 RID: 30710 RVA: 0x002B5784 File Offset: 0x002B3984
		// (remove) Token: 0x060077F7 RID: 30711 RVA: 0x002B57B8 File Offset: 0x002B39B8
		public static event WriterSignals.TextTagTokenHandler OnTextTagToken;

		// Token: 0x060077F8 RID: 30712 RVA: 0x00051A01 File Offset: 0x0004FC01
		public static void DoTextTagToken(Writer writer, TextTagToken token, int index, int maxIndex)
		{
			if (WriterSignals.OnTextTagToken != null)
			{
				WriterSignals.OnTextTagToken(writer, token, index, maxIndex);
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060077F9 RID: 30713 RVA: 0x002B57EC File Offset: 0x002B39EC
		// (remove) Token: 0x060077FA RID: 30714 RVA: 0x002B5820 File Offset: 0x002B3A20
		public static event WriterSignals.WriterStateHandler OnWriterState;

		// Token: 0x060077FB RID: 30715 RVA: 0x00051A18 File Offset: 0x0004FC18
		public static void DoWriterState(Writer writer, WriterState writerState)
		{
			if (WriterSignals.OnWriterState != null)
			{
				WriterSignals.OnWriterState(writer, writerState);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060077FC RID: 30716 RVA: 0x002B5854 File Offset: 0x002B3A54
		// (remove) Token: 0x060077FD RID: 30717 RVA: 0x002B5888 File Offset: 0x002B3A88
		public static event WriterSignals.WriterInputHandler OnWriterInput;

		// Token: 0x060077FE RID: 30718 RVA: 0x00051A2D File Offset: 0x0004FC2D
		public static void DoWriterInput(Writer writer)
		{
			if (WriterSignals.OnWriterInput != null)
			{
				WriterSignals.OnWriterInput(writer);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060077FF RID: 30719 RVA: 0x002B58BC File Offset: 0x002B3ABC
		// (remove) Token: 0x06007800 RID: 30720 RVA: 0x002B58F0 File Offset: 0x002B3AF0
		public static event WriterSignals.WriterGlyphHandler OnWriterGlyph;

		// Token: 0x06007801 RID: 30721 RVA: 0x00051A41 File Offset: 0x0004FC41
		public static void DoWriterGlyph(Writer writer)
		{
			if (WriterSignals.OnWriterGlyph != null)
			{
				WriterSignals.OnWriterGlyph(writer);
			}
		}

		// Token: 0x02001350 RID: 4944
		// (Invoke) Token: 0x06007803 RID: 30723
		public delegate void TextTagTokenHandler(Writer writer, TextTagToken token, int index, int maxIndex);

		// Token: 0x02001351 RID: 4945
		// (Invoke) Token: 0x06007807 RID: 30727
		public delegate void WriterStateHandler(Writer writer, WriterState writerState);

		// Token: 0x02001352 RID: 4946
		// (Invoke) Token: 0x0600780B RID: 30731
		public delegate void WriterInputHandler(Writer writer);

		// Token: 0x02001353 RID: 4947
		// (Invoke) Token: 0x0600780F RID: 30735
		public delegate void WriterGlyphHandler(Writer writer);
	}
}
