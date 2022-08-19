using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003CB RID: 971
public class loadingTips : MonoBehaviour
{
	// Token: 0x06001FAC RID: 8108 RVA: 0x000DF550 File Offset: 0x000DD750
	private void Start()
	{
		int index = jsonData.GetRandom() % this.Tips.Count;
		this.text.text = this.Tips[index];
	}

	// Token: 0x040019BD RID: 6589
	public List<string> Tips;

	// Token: 0x040019BE RID: 6590
	public Text text;
}
