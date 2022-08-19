using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E7A RID: 3706
	[AddComponentMenu("")]
	public class Node : MonoBehaviour
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060068E8 RID: 26856 RVA: 0x0028E9D8 File Offset: 0x0028CBD8
		// (set) Token: 0x060068E9 RID: 26857 RVA: 0x0028E9E0 File Offset: 0x0028CBE0
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

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060068EA RID: 26858 RVA: 0x0028E9E9 File Offset: 0x0028CBE9
		// (set) Token: 0x060068EB RID: 26859 RVA: 0x0028E9F1 File Offset: 0x0028CBF1
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

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060068EC RID: 26860 RVA: 0x0028E9FA File Offset: 0x0028CBFA
		// (set) Token: 0x060068ED RID: 26861 RVA: 0x0028EA02 File Offset: 0x0028CC02
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

		// Token: 0x04005905 RID: 22789
		[SerializeField]
		protected Rect nodeRect = new Rect(0f, 0f, 120f, 30f);

		// Token: 0x04005906 RID: 22790
		[SerializeField]
		protected Color tint = Color.white;

		// Token: 0x04005907 RID: 22791
		[SerializeField]
		protected bool useCustomTint;
	}
}
