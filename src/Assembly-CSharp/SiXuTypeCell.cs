using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000528 RID: 1320
public class SiXuTypeCell : MonoBehaviour
{
	// Token: 0x060021D1 RID: 8657 RVA: 0x0001BC65 File Offset: 0x00019E65
	public void setContent(string content, string outTime)
	{
		this.SiXuContent.text = content;
		this.OutTime.text = outTime;
	}

	// Token: 0x04001D43 RID: 7491
	[SerializeField]
	private Text SiXuContent;

	// Token: 0x04001D44 RID: 7492
	[SerializeField]
	private Text OutTime;
}
