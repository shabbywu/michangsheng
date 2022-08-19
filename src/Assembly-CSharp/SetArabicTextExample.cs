using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class SetArabicTextExample : MonoBehaviour
{
	// Token: 0x06000A9A RID: 2714 RVA: 0x000405B3 File Offset: 0x0003E7B3
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = "This sentence (wrong display):\n" + this.text + "\n\nWill appear correctly as:\n" + ArabicFixer.Fix(this.text, false, false);
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000691 RID: 1681
	public string text;
}
