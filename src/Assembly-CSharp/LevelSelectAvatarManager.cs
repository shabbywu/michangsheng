using System;

// Token: 0x0200057A RID: 1402
public class LevelSelectAvatarManager : LevelSelectManager
{
	// Token: 0x06002393 RID: 9107 RVA: 0x0001CBBC File Offset: 0x0001ADBC
	public override void openLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(true);
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x0000ED23 File Offset: 0x0000CF23
	public override void closeLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(false);
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
