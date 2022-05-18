using System;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x02000516 RID: 1302
public class SendHyperlink : MonoBehaviour
{
	// Token: 0x0600218F RID: 8591 RVA: 0x000118F6 File Offset: 0x0000FAF6
	public void Send(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}
}
