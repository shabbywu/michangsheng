using System;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003F7 RID: 1015
public class ReceiveJoineam : MonoBehaviour
{
	// Token: 0x060020BE RID: 8382 RVA: 0x000E6714 File Offset: 0x000E4914
	private void Start()
	{
		base.transform.Find("consent").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choiceconsent));
		base.transform.Find("reject").GetComponent<Button>().onClick.AddListener(new UnityAction(this.choicereject));
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000E6777 File Offset: 0x000E4977
	public void choiceconsent()
	{
		this.requestReceive(1);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000E678B File Offset: 0x000E498B
	public void choicereject()
	{
		this.requestReceive(0);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x000E67A0 File Offset: 0x000E49A0
	public void requestReceive(int choice)
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.requestReceiveTeam((ushort)choice, this.teamUUID);
		}
	}

	// Token: 0x04001AA3 RID: 6819
	public ulong teamUUID;

	// Token: 0x04001AA4 RID: 6820
	public ulong friendDBid;

	// Token: 0x04001AA5 RID: 6821
	public string friendName;
}
