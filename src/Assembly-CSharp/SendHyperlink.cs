using System;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x02000391 RID: 913
public class SendHyperlink : MonoBehaviour
{
	// Token: 0x06001E14 RID: 7700 RVA: 0x0005F32B File Offset: 0x0005D52B
	public void Send(string link)
	{
		TuJianManager.Inst.OnHyperlink(link);
	}
}
