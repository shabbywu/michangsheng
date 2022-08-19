using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E85 RID: 3717
	[RequireComponent(typeof(Selectable))]
	public class SelectOnEnable : MonoBehaviour
	{
		// Token: 0x06006961 RID: 26977 RVA: 0x00290E90 File Offset: 0x0028F090
		protected void Awake()
		{
			this.selectable = base.GetComponent<Selectable>();
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x00290E9E File Offset: 0x0028F09E
		protected void OnEnable()
		{
			this.selectable.Select();
		}

		// Token: 0x04005957 RID: 22871
		protected Selectable selectable;
	}
}
