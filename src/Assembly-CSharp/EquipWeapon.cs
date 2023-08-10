using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
	public Transform[] weapon;

	public Transform weaponHand;

	private Transform currentweapon;

	private Dictionary<int, Transform> models = new Dictionary<int, Transform>();

	private bool isInit;

	private void Start()
	{
		init();
	}

	private void init()
	{
	}

	public void equipWeapon(int wIndex)
	{
	}

	public void clearWeapon()
	{
		if ((Object)(object)currentweapon != (Object)null)
		{
			currentweapon.parent = null;
			Object.Destroy((Object)(object)((Component)currentweapon).gameObject);
		}
	}

	private void OnDestroy()
	{
		clearWeapon();
	}
}
