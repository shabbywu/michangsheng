using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005A7 RID: 1447
public class ReceiveJoineam : MonoBehaviour
{
	// Token: 0x06002470 RID: 9328 RVA: 0x00128850 File Offset: 0x00126A50
	private void Start()
	{
		base.transform.Find("consent").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choiceconsent));
		base.transform.Find("reject").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choicereject));
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x0001D555 File Offset: 0x0001B755
	public void choiceconsent()
	{
		this.requestReceive(1);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x0001D569 File Offset: 0x0001B769
	public void choicereject()
	{
		this.requestReceive(0);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x001288B4 File Offset: 0x00126AB4
	public void requestReceive(int choice)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestReceiveTeam((ushort)choice, this.teamUUID);
		}
	}

	// Token: 0x04001F5F RID: 8031
	public ulong teamUUID;

	// Token: 0x04001F60 RID: 8032
	public ulong friendDBid;

	// Token: 0x04001F61 RID: 8033
	public string friendName;
}
