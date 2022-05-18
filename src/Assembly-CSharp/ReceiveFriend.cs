using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005A6 RID: 1446
public class ReceiveFriend : MonoBehaviour
{
	// Token: 0x0600246B RID: 9323 RVA: 0x001287BC File Offset: 0x001269BC
	private void Start()
	{
		base.transform.Find("consent").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choiceconsent));
		base.transform.Find("reject").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choicereject));
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x0001D52D File Offset: 0x0001B72D
	public void choiceconsent()
	{
		this.requestReceive(1);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x0001D541 File Offset: 0x0001B741
	public void choicereject()
	{
		this.requestReceive(0);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x00128820 File Offset: 0x00126A20
	public void requestReceive(int choice)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestReceive((ushort)choice, this.friendDbid);
		}
	}

	// Token: 0x04001F5D RID: 8029
	public ulong friendDbid;

	// Token: 0x04001F5E RID: 8030
	public string friendName;
}
