using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003F6 RID: 1014
public class ReceiveFriend : MonoBehaviour
{
	// Token: 0x060020B9 RID: 8377 RVA: 0x000E6658 File Offset: 0x000E4858
	private void Start()
	{
		base.transform.Find("consent").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choiceconsent));
		base.transform.Find("reject").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choicereject));
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x000E66BB File Offset: 0x000E48BB
	public void choiceconsent()
	{
		this.requestReceive(1);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x000E66CF File Offset: 0x000E48CF
	public void choicereject()
	{
		this.requestReceive(0);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x000E66E4 File Offset: 0x000E48E4
	public void requestReceive(int choice)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestReceive((ushort)choice, this.friendDbid);
		}
	}

	// Token: 0x04001AA1 RID: 6817
	public ulong friendDbid;

	// Token: 0x04001AA2 RID: 6818
	public string friendName;
}
