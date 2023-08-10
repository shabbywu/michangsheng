using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival;

public static class GUIUtils
{
	public static Text CreateTextUnder(string name, RectTransform parent, TextAnchor anchor, Vector2 offset)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		Text component = new GameObject(name, new Type[1] { typeof(Text) }).GetComponent<Text>();
		((Component)component).transform.SetParent((Transform)(object)parent);
		((Component)component).transform.localPosition = Vector2.op_Implicit(offset);
		((Component)component).transform.localScale = Vector3.one;
		((Graphic)component).rectTransform.pivot = Vector2.one * 0.5f;
		((Graphic)component).rectTransform.sizeDelta = parent.sizeDelta;
		component.alignment = anchor;
		return component;
	}

	public static Image CreateImageUnder(string name, RectTransform parent, Vector2 offset, Vector2 size)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Image component = new GameObject(name, new Type[1] { typeof(Image) }).GetComponent<Image>();
		((Component)component).transform.SetParent((Transform)(object)parent);
		((Component)component).transform.localPosition = Vector2.op_Implicit(offset);
		((Component)component).transform.localScale = Vector3.one;
		((Graphic)component).rectTransform.pivot = Vector2.one * 0.5f;
		((Graphic)component).rectTransform.sizeDelta = size;
		((Graphic)component).raycastTarget = false;
		return component;
	}
}
