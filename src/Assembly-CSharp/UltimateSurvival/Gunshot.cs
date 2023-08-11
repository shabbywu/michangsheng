using UnityEngine;

namespace UltimateSurvival;

public class Gunshot
{
	public Vector3 Position { get; private set; }

	public EntityEventHandler EntityThatShot { get; private set; }

	public Gunshot(Vector3 position, EntityEventHandler entityThatShot = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Position = position;
		EntityThatShot = entityThatShot;
	}
}
