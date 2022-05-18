using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class EquipWeapon : MonoBehaviour
{
	// Token: 0x060016CA RID: 5834 RVA: 0x0001432E File Offset: 0x0001252E
	private void Start()
	{
		this.init();
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000042DD File Offset: 0x000024DD
	private void init()
	{
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000042DD File Offset: 0x000024DD
	public void equipWeapon(int wIndex)
	{
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x00014336 File Offset: 0x00012536
	public void clearWeapon()
	{
		if (this.currentweapon != null)
		{
			this.currentweapon.parent = null;
			Object.Destroy(this.currentweapon.gameObject);
		}
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x00014362 File Offset: 0x00012562
	private void OnDestroy()
	{
		this.clearWeapon();
	}

	// Token: 0x04001231 RID: 4657
	public Transform[] weapon;

	// Token: 0x04001232 RID: 4658
	public Transform weaponHand;

	// Token: 0x04001233 RID: 4659
	private Transform currentweapon;

	// Token: 0x04001234 RID: 4660
	private Dictionary<int, Transform> models = new Dictionary<int, Transform>();

	// Token: 0x04001235 RID: 4661
	private bool isInit;
}
