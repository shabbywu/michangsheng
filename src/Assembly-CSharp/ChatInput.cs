using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x06000444 RID: 1092 RVA: 0x00017A60 File Offset: 0x00015C60
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

	// Token: 0x06000445 RID: 1093 RVA: 0x00017AF0 File Offset: 0x00015CF0
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

	// Token: 0x04000269 RID: 617
	public UITextList textList;

	// Token: 0x0400026A RID: 618
	public bool fillWithDummyData;

	// Token: 0x0400026B RID: 619
	private UIInput mInput;
}
