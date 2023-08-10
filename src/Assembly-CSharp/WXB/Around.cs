using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public class Around
{
	private List<Rect> m_Rects = new List<Rect>();

	public void Add(Rect rect)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		m_Rects.Add(rect);
	}

	public void Clear()
	{
		m_Rects.Clear();
	}

	public bool isContain(Rect rect, out float ox)
	{
		if (m_Rects.Count == 0)
		{
			ox = 0f;
			return true;
		}
		return isContain(((Rect)(ref rect)).x, ((Rect)(ref rect)).y, ((Rect)(ref rect)).width, ((Rect)(ref rect)).height, out ox);
	}

	public bool isContain(float x, float y, float w, float h, out float ox)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (m_Rects.Count == 0)
		{
			ox = 0f;
			return true;
		}
		Rect val = default(Rect);
		((Rect)(ref val))._002Ector(x, y, w, h);
		for (int i = 0; i < m_Rects.Count; i++)
		{
			Rect val2 = m_Rects[i];
			if (((Rect)(ref val2)).Overlaps(val))
			{
				val2 = m_Rects[i];
				ox = ((Rect)(ref val2)).xMax + 5f;
				return false;
			}
		}
		ox = 0f;
		return true;
	}
}
