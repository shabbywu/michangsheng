public class LevelSelectShengXianMag : LevelSelectManager
{
	public override void openLevelSelect()
	{
		levelSelectMenu.gameObject.SetActive(true);
	}

	public override int StartIndex()
	{
		return 100;
	}
}
