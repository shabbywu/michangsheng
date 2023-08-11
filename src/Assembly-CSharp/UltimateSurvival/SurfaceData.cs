using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class SurfaceData
{
	private const int BULLET_DECAL_POOL_SIZE = 100;

	private const int BULLET_IMPACT_FX_POOL_SIZE = 100;

	private const int CHOP_FX_POOL_SIZE = 25;

	private const int HIT_FX_POOL_SIZE = 25;

	[SerializeField]
	private string m_Name;

	[SerializeField]
	private Texture[] m_Textures;

	[Header("Footsteps")]
	[SerializeField]
	private SoundPlayer m_FootstepSounds;

	[SerializeField]
	private SoundPlayer m_JumpSounds;

	[SerializeField]
	private SoundPlayer m_LandSounds;

	[Header("Bullet Impact")]
	[SerializeField]
	private SoundPlayer m_BulletImpactSounds;

	[SerializeField]
	private PooledObject[] m_BulletDecals;

	[SerializeField]
	private PooledObject[] m_BulletImpactFX;

	[Header("Chop & Hit")]
	[SerializeField]
	private SoundPlayer m_ChopSounds;

	[SerializeField]
	private SoundPlayer m_HitSounds;

	[SerializeField]
	private PooledObject[] m_ChopFX;

	[SerializeField]
	private PooledObject[] m_HitFX;

	[Header("Penetration")]
	[SerializeField]
	private bool m_IsPenetrable;

	[SerializeField]
	private SoundPlayer m_SpearPenetrationSounds;

	[SerializeField]
	private SoundPlayer m_ArrowPenetrationSounds;

	private int m_LastPlayedFootstep;

	public string Name => m_Name;

	public bool IsPenetrable => m_IsPenetrable;

	public bool HasTexture(Texture texture)
	{
		for (int i = 0; i < m_Textures.Length; i++)
		{
			if ((Object)(object)m_Textures[i] == (Object)(object)texture)
			{
				return true;
			}
		}
		return false;
	}

	public void PlaySound(ItemSelectionMethod selectionMethod, SoundType soundType, float volumeFactor = 1f, AudioSource audioSource = null)
	{
		switch (soundType)
		{
		case SoundType.BulletImpact:
			m_BulletImpactSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.Footstep:
			m_FootstepSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.Jump:
			m_JumpSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.Land:
			m_LandSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.Chop:
			m_ChopSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.Hit:
			m_HitSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.SpearPenetration:
			m_SpearPenetrationSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		case SoundType.ArrowPenetration:
			m_ArrowPenetrationSounds.Play(selectionMethod, audioSource, volumeFactor);
			break;
		}
	}

	public void PlaySound(ItemSelectionMethod selectionMethod, SoundType soundType, float volumeFactor = 1f, Vector3 position = default(Vector3))
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		switch (soundType)
		{
		case SoundType.BulletImpact:
			m_BulletImpactSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.Footstep:
			m_FootstepSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.Jump:
			m_JumpSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.Land:
			m_LandSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.Chop:
			m_ChopSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.Hit:
			m_HitSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.SpearPenetration:
			m_SpearPenetrationSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		case SoundType.ArrowPenetration:
			m_ArrowPenetrationSounds.PlayAtPosition(selectionMethod, position, volumeFactor);
			break;
		}
	}

	public void CreateBulletDecal(Vector3 position, Quaternion rotation, Transform parent = null)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (m_BulletDecals.Length != 0)
		{
			PooledObject pooledObject = m_BulletDecals[Random.Range(0, m_BulletDecals.Length)];
			if (Object.op_Implicit((Object)(object)pooledObject))
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A decal object was found null, please check the surface database for missing decals.", new object[1] { Name });
		}
	}

	public void CreateBulletImpactFX(Vector3 position, Quaternion rotation, Transform parent = null)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (m_BulletImpactFX.Length != 0)
		{
			PooledObject pooledObject = m_BulletImpactFX[Random.Range(0, m_BulletImpactFX.Length)];
			if (Object.op_Implicit((Object)(object)pooledObject))
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A bullet impact FX prefab was found null, please check the surface database for missing effects.", new object[1] { Name });
		}
	}

	public void CreateHitFX(Vector3 position, Quaternion rotation, Transform parent = null)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (m_HitFX.Length != 0)
		{
			PooledObject pooledObject = m_HitFX[Random.Range(0, m_HitFX.Length)];
			if (Object.op_Implicit((Object)(object)pooledObject))
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A hit FX prefab was found null, please check the surface database for missing effects.", new object[1] { Name });
		}
	}

	public void CreateChopFX(Vector3 position, Quaternion rotation, Transform parent = null)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (m_ChopFX.Length != 0)
		{
			PooledObject pooledObject = m_ChopFX[Random.Range(0, m_ChopFX.Length)];
			if (Object.op_Implicit((Object)(object)pooledObject))
			{
				Object.Instantiate<PooledObject>(pooledObject, position, rotation, parent);
				return;
			}
			Debug.LogWarningFormat("[({0}) SurfaceData] - A chop FX prefab was found null, please check the surface database for missing effects.", new object[1] { Name });
		}
	}
}
