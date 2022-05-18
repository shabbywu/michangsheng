using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class Fix3dTextCS : MonoBehaviour
{
	// Token: 0x06000B71 RID: 2929 RVA: 0x0000D7EE File Offset: 0x0000B9EE
	private void Start()
	{
		base.gameObject.GetComponent<TextMesh>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400082F RID: 2095
	public string text;

	// Token: 0x04000830 RID: 2096
	public bool tashkeel = true;

	// Token: 0x04000831 RID: 2097
	public bool hinduNumbers = true;
}
