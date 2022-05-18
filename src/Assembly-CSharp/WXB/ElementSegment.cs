using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009A0 RID: 2464
	public interface ElementSegment
	{
		// Token: 0x06003EDF RID: 16095
		void Segment(string text, List<NodeBase.Element> widths, Func<char, float> fontwidth);
	}
}
