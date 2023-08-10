using KBEngine;
using UnityEngine;

public class WorkingStation : MonoBehaviour
{
	public KeyCode openInventory;

	public GameObject craftSystem;

	public int distanceToOpenWorkingStation = 3;

	private bool showCraftSystem;

	private Inventory craftInventory;

	private CraftSystem cS;

	private void Start()
	{
		if ((Object)(object)craftSystem != (Object)null)
		{
			craftInventory = craftSystem.GetComponent<Inventory>();
			cS = craftSystem.GetComponent<CraftSystem>();
		}
	}

	private void Update()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector3.Distance(((Component)this).gameObject.transform.position, ((GameObject)KBEngineApp.app.player().renderObj).transform.position);
		if (Input.GetKeyDown(openInventory) && num <= (float)distanceToOpenWorkingStation)
		{
			showCraftSystem = !showCraftSystem;
			if (showCraftSystem)
			{
				craftInventory.openInventory();
			}
			else
			{
				cS.backToInventory();
				craftInventory.closeInventory();
			}
		}
		if (showCraftSystem && num > (float)distanceToOpenWorkingStation)
		{
			cS.backToInventory();
			craftInventory.closeInventory();
		}
	}
}
