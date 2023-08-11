using UnityEngine;

namespace GUIPackage;

public class Store_Shop : MonoBehaviour
{
	private int id;

	private void Start()
	{
		id = ((Component)((Component)this).transform.parent).GetComponentInChildren<StoreCell>().storeID;
	}

	private void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Singleton.store.ShowNumInput(id);
		}
	}
}
