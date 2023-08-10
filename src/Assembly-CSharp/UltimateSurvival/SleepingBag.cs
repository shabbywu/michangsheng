using UnityEngine;

namespace UltimateSurvival;

public class SleepingBag : InteractableObject
{
	[SerializeField]
	[Tooltip("The player spawn position offset, relative to this object.")]
	private Vector3 m_SpawnPosOffset = new Vector3(0f, 0.3f, 0f);

	[SerializeField]
	[Tooltip("Player sleep position, relative to this object.")]
	private Vector3 m_SleepPosOffset;

	[SerializeField]
	[Tooltip("Player sleep rotation, relative to this object.")]
	private Vector3 m_SleepRotOffset;

	public Vector3 SpawnPosOffset => ((Component)this).transform.position + ((Component)this).transform.TransformVector(m_SpawnPosOffset);

	public Vector3 SleepPosition => ((Component)this).transform.position + ((Component)this).transform.TransformVector(m_SleepPosOffset);

	public Quaternion SleepRotation => ((Component)this).transform.rotation * Quaternion.Euler(m_SleepRotOffset);

	public override void OnInteract(PlayerEventHandler player)
	{
		if (!player.Sleep.Active && MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && player.StartSleeping.Try(this))
		{
			player.Sleep.ForceStart();
		}
	}
}
