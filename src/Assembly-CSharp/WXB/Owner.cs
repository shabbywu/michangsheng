using System;
using UnityEngine;

namespace WXB;

public interface Owner
{
	int minLineHeight { get; set; }

	Around around { get; }

	RenderCache renderCache { get; }

	Anchor anchor { get; }

	ElementSegment elementSegment { get; }

	Material material { get; }

	LineAlignment lineAlignment { get; }

	void SetRenderDirty();

	Draw GetDraw(DrawType type, long key, Action<Draw, object> onCreate, object para = null);
}
