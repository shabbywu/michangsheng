using System;
using GUIPackage;

// Token: 0x020001A5 RID: 421
public class selectStaticSkill : prepareSelect
{
	// Token: 0x060011E5 RID: 4581 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0006BF60 File Offset: 0x0006A160
	public override void addNowPage()
	{
		this.obj.nowIndex++;
		this.nowIndex = this.obj.nowIndex;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
			this.obj.nowIndex = 0;
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0006BFB4 File Offset: 0x0006A1B4
	public override void reduceIndex()
	{
		this.obj.nowIndex--;
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
			this.obj.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x0006BCDC File Offset: 0x00069EDC
	public override void resetObj()
	{
		base.setPageTetx();
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0006C00C File Offset: 0x0006A20C
	public override void SetFirstPage()
	{
		this.obj.nowIndex = 0;
		base.SetFirstPage();
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000CBD RID: 3261
	public Skill_UIST obj;
}
