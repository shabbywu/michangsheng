using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000402 RID: 1026
public class UI_CDK : MonoBehaviour
{
	// Token: 0x0600211A RID: 8474 RVA: 0x000E7C6F File Offset: 0x000E5E6F
	private void Awake()
	{
		this.backgroud.SetActive(false);
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x000E7C7D File Offset: 0x000E5E7D
	public void open()
	{
		this.backtype = 1;
		this.backgroud.SetActive(true);
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x000E7C92 File Offset: 0x000E5E92
	public void close()
	{
		this.backtype = 0;
		this.backgroud.SetActive(false);
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x000E7CA8 File Offset: 0x000E5EA8
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

	// Token: 0x0600211E RID: 8478 RVA: 0x000E7CFA File Offset: 0x000E5EFA
	public void resetTime()
	{
		this.stopTime = 0;
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x000E7D03 File Offset: 0x000E5F03
	public void buttondwon()
	{
		if (this.backtype == 0)
		{
			this.open();
			return;
		}
		this.close();
	}

	// Token: 0x04001ACF RID: 6863
	public GameObject backgroud;

	// Token: 0x04001AD0 RID: 6864
	public InputField CDK;

	// Token: 0x04001AD1 RID: 6865
	private int stopTime;

	// Token: 0x04001AD2 RID: 6866
	private int backtype;
}
