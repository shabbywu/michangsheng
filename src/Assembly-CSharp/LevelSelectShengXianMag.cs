using System;

// Token: 0x020003E0 RID: 992
public class LevelSelectShengXianMag : LevelSelectManager
{
	// Token: 0x06002017 RID: 8215 RVA: 0x000E203C File Offset: 0x000E023C
	public override void openLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(true);
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x000E2057 File Offset: 0x000E0257
	public override int StartIndex()
	{
		return 100;
	}
}
