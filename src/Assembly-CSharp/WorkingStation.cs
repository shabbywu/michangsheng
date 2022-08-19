using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class WorkingStation : MonoBehaviour
{
	// Token: 0x06000E31 RID: 3633 RVA: 0x00054549 File Offset: 0x00052749
	private void Start()
	{
		if (this.craftSystem != null)
		{
			this.craftInventory = this.craftSystem.GetComponent<Inventory>();
			this.cS = this.craftSystem.GetComponent<CraftSystem>();
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0005457C File Offset: 0x0005277C
	private void Update()
	{
		float num = Vector3.Distance(base.gameObject.transform.position, ((GameObject)KBEngineApp.app.player().renderObj).transform.position);
		if (Input.GetKeyDown(this.openInventory) && num <= (float)this.distanceToOpenWorkingStation)
		{
			this.showCraftSystem = !this.showCraftSystem;
			if (this.showCraftSystem)
			{
				this.craftInventory.openInventory();
			}
			else
			{
				this.cS.backToInventory();
				this.craftInventory.closeInventory();
			}
		}
		if (this.showCraftSystem && num > (float)this.distanceToOpenWorkingStation)
		{
			this.cS.backToInventory();
			this.craftInventory.closeInventory();
		}
	}

	// Token: 0x04000A38 RID: 2616
	public KeyCode openInventory;

	// Token: 0x04000A39 RID: 2617
	public GameObject craftSystem;

	// Token: 0x04000A3A RID: 2618
	public int distanceToOpenWorkingStation = 3;

	// Token: 0x04000A3B RID: 2619
	private bool showCraftSystem;

	// Token: 0x04000A3C RID: 2620
	private Inventory craftInventory;

	// Token: 0x04000A3D RID: 2621
	private CraftSystem cS;
}
