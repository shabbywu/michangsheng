using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x0200068E RID: 1678
	public interface ElementSegment
	{
		// Token: 0x06003524 RID: 13604
		void Segment(string text, List<NodeBase.Element> widths, Func<char, float> fontwidth);
	}
}
