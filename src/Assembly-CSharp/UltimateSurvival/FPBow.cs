using UnityEngine;

namespace UltimateSurvival;

public class FPBow : FPWeaponBase
{
	[Header("Bow Setup")]
	[SerializeField]
	private LayerMask m_Mask;

	[SerializeField]
	private float m_MaxDistance = 50f;

	[Header("Bow Settings")]
	[SerializeField]
	private float m_MinTimeBetweenShots = 1f;

	[Header("Arrow")]
	[SerializeField]
	private ShaftedProjectile m_ArrowPrefab;

	[SerializeField]
	private Vector3 m_SpawnOffset;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_ReleaseAudio;

	[SerializeField]
	private SoundPlayer m_StretchAudio;

	private float m_NextTimeCanShoot;

	public override bool TryAttackOnce(Camera camera)
	{
		if (!base.Player.Aim.Active || Time.time < m_NextTimeCanShoot)
		{
			return false;
		}
		m_ReleaseAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		SpawnArrow(camera);
		m_NextTimeCanShoot = Time.time + m_MinTimeBetweenShots;
		base.Player.Aim.ForceStop();
		base.Attack.Send();
		return true;
	}

	protected override void Awake()
	{
		base.Awake();
		base.Player.Aim.AddStartTryer(OnTryStart_Aim);
	}

	private bool OnTryStart_Aim()
	{
		int num;
		if (!(Time.time > m_NextTimeCanShoot))
		{
			num = ((!base.IsEnabled) ? 1 : 0);
			if (num == 0)
			{
				goto IL_003b;
			}
		}
		else
		{
			num = 1;
		}
		if (base.IsEnabled)
		{
			m_StretchAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		}
		goto IL_003b;
		IL_003b:
		return (byte)num != 0;
	}

	private void SpawnArrow(Camera camera)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit((Object)(object)m_ArrowPrefab))
		{
			Debug.LogErrorFormat("[{0}.FPBow] - No arrow prefab assigned in the inspector! Please assign one.", new object[1] { ((Object)this).name });
			return;
		}
		RaycastHit val = default(RaycastHit);
		Vector3 val2 = ((!Physics.Raycast(camera.ViewportPointToRay(Vector3.one * 0.5f), ref val, m_MaxDistance, LayerMask.op_Implicit(m_Mask), (QueryTriggerInteraction)1)) ? (((Component)camera).transform.position + ((Component)camera).transform.forward * m_MaxDistance) : ((RaycastHit)(ref val)).point);
		Vector3 val3 = ((Component)this).transform.position + ((Component)camera).transform.TransformVector(m_SpawnOffset);
		Quaternion val4 = Quaternion.LookRotation(val2 - val3);
		Object.Instantiate<GameObject>(((Component)m_ArrowPrefab).gameObject, val3, val4).GetComponent<ShaftedProjectile>().Launch(base.Player);
		if (m_Durability != null)
		{
			ItemProperty.Float @float = m_Durability.Float;
			@float.Current--;
			m_Durability.SetValue(ItemProperty.Type.Float, @float);
			if (@float.Current == 0f)
			{
				base.Player.DestroyEquippedItem.Try();
			}
		}
	}
}
