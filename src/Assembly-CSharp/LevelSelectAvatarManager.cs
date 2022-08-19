using System;

// Token: 0x020003DF RID: 991
public class LevelSelectAvatarManager : LevelSelectManager
{
	// Token: 0x06002013 RID: 8211 RVA: 0x000E203C File Offset: 0x000E023C
	public override void openLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(true);
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x00048C3B File Offset: 0x00046E3B
	public override void closeLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(false);
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
