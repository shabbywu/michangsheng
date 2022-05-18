using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x0600048C RID: 1164 RVA: 0x0006EF08 File Offset: 0x0006D108
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 == 0) ? "[FFFFFF]" : "[AAAAAA]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0006EF98 File Offset: 0x0006D198
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = "";
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x040002D5 RID: 725
	public UITextList textList;

	// Token: 0x040002D6 RID: 726
	public bool fillWithDummyData;

	// Token: 0x040002D7 RID: 727
	private UIInput mInput;
}
