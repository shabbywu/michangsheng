using UnityEngine;

namespace UltimateSurvival;

public abstract class FPWeaponBase : FPObject
{
	private Message m_Attack = new Message();

	[SerializeField]
	[Tooltip("Can this weapon be used while too close to other objects? (e.g. a wall)")]
	private bool m_UseWhileNearObjects = true;

	public Message Attack => m_Attack;

	public bool UseWhileNearObjects => m_UseWhileNearObjects;

	public bool CanBeUsed { get; set; }

	public override void On_Draw(SavableItem correspondingItem)
	{
		base.On_Draw(correspondingItem);
	}

	public virtual bool TryAttackOnce(Camera camera)
	{
		return false;
	}

	public virtual bool TryAttackContinuously(Camera camera)
	{
		return false;
	}
}
