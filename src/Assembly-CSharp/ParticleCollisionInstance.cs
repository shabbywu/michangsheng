using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionInstance : MonoBehaviour
{
	public GameObject[] EffectsOnCollision;

	public float DestroyTimeDelay = 5f;

	public bool UseWorldSpacePosition;

	public float Offset;

	public Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

	public bool useOnlyRotationOffset = true;

	public bool UseFirePointRotation;

	public bool DestoyMainEffect = true;

	private ParticleSystem part;

	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	private ParticleSystem ps;

	private void Start()
	{
		part = ((Component)this).GetComponent<ParticleSystem>();
	}

	private void OnParticleCollision(GameObject other)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		int num = ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents);
		for (int i = 0; i < num; i++)
		{
			GameObject[] effectsOnCollision = EffectsOnCollision;
			foreach (GameObject obj in effectsOnCollision)
			{
				ParticleCollisionEvent val = collisionEvents[i];
				Vector3 intersection = ((ParticleCollisionEvent)(ref val)).intersection;
				val = collisionEvents[i];
				GameObject val2 = Object.Instantiate<GameObject>(obj, intersection + ((ParticleCollisionEvent)(ref val)).normal * Offset, default(Quaternion));
				if (!UseWorldSpacePosition)
				{
					val2.transform.parent = ((Component)this).transform;
				}
				if (UseFirePointRotation)
				{
					val2.transform.LookAt(((Component)this).transform.position);
				}
				else if (rotationOffset != Vector3.zero && useOnlyRotationOffset)
				{
					val2.transform.rotation = Quaternion.Euler(rotationOffset);
				}
				else
				{
					Transform transform = val2.transform;
					val = collisionEvents[i];
					Vector3 intersection2 = ((ParticleCollisionEvent)(ref val)).intersection;
					val = collisionEvents[i];
					transform.LookAt(intersection2 + ((ParticleCollisionEvent)(ref val)).normal);
					Transform transform2 = val2.transform;
					transform2.rotation *= Quaternion.Euler(rotationOffset);
				}
				Object.Destroy((Object)(object)val2, DestroyTimeDelay);
			}
		}
		if (DestoyMainEffect)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject, DestroyTimeDelay + 0.5f);
		}
	}
}
