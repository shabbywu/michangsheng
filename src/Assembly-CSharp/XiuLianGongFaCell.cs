using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020005F5 RID: 1525
public class XiuLianGongFaCell : MonoBehaviour
{
	// Token: 0x06002646 RID: 9798 RVA: 0x0001E882 File Offset: 0x0001CA82
	private void Start()
	{
		this.keyCell = base.GetComponent<KeyCell>();
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x000042DD File Offset: 0x000024DD
	public void init1()
	{
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x000042DD File Offset: 0x000024DD
	public void useGongFa()
	{
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x040020B4 RID: 8372
	public UILabel Name;

	// Token: 0x040020B5 RID: 8373
	public UILabel desc;

	// Token: 0x040020B6 RID: 8374
	public UILabel Time;

	// Token: 0x040020B7 RID: 8375
	public InitLinWu linWu;

	// Token: 0x040020B8 RID: 8376
	private KeyCell keyCell;
}
