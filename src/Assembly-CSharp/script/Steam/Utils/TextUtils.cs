using System;
using UnityEngine;
using UnityEngine.UI;

namespace script.Steam.Utils
{
	// Token: 0x020009DD RID: 2525
	public static class TextUtils
	{
		// Token: 0x06004601 RID: 17921 RVA: 0x001DA1A4 File Offset: 0x001D83A4
		public static void SetTextWithEllipsis(this Text obj, string value)
		{
			if (value == null)
			{
				obj.text = "";
				return;
			}
			TextGenerator textGenerator = new TextGenerator();
			RectTransform component = obj.GetComponent<RectTransform>();
			TextGenerationSettings generationSettings = obj.GetGenerationSettings(component.rect.size);
			textGenerator.Populate(value, generationSettings);
			int characterCountVisible = textGenerator.characterCountVisible;
			string text = value;
			if (value.Length > characterCountVisible)
			{
				text = value.Substring(0, characterCountVisible - 3);
				text += "…";
			}
			obj.text = text;
		}
	}
}
