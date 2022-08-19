using System;
using Fungus;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class talkCompont : MonoBehaviour
{
	// Token: 0x060011F1 RID: 4593 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0006C0D9 File Offset: 0x0006A2D9
	public void StartFight()
	{
		Tools.instance.startFight(this.flowchat.GetIntegerVariable("MonsterID"));
	}

	// Token: 0x04000CBE RID: 3262
	public Flowchart flowchat;
}
