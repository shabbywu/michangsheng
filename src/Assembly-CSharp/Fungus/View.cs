using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001300 RID: 4864
	[ExecuteInEditMode]
	public class View : MonoBehaviour
	{
		// Token: 0x06007696 RID: 30358 RVA: 0x0001F611 File Offset: 0x0001D811
		protected virtual void Update()
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06007697 RID: 30359 RVA: 0x00050B99 File Offset: 0x0004ED99
		// (set) Token: 0x06007698 RID: 30360 RVA: 0x00050BA1 File Offset: 0x0004EDA1
		public virtual float ViewSize
		{
			get
			{
				return this.viewSize;
			}
			set
			{
				this.viewSize = value;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06007699 RID: 30361 RVA: 0x00050BAA File Offset: 0x0004EDAA
		// (set) Token: 0x0600769A RID: 30362 RVA: 0x00050BB2 File Offset: 0x0004EDB2
		public virtual Vector2 PrimaryAspectRatio
		{
			get
			{
				return this.primaryAspectRatio;
			}
			set
			{
				this.primaryAspectRatio = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600769B RID: 30363 RVA: 0x00050BBB File Offset: 0x0004EDBB
		// (set) Token: 0x0600769C RID: 30364 RVA: 0x00050BC3 File Offset: 0x0004EDC3
		public virtual Vector2 SecondaryAspectRatio
		{
			get
			{
				return this.secondaryAspectRatio;
			}
			set
			{
				this.secondaryAspectRatio = value;
			}
		}

		// Token: 0x04006769 RID: 26473
		[Tooltip("Orthographic size of the camera view in world units.")]
		[SerializeField]
		protected float viewSize = 0.5f;

		// Token: 0x0400676A RID: 26474
		[Tooltip("Aspect ratio of the primary view rectangle. (e.g. 4:3 aspect ratio = 1.333)")]
		[SerializeField]
		protected Vector2 primaryAspectRatio = new Vector2(4f, 3f);

		// Token: 0x0400676B RID: 26475
		[Tooltip("Aspect ratio of the secondary view rectangle. (e.g. 2:1 aspect ratio = 2.0)")]
		[SerializeField]
		protected Vector2 secondaryAspectRatio = new Vector2(2f, 1f);
	}
}
