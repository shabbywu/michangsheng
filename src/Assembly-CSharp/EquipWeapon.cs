using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class EquipWeapon : MonoBehaviour
{
	// Token: 0x06001425 RID: 5157 RVA: 0x000827C4 File Offset: 0x000809C4
	private void Start()
	{
		this.init();
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x00004095 File Offset: 0x00002295
	private void init()
	{
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x00004095 File Offset: 0x00002295
	public void equipWeapon(int wIndex)
	{
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000827CC File Offset: 0x000809CC
	public void clearWeapon()
	{
		if (this.currentweapon != null)
		{
			this.currentweapon.parent = null;
			Object.Destroy(this.currentweapon.gameObject);
		}
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000827F8 File Offset: 0x000809F8
	private void OnDestroy()
	{
		this.clearWeapon();
	}

	// Token: 0x04000EF3 RID: 3827
	public Transform[] weapon;

	// Token: 0x04000EF4 RID: 3828
	public Transform weaponHand;

	// Token: 0x04000EF5 RID: 3829
	private Transform currentweapon;

	// Token: 0x04000EF6 RID: 3830
	private Dictionary<int, Transform> models = new Dictionary<int, Transform>();

	// Token: 0x04000EF7 RID: 3831
	private bool isInit;
}
