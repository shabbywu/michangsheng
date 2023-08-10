using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(FPObject))]
public class FPAnimator : PlayerBehaviour
{
	public enum ObjectType
	{
		Normal,
		Melee,
		Throwable,
		Hitscan,
		Bow
	}

	[SerializeField]
	private Animator m_Animator;

	[Header("General")]
	[SerializeField]
	private float m_DrawSpeed = 1f;

	[SerializeField]
	private float m_HolsterSpeed = 1f;

	private FPObject m_Object;

	private bool m_Initialized;

	public Animator Animator => m_Animator;

	public FPObject FPObject => m_Object;

	protected virtual void Awake()
	{
		m_Object = ((Component)this).GetComponent<FPObject>();
		m_Object.Draw.AddListener(On_Draw);
		m_Object.Holster.AddListener(On_Holster);
		base.Player.Sleep.AddStopListener(OnStop_Sleep);
		base.Player.Respawn.AddListener(On_Respawn);
		m_Animator.SetFloat("Draw Speed", m_DrawSpeed);
		m_Animator.SetFloat("Holster Speed", m_HolsterSpeed);
	}

	protected virtual void OnValidate()
	{
		if (Object.op_Implicit((Object)(object)FPObject) && FPObject.IsEnabled && Object.op_Implicit((Object)(object)Animator))
		{
			m_Animator.SetFloat("Draw Speed", m_DrawSpeed);
			m_Animator.SetFloat("Holster Speed", m_HolsterSpeed);
		}
	}

	private void On_Draw()
	{
		OnValidate();
		if (Object.op_Implicit((Object)(object)m_Animator))
		{
			m_Animator.SetTrigger("Draw");
		}
	}

	private void On_Holster()
	{
		if (Object.op_Implicit((Object)(object)m_Animator))
		{
			m_Animator.SetTrigger("Holster");
		}
	}

	private void OnStop_Sleep()
	{
		if (FPObject.IsEnabled)
		{
			OnValidate();
		}
	}

	private void On_Respawn()
	{
		if (FPObject.IsEnabled)
		{
			OnValidate();
		}
	}
}
