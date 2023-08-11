using UnityEngine;

public class ShockWaveSkill : MonoBehaviour
{
	public GameObject Skill;

	public float Speed;

	public float Spawnrate;

	public float LifeTime = 1f;

	private float timeTemp;

	private void Start()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject, LifeTime);
		ShakeCamera.Shake(0.2f, 0.7f);
	}

	private void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = ((Component)this).transform;
		transform.position += ((Component)this).transform.forward * Speed * Time.deltaTime;
		if (Time.time > timeTemp + Spawnrate)
		{
			if (Object.op_Implicit((Object)(object)Skill))
			{
				Object.Instantiate<GameObject>(Skill, ((Component)this).transform.position, Quaternion.identity);
			}
			timeTemp = Time.time;
		}
	}
}
