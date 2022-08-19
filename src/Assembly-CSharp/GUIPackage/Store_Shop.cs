using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A70 RID: 2672
	public class Store_Shop : MonoBehaviour
	{
		// Token: 0x06004B1E RID: 19230 RVA: 0x001FF307 File Offset: 0x001FD507
		private void Start()
		{
			this.id = base.transform.parent.GetComponentInChildren<StoreCell>().storeID;
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x001FF324 File Offset: 0x001FD524
		private void OnPress()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Singleton.store.ShowNumInput(this.id);
			}
		}

		// Token: 0x04004A43 RID: 19011
		private int id;
	}
}
