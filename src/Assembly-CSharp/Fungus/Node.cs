using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012E5 RID: 4837
	[AddComponentMenu("")]
	public class Node : MonoBehaviour
	{
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060075CF RID: 30159 RVA: 0x00050427 File Offset: 0x0004E627
		// (set) Token: 0x060075D0 RID: 30160 RVA: 0x0005042F File Offset: 0x0004E62F
		public virtual Rect _NodeRect
		{
			get
			{
				return this.nodeRect;
			}
			set
			{
				this.nodeRect = value;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060075D1 RID: 30161 RVA: 0x00050438 File Offset: 0x0004E638
		// (set) Token: 0x060075D2 RID: 30162 RVA: 0x00050440 File Offset: 0x0004E640
		public virtual Color Tint
		{
			get
			{
				return this.tint;
			}
			set
			{
				this.tint = value;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060075D3 RID: 30163 RVA: 0x00050449 File Offset: 0x0004E649
		// (set) Token: 0x060075D4 RID: 30164 RVA: 0x00050451 File Offset: 0x0004E651
		public virtual bool UseCustomTint
		{
			get
			{
				return this.useCustomTint;
			}
			set
			{
				this.useCustomTint = value;
			}
		}

		// Token: 0x040066CD RID: 26317
		[SerializeField]
		protected Rect nodeRect = new Rect(0f, 0f, 120f, 30f);

		// Token: 0x040066CE RID: 26318
		[SerializeField]
		protected Color tint = Color.white;

		// Token: 0x040066CF RID: 26319
		[SerializeField]
		protected bool useCustomTint;
	}
}
