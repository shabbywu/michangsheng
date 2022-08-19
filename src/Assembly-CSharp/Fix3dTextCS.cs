using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class Fix3dTextCS : MonoBehaviour
{
	// Token: 0x06000A94 RID: 2708 RVA: 0x00040535 File Offset: 0x0003E735
	private void Start()
	{
		base.gameObject.GetComponent<TextMesh>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x0400068B RID: 1675
	public string text;

	// Token: 0x0400068C RID: 1676
	public bool tashkeel = true;

	// Token: 0x0400068D RID: 1677
	public bool hinduNumbers = true;
}
