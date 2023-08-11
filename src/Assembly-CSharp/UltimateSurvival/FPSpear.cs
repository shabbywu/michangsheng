using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

public class FPSpear : FPWeaponBase
{
	[Header("Setup")]
	[SerializeField]
	private LayerMask m_Mask;

	[SerializeField]
	private float m_MaxDistance = 50f;

	[Header("Settings")]
	[SerializeField]
	private float m_MinTimeBetweenThrows = 1.5f;

	[Header("Object To Throw")]
	[SerializeField]
	private ShaftedProjectile m_SpearPrefab;

	[SerializeField]
	private Vector3 m_SpawnOffset;

	[SerializeField]
	private float m_SpawnDelay = 0.3f;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	[SerializeField]
	private SoundPlayer m_ThrowAudio;

	private float m_NextTimeCanThrow;

	public bool CanThrow => Time.time > m_NextTimeCanThrow;

	public override bool TryAttackOnce(Camera camera)
	{
		if (!base.Player.Aim.Active || !CanThrow)
		{
			return false;
		}
		m_ThrowAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		((MonoBehaviour)this).StartCoroutine(C_ThrowWithDelay(camera, m_SpawnDelay));
		m_NextTimeCanThrow = Time.time + m_MinTimeBetweenThrows;
		base.Attack.Send();
		return true;
	}

	private void Start()
	{
		base.Player.Aim.AddStartTryer(OnTryStart_Aim);
	}

	private bool OnTryStart_Aim()
	{
		if (base.IsEnabled)
		{
			return CanThrow;
		}
		return true;
	}

	private IEnumerator C_ThrowWithDelay(Camera camera, float delay)
	{
		if (!Object.op_Implicit((Object)(object)m_SpearPrefab))
		{
			Debug.LogErrorFormat((Object)(object)this, "The spear prefab is not assigned in the inspector!.", new object[1] { ((Object)this).name });
			yield break;
		}
		yield return (object)new WaitForSeconds(delay);
		RaycastHit val = default(RaycastHit);
		Vector3 val2 = ((!Physics.Raycast(camera.ViewportPointToRay(Vector3.one * 0.5f), ref val, m_MaxDistance, LayerMask.op_Implicit(m_Mask), (QueryTriggerInteraction)1)) ? (((Component)camera).transform.position + ((Component)camera).transform.forward * m_MaxDistance) : ((RaycastHit)(ref val)).point);
		Vector3 val3 = ((Component)this).transform.position + ((Component)camera).transform.TransformVector(m_SpawnOffset);
		Quaternion val4 = Quaternion.LookRotation(val2 - val3);
		Object.Instantiate<GameObject>(((Component)m_SpearPrefab).gameObject, val3, val4).GetComponent<ShaftedProjectile>().Launch(base.Player);
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
