using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival
{
	// Token: 0x02000908 RID: 2312
	public static class GUIUtils
	{
		// Token: 0x06003B22 RID: 15138 RVA: 0x001AB544 File Offset: 0x001A9744
		public static Text CreateTextUnder(string name, RectTransform parent, TextAnchor anchor, Vector2 offset)
		{
			Text component = new GameObject(name, new Type[]
			{
				typeof(Text)
			}).GetComponent<Text>();
			component.transform.SetParent(parent);
			component.transform.localPosition = offset;
			component.transform.localScale = Vector3.one;
			component.rectTransform.pivot = Vector2.one * 0.5f;
			component.rectTransform.sizeDelta = parent.sizeDelta;
			component.alignment = anchor;
			return component;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x001AB5D0 File Offset: 0x001A97D0
		public static Image CreateImageUnder(string name, RectTransform parent, Vector2 offset, Vector2 size)
		{
			Image component = new GameObject(name, new Type[]
			{
				typeof(Image)
			}).GetComponent<Image>();
			component.transform.SetParent(parent);
			component.transform.localPosition = offset;
			component.transform.localScale = Vector3.one;
			component.rectTransform.pivot = Vector2.one * 0.5f;
			component.rectTransform.sizeDelta = size;
			component.raycastTarget = false;
			return component;
		}
	}
}
