using System;
using Fungus;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class talkCompont : MonoBehaviour
{
	// Token: 0x06001498 RID: 5272 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x00012FAA File Offset: 0x000111AA
	public void StartFight()
	{
		Tools.instance.startFight(this.flowchat.GetIntegerVariable("MonsterID"));
	}

	// Token: 0x04000FE6 RID: 4070
	public Flowchart flowchat;
}
