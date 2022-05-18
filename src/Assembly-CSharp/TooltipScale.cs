using System;

// Token: 0x020005E2 RID: 1506
public class TooltipScale : TooltipBase
{
	// Token: 0x060025D7 RID: 9687 RVA: 0x0001E46E File Offset: 0x0001C66E
	protected override void Update()
	{
		base.Update();
		this.setBGwight();
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x0012C238 File Offset: 0x0012A438
	public void setBGwight()
	{
		if (this.IsSprite)
		{
			this.uISprite.height = this.uILabel.height + this.BaseHeight;
		}
		if (this.childTexture != null)
		{
			this.childTexture.height = this.uILabel.height + this.BaseHeight;
		}
	}

	// Token: 0x04002068 RID: 8296
	public UILabel uILabel;

	// Token: 0x04002069 RID: 8297
	public bool IsSprite;

	// Token: 0x0400206A RID: 8298
	public UISprite uISprite;

	// Token: 0x0400206B RID: 8299
	public int BaseHeight = 28;
}
