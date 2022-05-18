using System;

namespace Fungus
{
	// Token: 0x0200136E RID: 4974
	public enum TokenType
	{
		// Token: 0x040068A4 RID: 26788
		Invalid,
		// Token: 0x040068A5 RID: 26789
		Words,
		// Token: 0x040068A6 RID: 26790
		BoldStart,
		// Token: 0x040068A7 RID: 26791
		BoldEnd,
		// Token: 0x040068A8 RID: 26792
		ItalicStart,
		// Token: 0x040068A9 RID: 26793
		ItalicEnd,
		// Token: 0x040068AA RID: 26794
		ColorStart,
		// Token: 0x040068AB RID: 26795
		ColorEnd,
		// Token: 0x040068AC RID: 26796
		SizeStart,
		// Token: 0x040068AD RID: 26797
		SizeEnd,
		// Token: 0x040068AE RID: 26798
		Wait,
		// Token: 0x040068AF RID: 26799
		WaitForInputNoClear,
		// Token: 0x040068B0 RID: 26800
		WaitForInputAndClear,
		// Token: 0x040068B1 RID: 26801
		WaitOnPunctuationStart,
		// Token: 0x040068B2 RID: 26802
		WaitOnPunctuationEnd,
		// Token: 0x040068B3 RID: 26803
		Clear,
		// Token: 0x040068B4 RID: 26804
		SpeedStart,
		// Token: 0x040068B5 RID: 26805
		SpeedEnd,
		// Token: 0x040068B6 RID: 26806
		Exit,
		// Token: 0x040068B7 RID: 26807
		Message,
		// Token: 0x040068B8 RID: 26808
		VerticalPunch,
		// Token: 0x040068B9 RID: 26809
		HorizontalPunch,
		// Token: 0x040068BA RID: 26810
		Punch,
		// Token: 0x040068BB RID: 26811
		Flash,
		// Token: 0x040068BC RID: 26812
		Audio,
		// Token: 0x040068BD RID: 26813
		AudioLoop,
		// Token: 0x040068BE RID: 26814
		AudioPause,
		// Token: 0x040068BF RID: 26815
		AudioStop,
		// Token: 0x040068C0 RID: 26816
		WaitForVoiceOver
	}
}
