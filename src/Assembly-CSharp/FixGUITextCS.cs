using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class FixGUITextCS : MonoBehaviour
{
	// Token: 0x06000A97 RID: 2711 RVA: 0x00040574 File Offset: 0x0003E774
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x0400068E RID: 1678
	public string text;

	// Token: 0x0400068F RID: 1679
	public bool tashkeel = true;

	// Token: 0x04000690 RID: 1680
	public bool hinduNumbers = true;
}
