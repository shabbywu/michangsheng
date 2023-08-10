using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimateSurvival;

public class Ragdoll : MonoBehaviour
{
	[Serializable]
	public class BoneToReparent
	{
		[SerializeField]
		private Transform m_Bone;

		[SerializeField]
		private Transform m_NewParent;

		public void Reparent()
		{
			m_Bone.SetParent(m_NewParent, true);
		}
	}

	[SerializeField]
	private Rigidbody m_Pelvis;

	[SerializeField]
	private string m_NormalLayer = "Hitbox";

	[SerializeField]
	private string m_RagdollLayer = "Default";

	[Header("Helpers")]
	[SerializeField]
	private BoneToReparent[] m_BonesToReparent;

	[SerializeField]
	private Texture m_SurfaceTexture;

	[SerializeField]
	private bool m_AutoAssignHitboxes = true;

	private List<Rigidbody> m_Bones = new List<Rigidbody>();

	public void Enable()
	{
		foreach (Rigidbody bone in m_Bones)
		{
			bone.isKinematic = false;
			((Component)bone).gameObject.layer = LayerMask.NameToLayer(m_RagdollLayer);
		}
		BoneToReparent[] bonesToReparent = m_BonesToReparent;
		for (int i = 0; i < bonesToReparent.Length; i++)
		{
			bonesToReparent[i].Reparent();
		}
	}

	public void Disable()
	{
		foreach (Rigidbody bone in m_Bones)
		{
			bone.isKinematic = true;
			((Component)bone).gameObject.layer = LayerMask.NameToLayer(m_NormalLayer);
		}
	}

	private void Awake()
	{
		m_Bones = (from joint in ((Component)this).GetComponentsInChildren<CharacterJoint>()
			select ((Component)joint).GetComponent<Rigidbody>()).ToList();
		m_Bones.Add(m_Pelvis);
		Disable();
		foreach (Rigidbody bone in m_Bones)
		{
			if (m_AutoAssignHitboxes && (Object)(object)((Component)bone).gameObject.GetComponent<HitBox>() == (Object)null)
			{
				((Component)bone).gameObject.AddComponent<HitBox>();
			}
			if (Object.op_Implicit((Object)(object)m_SurfaceTexture) && (Object)(object)((Component)bone).gameObject.GetComponent<SurfaceIdentity>() == (Object)null)
			{
				((Component)bone).gameObject.AddComponent<SurfaceIdentity>().Texture = m_SurfaceTexture;
			}
		}
	}
}
