using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C1 RID: 1473
	public class Ragdoll : MonoBehaviour
	{
		// Token: 0x06002FB3 RID: 12211 RVA: 0x001586B8 File Offset: 0x001568B8
		public void Enable()
		{
			foreach (Rigidbody rigidbody in this.m_Bones)
			{
				rigidbody.isKinematic = false;
				rigidbody.gameObject.layer = LayerMask.NameToLayer(this.m_RagdollLayer);
			}
			Ragdoll.BoneToReparent[] bonesToReparent = this.m_BonesToReparent;
			for (int i = 0; i < bonesToReparent.Length; i++)
			{
				bonesToReparent[i].Reparent();
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x0015873C File Offset: 0x0015693C
		public void Disable()
		{
			foreach (Rigidbody rigidbody in this.m_Bones)
			{
				rigidbody.isKinematic = true;
				rigidbody.gameObject.layer = LayerMask.NameToLayer(this.m_NormalLayer);
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x001587A4 File Offset: 0x001569A4
		private void Awake()
		{
			this.m_Bones = (from joint in base.GetComponentsInChildren<CharacterJoint>()
			select joint.GetComponent<Rigidbody>()).ToList<Rigidbody>();
			this.m_Bones.Add(this.m_Pelvis);
			this.Disable();
			foreach (Rigidbody rigidbody in this.m_Bones)
			{
				if (this.m_AutoAssignHitboxes && rigidbody.gameObject.GetComponent<HitBox>() == null)
				{
					rigidbody.gameObject.AddComponent<HitBox>();
				}
				if (this.m_SurfaceTexture && rigidbody.gameObject.GetComponent<SurfaceIdentity>() == null)
				{
					rigidbody.gameObject.AddComponent<SurfaceIdentity>().Texture = this.m_SurfaceTexture;
				}
			}
		}

		// Token: 0x040029FF RID: 10751
		[SerializeField]
		private Rigidbody m_Pelvis;

		// Token: 0x04002A00 RID: 10752
		[SerializeField]
		private string m_NormalLayer = "Hitbox";

		// Token: 0x04002A01 RID: 10753
		[SerializeField]
		private string m_RagdollLayer = "Default";

		// Token: 0x04002A02 RID: 10754
		[Header("Helpers")]
		[SerializeField]
		private Ragdoll.BoneToReparent[] m_BonesToReparent;

		// Token: 0x04002A03 RID: 10755
		[SerializeField]
		private Texture m_SurfaceTexture;

		// Token: 0x04002A04 RID: 10756
		[SerializeField]
		private bool m_AutoAssignHitboxes = true;

		// Token: 0x04002A05 RID: 10757
		private List<Rigidbody> m_Bones = new List<Rigidbody>();

		// Token: 0x020014A8 RID: 5288
		[Serializable]
		public class BoneToReparent
		{
			// Token: 0x06008160 RID: 33120 RVA: 0x002D8E0B File Offset: 0x002D700B
			public void Reparent()
			{
				this.m_Bone.SetParent(this.m_NewParent, true);
			}

			// Token: 0x04006CCC RID: 27852
			[SerializeField]
			private Transform m_Bone;

			// Token: 0x04006CCD RID: 27853
			[SerializeField]
			private Transform m_NewParent;
		}
	}
}
