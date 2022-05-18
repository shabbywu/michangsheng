using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000560 RID: 1376
public class loadingTips : MonoBehaviour
{
	// Token: 0x06002323 RID: 8995 RVA: 0x00121FD0 File Offset: 0x001201D0
	private void Start()
	{
		int index = jsonData.GetRandom() % this.Tips.Count;
		this.text.text = this.Tips[index];
	}

	// Token: 0x04001E3E RID: 7742
	public List<string> Tips;

	// Token: 0x04001E3F RID: 7743
	public Text text;
}
