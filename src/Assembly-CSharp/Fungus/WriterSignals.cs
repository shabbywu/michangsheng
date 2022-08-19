using System;

namespace Fungus
{
	// Token: 0x02000EBB RID: 3771
	public static class WriterSignals
	{
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06006A8D RID: 27277 RVA: 0x0029348C File Offset: 0x0029168C
		// (remove) Token: 0x06006A8E RID: 27278 RVA: 0x002934C0 File Offset: 0x002916C0
		public static event WriterSignals.TextTagTokenHandler OnTextTagToken;

		// Token: 0x06006A8F RID: 27279 RVA: 0x002934F3 File Offset: 0x002916F3
		public static void DoTextTagToken(Writer writer, TextTagToken token, int index, int maxIndex)
		{
			if (WriterSignals.OnTextTagToken != null)
			{
				WriterSignals.OnTextTagToken(writer, token, index, maxIndex);
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06006A90 RID: 27280 RVA: 0x0029350C File Offset: 0x0029170C
		// (remove) Token: 0x06006A91 RID: 27281 RVA: 0x00293540 File Offset: 0x00291740
		public static event WriterSignals.WriterStateHandler OnWriterState;

		// Token: 0x06006A92 RID: 27282 RVA: 0x00293573 File Offset: 0x00291773
		public static void DoWriterState(Writer writer, WriterState writerState)
		{
			if (WriterSignals.OnWriterState != null)
			{
				WriterSignals.OnWriterState(writer, writerState);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06006A93 RID: 27283 RVA: 0x00293588 File Offset: 0x00291788
		// (remove) Token: 0x06006A94 RID: 27284 RVA: 0x002935BC File Offset: 0x002917BC
		public static event WriterSignals.WriterInputHandler OnWriterInput;

		// Token: 0x06006A95 RID: 27285 RVA: 0x002935EF File Offset: 0x002917EF
		public static void DoWriterInput(Writer writer)
		{
			if (WriterSignals.OnWriterInput != null)
			{
				WriterSignals.OnWriterInput(writer);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06006A96 RID: 27286 RVA: 0x00293604 File Offset: 0x00291804
		// (remove) Token: 0x06006A97 RID: 27287 RVA: 0x00293638 File Offset: 0x00291838
		public static event WriterSignals.WriterGlyphHandler OnWriterGlyph;

		// Token: 0x06006A98 RID: 27288 RVA: 0x0029366B File Offset: 0x0029186B
		public static void DoWriterGlyph(Writer writer)
		{
			if (WriterSignals.OnWriterGlyph != null)
			{
				WriterSignals.OnWriterGlyph(writer);
			}
		}

		// Token: 0x02001708 RID: 5896
		// (Invoke) Token: 0x060088D4 RID: 35028
		public delegate void TextTagTokenHandler(Writer writer, TextTagToken token, int index, int maxIndex);

		// Token: 0x02001709 RID: 5897
		// (Invoke) Token: 0x060088D8 RID: 35032
		public delegate void WriterStateHandler(Writer writer, WriterState writerState);

		// Token: 0x0200170A RID: 5898
		// (Invoke) Token: 0x060088DC RID: 35036
		public delegate void WriterInputHandler(Writer writer);

		// Token: 0x0200170B RID: 5899
		// (Invoke) Token: 0x060088E0 RID: 35040
		public delegate void WriterGlyphHandler(Writer writer);
	}
}
