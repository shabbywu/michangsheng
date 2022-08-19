using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200043E RID: 1086
public class XiuLianGongFaCell : MonoBehaviour
{
	// Token: 0x06002287 RID: 8839 RVA: 0x000ED55F File Offset: 0x000EB75F
	private void Start()
	{
		this.keyCell = base.GetComponent<KeyCell>();
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x00004095 File Offset: 0x00002295
	public void init1()
	{
	}

	// Token: 0x06002289 RID: 8841 RVA: 0x00004095 File Offset: 0x00002295
	public void useGongFa()
	{
	}

	// Token: 0x0600228A RID: 8842 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BE8 RID: 7144
	public UILabel Name;

	// Token: 0x04001BE9 RID: 7145
	public UILabel desc;

	// Token: 0x04001BEA RID: 7146
	public UILabel Time;

	// Token: 0x04001BEB RID: 7147
	public InitLinWu linWu;

	// Token: 0x04001BEC RID: 7148
	private KeyCell keyCell;
}
