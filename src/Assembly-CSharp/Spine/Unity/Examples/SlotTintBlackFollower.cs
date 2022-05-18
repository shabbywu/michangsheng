using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E54 RID: 3668
	public class SlotTintBlackFollower : MonoBehaviour
	{
		// Token: 0x06005800 RID: 22528 RVA: 0x0003EF21 File Offset: 0x0003D121
		private void Start()
		{
			this.Initialize(false);
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x002466C4 File Offset: 0x002448C4
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

		// Token: 0x06005802 RID: 22530 RVA: 0x00246734 File Offset: 0x00244934
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

		// Token: 0x06005803 RID: 22531 RVA: 0x0003EF2A File Offset: 0x0003D12A
		private void OnDisable()
		{
			this.mb.Clear();
			this.mr.SetPropertyBlock(this.mb);
		}

		// Token: 0x040057FF RID: 22527
		[SpineSlot("", "", false, true, false)]
		[SerializeField]
		protected string slotName;

		// Token: 0x04005800 RID: 22528
		[SerializeField]
		protected string colorPropertyName = "_Color";

		// Token: 0x04005801 RID: 22529
		[SerializeField]
		protected string blackPropertyName = "_Black";

		// Token: 0x04005802 RID: 22530
		public Slot slot;

		// Token: 0x04005803 RID: 22531
		private MeshRenderer mr;

		// Token: 0x04005804 RID: 22532
		private MaterialPropertyBlock mb;

		// Token: 0x04005805 RID: 22533
		private int colorPropertyId;

		// Token: 0x04005806 RID: 22534
		private int blackPropertyId;
	}
}
