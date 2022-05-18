using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F0 RID: 1520
public class LingGuangCell : MonoBehaviour
{
	// Token: 0x06002624 RID: 9764 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x0001E723 File Offset: 0x0001C923
	public void init(string desc)
	{
		this.Desc.text = desc;
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x0001E731 File Offset: 0x0001C931
	public void Click()
	{
		Event.fireOut("ClickLingGuangCell", new object[]
		{
			this.Info
		});
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400209D RID: 8349
	public JSONObject Info;

	// Token: 0x0400209E RID: 8350
	public Text Desc;

	// Token: 0x0400209F RID: 8351
	public Text ShengYuTime;
}
