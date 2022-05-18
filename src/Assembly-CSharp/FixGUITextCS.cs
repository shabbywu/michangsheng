using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class FixGUITextCS : MonoBehaviour
{
	// Token: 0x06000B74 RID: 2932 RVA: 0x0000D82D File Offset: 0x0000BA2D
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000832 RID: 2098
	public string text;

	// Token: 0x04000833 RID: 2099
	public bool tashkeel = true;

	// Token: 0x04000834 RID: 2100
	public bool hinduNumbers = true;
}
