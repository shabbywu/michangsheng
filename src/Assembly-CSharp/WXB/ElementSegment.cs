using System;
using System.Collections.Generic;

namespace WXB;

public interface ElementSegment
{
	void Segment(string text, List<NodeBase.Element> widths, Func<char, float> fontwidth);
}
