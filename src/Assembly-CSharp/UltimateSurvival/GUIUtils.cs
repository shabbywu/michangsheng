using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival
{
	// Token: 0x02000621 RID: 1569
	public static class GUIUtils
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x00161C94 File Offset: 0x0015FE94
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

		// Token: 0x060031EF RID: 12783 RVA: 0x00161D20 File Offset: 0x0015FF20
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
