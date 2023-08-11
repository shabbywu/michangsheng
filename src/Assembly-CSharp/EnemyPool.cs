using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	public static int AvailableEnemies;

	private void Start()
	{
		AvailableEnemies = ((Component)this).transform.childCount;
	}

	public GameObject getEnemy()
	{
		if (((Component)this).transform.childCount > 0)
		{
			return ((Component)((Component)this).transform.GetChild(Random.Range(0, ((Component)this).transform.childCount))).gameObject;
		}
		return null;
	}
}
