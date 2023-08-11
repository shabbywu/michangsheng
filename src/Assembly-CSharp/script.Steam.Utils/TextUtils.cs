using UnityEngine;
using UnityEngine.UI;

namespace script.Steam.Utils;

public static class TextUtils
{
	public static void SetTextWithEllipsis(this Text obj, string value)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (value == null)
		{
			obj.text = "";
			return;
		}
		TextGenerator val = new TextGenerator();
		RectTransform component = ((Component)obj).GetComponent<RectTransform>();
		Rect rect = component.rect;
		TextGenerationSettings generationSettings = obj.GetGenerationSettings(((Rect)(ref rect)).size);
		val.Populate(value, generationSettings);
		int characterCountVisible = val.characterCountVisible;
		string text = value;
		if (value.Length > characterCountVisible)
		{
			text = value.Substring(0, characterCountVisible - 3);
			text += "…";
		}
		obj.text = text;
	}
}
