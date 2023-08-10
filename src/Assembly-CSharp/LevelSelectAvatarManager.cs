public class LevelSelectAvatarManager : LevelSelectManager
{
	public override void openLevelSelect()
	{
		levelSelectMenu.gameObject.SetActive(true);
	}

	public override void closeLevelSelect()
	{
		levelSelectMenu.gameObject.SetActive(false);
	}

	private void Update()
	{
	}
}
