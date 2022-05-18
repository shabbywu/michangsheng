using System;

// Token: 0x0200057B RID: 1403
public class LevelSelectShengXianMag : LevelSelectManager
{
	// Token: 0x06002397 RID: 9111 RVA: 0x0001CBBC File Offset: 0x0001ADBC
	public override void openLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(true);
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x0001CBD7 File Offset: 0x0001ADD7
	public override int StartIndex()
	{
		return 100;
	}
}
