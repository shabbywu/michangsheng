using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E8F RID: 3727
	[ExecuteInEditMode]
	public class View : MonoBehaviour
	{
		// Token: 0x06006997 RID: 27031 RVA: 0x000F5A15 File Offset: 0x000F3C15
		protected virtual void Update()
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06006998 RID: 27032 RVA: 0x0029140F File Offset: 0x0028F60F
		// (set) Token: 0x06006999 RID: 27033 RVA: 0x00291417 File Offset: 0x0028F617
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

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x00291420 File Offset: 0x0028F620
		// (set) Token: 0x0600699B RID: 27035 RVA: 0x00291428 File Offset: 0x0028F628
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

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x00291431 File Offset: 0x0028F631
		// (set) Token: 0x0600699D RID: 27037 RVA: 0x00291439 File Offset: 0x0028F639
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

		// Token: 0x04005988 RID: 22920
		[Tooltip("Orthographic size of the camera view in world units.")]
		[SerializeField]
		protected float viewSize = 0.5f;

		// Token: 0x04005989 RID: 22921
		[Tooltip("Aspect ratio of the primary view rectangle. (e.g. 4:3 aspect ratio = 1.333)")]
		[SerializeField]
		protected Vector2 primaryAspectRatio = new Vector2(4f, 3f);

		// Token: 0x0400598A RID: 22922
		[Tooltip("Aspect ratio of the secondary view rectangle. (e.g. 2:1 aspect ratio = 2.0)")]
		[SerializeField]
		protected Vector2 secondaryAspectRatio = new Vector2(2f, 1f);
	}
}
