using UnityEngine;

public class OrbitGameObject : Orbit
{
	public GameObject Target;

	public Vector3 ArmOffset = Vector3.zero;

	private void Start()
	{
		Data.Zenith = -0.3f;
		Data.Length = -6f;
	}

	protected override void Update()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Time.timeScale += (1f - Time.timeScale) / 10f;
		Vector3 val = ArmOffset;
		if ((Object)(object)Target != (Object)null)
		{
			val += Target.transform.position;
		}
		base.Update();
		Transform transform = ((Component)this).gameObject.transform;
		transform.position += val;
		((Component)this).gameObject.transform.LookAt(val);
	}
}
