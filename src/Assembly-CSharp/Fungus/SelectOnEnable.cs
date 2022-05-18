using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012F6 RID: 4854
	[RequireComponent(typeof(Selectable))]
	public class SelectOnEnable : MonoBehaviour
	{
		// Token: 0x06007660 RID: 30304 RVA: 0x00050927 File Offset: 0x0004EB27
		protected void Awake()
		{
			this.selectable = base.GetComponent<Selectable>();
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x00050935 File Offset: 0x0004EB35
		protected void OnEnable()
		{
			this.selectable.Select();
		}

		// Token: 0x04006738 RID: 26424
		protected Selectable selectable;
	}
}
