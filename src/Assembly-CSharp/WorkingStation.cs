using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class WorkingStation : MonoBehaviour
{
	// Token: 0x0600103F RID: 4159 RVA: 0x000103FB File Offset: 0x0000E5FB
	private void Start()
	{
		if (this.craftSystem != null)
		{
			this.craftInventory = this.craftSystem.GetComponent<Inventory>();
			this.cS = this.craftSystem.GetComponent<CraftSystem>();
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x000A4BDC File Offset: 0x000A2DDC
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

	// Token: 0x04000CD0 RID: 3280
	public KeyCode openInventory;

	// Token: 0x04000CD1 RID: 3281
	public GameObject craftSystem;

	// Token: 0x04000CD2 RID: 3282
	public int distanceToOpenWorkingStation = 3;

	// Token: 0x04000CD3 RID: 3283
	private bool showCraftSystem;

	// Token: 0x04000CD4 RID: 3284
	private Inventory craftInventory;

	// Token: 0x04000CD5 RID: 3285
	private CraftSystem cS;
}
