using System;
using GUIPackage;

// Token: 0x020002A2 RID: 674
public class selectStaticSkill : prepareSelect
{
	// Token: 0x0600148C RID: 5260 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x000B9F6C File Offset: 0x000B816C
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

	// Token: 0x0600148E RID: 5262 RVA: 0x000B9FC0 File Offset: 0x000B81C0
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

	// Token: 0x0600148F RID: 5263 RVA: 0x00012F26 File Offset: 0x00011126
	public override void resetObj()
	{
		base.setPageTetx();
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x00012F77 File Offset: 0x00011177
	public override void SetFirstPage()
	{
		this.obj.nowIndex = 0;
		base.SetFirstPage();
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000FE5 RID: 4069
	public Skill_UIST obj;
}
