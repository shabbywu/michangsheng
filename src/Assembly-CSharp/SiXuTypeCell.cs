using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039F RID: 927
public class SiXuTypeCell : MonoBehaviour
{
	// Token: 0x06001E50 RID: 7760 RVA: 0x000D5975 File Offset: 0x000D3B75
	public void setContent(string content, string outTime)
	{
		this.SiXuContent.text = content;
		this.OutTime.text = outTime;
	}

	// Token: 0x040018DA RID: 6362
	[SerializeField]
	private Text SiXuContent;

	// Token: 0x040018DB RID: 6363
	[SerializeField]
	private Text OutTime;
}
