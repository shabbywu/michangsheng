using UnityEngine;

public class OrbitLookAt : Orbit
{
	public Vector3 LookAt = Vector3.zero;

	private void Start()
	{
		Data.Zenith = -0.3f;
		Data.Length = -6f;
	}

	protected override void Update()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		Transform transform = ((Component)this).gameObject.transform;
		transform.position += LookAt;
		((Component)this).gameObject.transform.LookAt(LookAt);
	}
}
