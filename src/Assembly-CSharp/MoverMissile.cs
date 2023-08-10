using UnityEngine;

public class MoverMissile : MonoBehaviour
{
	public GameObject target;

	public string TargetTag;

	public float damping = 3f;

	public float Speed = 500f;

	public float SpeedMax = 1000f;

	public float SpeedMult = 1f;

	public Vector3 Noise = new Vector3(20f, 20f, 20f);

	public int distanceLock = 70;

	public int DurationLock = 40;

	public float targetlockdirection = 0.5f;

	public bool Seeker;

	public float LifeTime = 5f;

	private int timetorock;

	private bool locked;

	private void Start()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject, LifeTime);
	}

	private void Update()
	{
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (Seeker)
		{
			if (timetorock > DurationLock)
			{
				if (!locked && !Object.op_Implicit((Object)(object)target))
				{
					float num = 2.1474836E+09f;
					if (GameObject.FindGameObjectsWithTag(TargetTag).Length != 0)
					{
						GameObject[] array = GameObject.FindGameObjectsWithTag(TargetTag);
						foreach (GameObject val in array)
						{
							if (!Object.op_Implicit((Object)(object)val))
							{
								continue;
							}
							float num2 = Vector3.Distance(val.transform.position, ((Component)this).gameObject.transform.position);
							if ((float)distanceLock > num2)
							{
								if (num > num2)
								{
									num = num2;
									target = val;
								}
								locked = true;
							}
						}
					}
				}
			}
			else
			{
				timetorock++;
			}
			if (Object.op_Implicit((Object)(object)target))
			{
				damping += 0.9f;
				Quaternion val2 = Quaternion.LookRotation(target.transform.position - ((Component)((Component)this).transform).transform.position);
				((Component)this).transform.rotation = Quaternion.Slerp(((Component)this).transform.rotation, val2, Time.deltaTime * damping);
				Vector3 val3 = target.transform.position - ((Component)this).transform.position;
				if (Vector3.Dot(((Vector3)(ref val3)).normalized, ((Component)this).transform.forward) < targetlockdirection)
				{
					target = null;
				}
			}
			else
			{
				locked = false;
			}
		}
		Speed += SpeedMult;
		if (Speed > SpeedMax)
		{
			Speed = SpeedMax;
		}
		((Component)this).GetComponent<Rigidbody>().velocity = Speed * Time.deltaTime * ((Component)this).gameObject.transform.forward;
		Rigidbody component = ((Component)this).GetComponent<Rigidbody>();
		component.velocity += new Vector3(Random.Range(0f - Noise.x, Noise.x), Random.Range(0f - Noise.y, Noise.y), Random.Range(0f - Noise.z, Noise.z));
	}
}
