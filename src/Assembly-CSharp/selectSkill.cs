using System;
using GUIPackage;

// Token: 0x020001A2 RID: 418
public class selectSkill : prepareSelect
{
	// Token: 0x060011D3 RID: 4563 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0006BB0E File Offset: 0x00069D0E
	public void setMaxPage(int max)
	{
		this.maxPage = max;
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x0006BC30 File Offset: 0x00069E30
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

	// Token: 0x060011D6 RID: 4566 RVA: 0x0006BC84 File Offset: 0x00069E84
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

	// Token: 0x060011D7 RID: 4567 RVA: 0x0006BCDC File Offset: 0x00069EDC
	public override void resetObj()
	{
		base.setPageTetx();
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0006BCE4 File Offset: 0x00069EE4
	public override void SetFirstPage()
	{
		this.obj.nowIndex = 0;
		base.SetFirstPage();
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000CBA RID: 3258
	public Skill_UI obj;
}
