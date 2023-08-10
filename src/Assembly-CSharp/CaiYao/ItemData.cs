namespace CaiYao;

public class ItemData
{
	public int ItemId;

	public int ItemNum;

	public int AddNum;

	public int AddTime;

	public bool HasEnemy;

	public int FirstEnemyId;

	public int ScondEnemyId;

	public ItemData(int itemId, int itemNum, int addNum, int addTime, bool hasEnemy, int firstEnemyId, int scondEnemyId)
	{
		ItemId = itemId;
		ItemNum = itemNum;
		AddNum = addNum;
		AddTime = addTime;
		HasEnemy = hasEnemy;
		FirstEnemyId = firstEnemyId;
		ScondEnemyId = scondEnemyId;
	}
}
