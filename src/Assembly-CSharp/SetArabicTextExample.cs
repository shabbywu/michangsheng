using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class SetArabicTextExample : MonoBehaviour
{
	// Token: 0x06000B77 RID: 2935 RVA: 0x0000D86C File Offset: 0x0000BA6C
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = "This sentence (wrong display):\n" + this.text + "\n\nWill appear correctly as:\n" + ArabicFixer.Fix(this.text, false, false);
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000835 RID: 2101
	public string text;
}
