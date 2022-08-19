using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E6A RID: 3690
	[ExecuteInEditMode]
	public class CustomTag : MonoBehaviour
	{
		// Token: 0x060067E8 RID: 26600 RVA: 0x0028B5AA File Offset: 0x002897AA
		protected virtual void OnEnable()
		{
			if (!CustomTag.activeCustomTags.Contains(this))
			{
				CustomTag.activeCustomTags.Add(this);
			}
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x0028B5C4 File Offset: 0x002897C4
		protected virtual void OnDisable()
		{
			CustomTag.activeCustomTags.Remove(this);
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x0028B5D2 File Offset: 0x002897D2
		public virtual string TagStartSymbol
		{
			get
			{
				return this.tagStartSymbol;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x0028B5DA File Offset: 0x002897DA
		public virtual string TagEndSymbol
		{
			get
			{
				return this.tagEndSymbol;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060067EC RID: 26604 RVA: 0x0028B5E2 File Offset: 0x002897E2
		public virtual string ReplaceTagStartWith
		{
			get
			{
				return this.replaceTagStartWith;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060067ED RID: 26605 RVA: 0x0028B5EA File Offset: 0x002897EA
		public virtual string ReplaceTagEndWith
		{
			get
			{
				return this.replaceTagEndWith;
			}
		}

		// Token: 0x040058A3 RID: 22691
		[Tooltip("String that defines the start of the tag.")]
		[SerializeField]
		protected string tagStartSymbol;

		// Token: 0x040058A4 RID: 22692
		[Tooltip("String that defines the end of the tag.")]
		[SerializeField]
		protected string tagEndSymbol;

		// Token: 0x040058A5 RID: 22693
		[Tooltip("String to replace the start tag with.")]
		[SerializeField]
		protected string replaceTagStartWith;

		// Token: 0x040058A6 RID: 22694
		[Tooltip("String to replace the end tag with.")]
		[SerializeField]
		protected string replaceTagEndWith;

		// Token: 0x040058A7 RID: 22695
		public static List<CustomTag> activeCustomTags = new List<CustomTag>();
	}
}
