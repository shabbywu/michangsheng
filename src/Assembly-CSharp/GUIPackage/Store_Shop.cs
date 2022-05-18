using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D9F RID: 3487
	public class Store_Shop : MonoBehaviour
	{
		// Token: 0x06005432 RID: 21554 RVA: 0x0003C3E9 File Offset: 0x0003A5E9
		private void Start()
		{
			this.id = base.transform.parent.GetComponentInChildren<StoreCell>().storeID;
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0003C406 File Offset: 0x0003A606
		private void OnPress()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Singleton.store.ShowNumInput(this.id);
			}
		}

		// Token: 0x040053E8 RID: 21480
		private int id;
	}
}
