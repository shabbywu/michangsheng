using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005B2 RID: 1458
public class UI_CDK : MonoBehaviour
{
	// Token: 0x060024CC RID: 9420 RVA: 0x0001D939 File Offset: 0x0001BB39
	private void Awake()
	{
		this.backgroud.SetActive(false);
	}

	// Token: 0x060024CD RID: 9421 RVA: 0x0001D947 File Offset: 0x0001BB47
	public void open()
	{
		this.backtype = 1;
		this.backgroud.SetActive(true);
	}

	// Token: 0x060024CE RID: 9422 RVA: 0x0001D95C File Offset: 0x0001BB5C
	public void close()
	{
		this.backtype = 0;
		this.backgroud.SetActive(false);
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x001299C4 File Offset: 0x00127BC4
	public void useCDK()
	{
		string text = this.CDK.text;
		Account account = (Account)KBEngineApp.app.player();
		if (account != null && this.stopTime == 0)
		{
			this.stopTime = 1;
			account.useCDK(text);
			base.Invoke("resetTime", 1f);
		}
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x0001D971 File Offset: 0x0001BB71
	public void resetTime()
	{
		this.stopTime = 0;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x0001D97A File Offset: 0x0001BB7A
	public void buttondwon()
	{
		if (this.backtype == 0)
		{
			this.open();
			return;
		}
		this.close();
	}

	// Token: 0x04001F8B RID: 8075
	public GameObject backgroud;

	// Token: 0x04001F8C RID: 8076
	public InputField CDK;

	// Token: 0x04001F8D RID: 8077
	private int stopTime;

	// Token: 0x04001F8E RID: 8078
	private int backtype;
}
