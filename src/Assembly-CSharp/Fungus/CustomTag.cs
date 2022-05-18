using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012C7 RID: 4807
	[ExecuteInEditMode]
	public class CustomTag : MonoBehaviour
	{
		// Token: 0x0600749A RID: 29850 RVA: 0x0004F923 File Offset: 0x0004DB23
		protected virtual void OnEnable()
		{
			if (!CustomTag.activeCustomTags.Contains(this))
			{
				CustomTag.activeCustomTags.Add(this);
			}
		}

		// Token: 0x0600749B RID: 29851 RVA: 0x0004F93D File Offset: 0x0004DB3D
		protected virtual void OnDisable()
		{
			CustomTag.activeCustomTags.Remove(this);
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600749C RID: 29852 RVA: 0x0004F94B File Offset: 0x0004DB4B
		public virtual string TagStartSymbol
		{
			get
			{
				return this.tagStartSymbol;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x0004F953 File Offset: 0x0004DB53
		public virtual string TagEndSymbol
		{
			get
			{
				return this.tagEndSymbol;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600749E RID: 29854 RVA: 0x0004F95B File Offset: 0x0004DB5B
		public virtual string ReplaceTagStartWith
		{
			get
			{
				return this.replaceTagStartWith;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600749F RID: 29855 RVA: 0x0004F963 File Offset: 0x0004DB63
		public virtual string ReplaceTagEndWith
		{
			get
			{
				return this.replaceTagEndWith;
			}
		}

		// Token: 0x0400663B RID: 26171
		[Tooltip("String that defines the start of the tag.")]
		[SerializeField]
		protected string tagStartSymbol;

		// Token: 0x0400663C RID: 26172
		[Tooltip("String that defines the end of the tag.")]
		[SerializeField]
		protected string tagEndSymbol;

		// Token: 0x0400663D RID: 26173
		[Tooltip("String to replace the start tag with.")]
		[SerializeField]
		protected string replaceTagStartWith;

		// Token: 0x0400663E RID: 26174
		[Tooltip("String to replace the end tag with.")]
		[SerializeField]
		protected string replaceTagEndWith;

		// Token: 0x0400663F RID: 26175
		public static List<CustomTag> activeCustomTags = new List<CustomTag>();
	}
}
