using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] Objectman;

	public float timeSpawn = 3f;

	public int enemyCount = 10;

	public int radius;

	private float timetemp;

	private void Start()
	{
		timetemp = Time.time;
	}

	private void Update()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		if (Random.Range(0, 10) > 8)
		{
			num = 1;
		}
		if (GameObject.FindGameObjectsWithTag(Objectman[num].tag).Length < enemyCount && Time.time > timetemp + timeSpawn)
		{
			timetemp = Time.time;
			Object.Instantiate<GameObject>(Objectman[num], ((Component)this).transform.position + new Vector3((float)Random.Range(-radius, radius), ((Component)this).transform.position.y, (float)Random.Range(-radius, radius)), Quaternion.identity);
		}
	}
}
