using System;

// Token: 0x0200042B RID: 1067
public class TooltipScale : TooltipBase
{
	// Token: 0x06002218 RID: 8728 RVA: 0x000EAF96 File Offset: 0x000E9196
	protected override void Update()
	{
		base.Update();
		this.setBGwight();
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x000EAFA4 File Offset: 0x000E91A4
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

	// Token: 0x04001B9C RID: 7068
	public UILabel uILabel;

	// Token: 0x04001B9D RID: 7069
	public bool IsSprite;

	// Token: 0x04001B9E RID: 7070
	public UISprite uISprite;

	// Token: 0x04001B9F RID: 7071
	public int BaseHeight = 28;
}
