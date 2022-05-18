using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200087F RID: 2175
	public class Ragdoll : MonoBehaviour
	{
		// Token: 0x06003837 RID: 14391 RVA: 0x001A2728 File Offset: 0x001A0928
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

		// Token: 0x06003838 RID: 14392 RVA: 0x001A27AC File Offset: 0x001A09AC
		public void Disable()
		{
			foreach (Rigidbody rigidbody in this.m_Bones)
			{
				rigidbody.isKinematic = true;
				rigidbody.gameObject.layer = LayerMask.NameToLayer(this.m_NormalLayer);
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x001A2814 File Offset: 0x001A0A14
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

		// Token: 0x04003289 RID: 12937
		[SerializeField]
		private Rigidbody m_Pelvis;

		// Token: 0x0400328A RID: 12938
		[SerializeField]
		private string m_NormalLayer = "Hitbox";

		// Token: 0x0400328B RID: 12939
		[SerializeField]
		private string m_RagdollLayer = "Default";

		// Token: 0x0400328C RID: 12940
		[Header("Helpers")]
		[SerializeField]
		private Ragdoll.BoneToReparent[] m_BonesToReparent;

		// Token: 0x0400328D RID: 12941
		[SerializeField]
		private Texture m_SurfaceTexture;

		// Token: 0x0400328E RID: 12942
		[SerializeField]
		private bool m_AutoAssignHitboxes = true;

		// Token: 0x0400328F RID: 12943
		private List<Rigidbody> m_Bones = new List<Rigidbody>();

		// Token: 0x02000880 RID: 2176
		[Serializable]
		public class BoneToReparent
		{
			// Token: 0x0600383B RID: 14395 RVA: 0x00028E48 File Offset: 0x00027048
			public void Reparent()
			{
				this.m_Bone.SetParent(this.m_NewParent, true);
			}

			// Token: 0x04003290 RID: 12944
			[SerializeField]
			private Transform m_Bone;

			// Token: 0x04003291 RID: 12945
			[SerializeField]
			private Transform m_NewParent;
		}
	}
}
