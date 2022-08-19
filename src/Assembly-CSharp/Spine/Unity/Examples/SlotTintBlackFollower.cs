using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFB RID: 2811
	public class SlotTintBlackFollower : MonoBehaviour
	{
		// Token: 0x06004E5F RID: 20063 RVA: 0x002167B9 File Offset: 0x002149B9
		private void Start()
		{
			this.Initialize(false);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x002167C4 File Offset: 0x002149C4
		public void Initialize(bool overwrite)
		{
			if (overwrite || this.mb == null)
			{
				this.mb = new MaterialPropertyBlock();
				this.mr = base.GetComponent<MeshRenderer>();
				this.slot = base.GetComponent<ISkeletonComponent>().Skeleton.FindSlot(this.slotName);
				this.colorPropertyId = Shader.PropertyToID(this.colorPropertyName);
				this.blackPropertyId = Shader.PropertyToID(this.blackPropertyName);
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00216834 File Offset: 0x00214A34
		public void Update()
		{
			Slot slot = this.slot;
			if (slot == null)
			{
				return;
			}
			this.mb.SetColor(this.colorPropertyId, SkeletonExtensions.GetColor(slot));
			this.mb.SetColor(this.blackPropertyId, SkeletonExtensions.GetColorTintBlack(slot));
			this.mr.SetPropertyBlock(this.mb);
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0021688B File Offset: 0x00214A8B
		private void OnDisable()
		{
			this.mb.Clear();
			this.mr.SetPropertyBlock(this.mb);
		}

		// Token: 0x04004DD3 RID: 19923
		[SpineSlot("", "", false, true, false)]
		[SerializeField]
		protected string slotName;

		// Token: 0x04004DD4 RID: 19924
		[SerializeField]
		protected string colorPropertyName = "_Color";

		// Token: 0x04004DD5 RID: 19925
		[SerializeField]
		protected string blackPropertyName = "_Black";

		// Token: 0x04004DD6 RID: 19926
		public Slot slot;

		// Token: 0x04004DD7 RID: 19927
		private MeshRenderer mr;

		// Token: 0x04004DD8 RID: 19928
		private MaterialPropertyBlock mb;

		// Token: 0x04004DD9 RID: 19929
		private int colorPropertyId;

		// Token: 0x04004DDA RID: 19930
		private int blackPropertyId;
	}
}
