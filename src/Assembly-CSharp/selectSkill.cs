using System;
using GUIPackage;

// Token: 0x0200029E RID: 670
public class selectSkill : prepareSelect
{
	// Token: 0x0600147A RID: 5242 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x00012EFA File Offset: 0x000110FA
	public void setMaxPage(int max)
	{
		this.maxPage = max;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000B9CF4 File Offset: 0x000B7EF4
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

	// Token: 0x0600147D RID: 5245 RVA: 0x000B9D48 File Offset: 0x000B7F48
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

	// Token: 0x0600147E RID: 5246 RVA: 0x00012F26 File Offset: 0x00011126
	public override void resetObj()
	{
		base.setPageTetx();
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00012F2E File Offset: 0x0001112E
	public override void SetFirstPage()
	{
		this.obj.nowIndex = 0;
		base.SetFirstPage();
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000FDE RID: 4062
	public Skill_UI obj;
}
