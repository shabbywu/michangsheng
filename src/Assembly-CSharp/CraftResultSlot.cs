using UnityEngine;
using UnityEngine.UI;

public class CraftResultSlot : MonoBehaviour
{
	private CraftSystem craftSystem;

	public int temp;

	private GameObject itemGameObject;

	private void Start()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		craftSystem = ((Component)((Component)this).transform.parent).GetComponent<CraftSystem>();
		ref GameObject reference = ref itemGameObject;
		Object obj = Resources.Load("Prefabs/Item");
		reference = Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null));
		itemGameObject.transform.SetParent(((Component)this).gameObject.transform);
		((Transform)itemGameObject.GetComponent<RectTransform>()).localPosition = Vector3.zero;
		((Behaviour)itemGameObject.GetComponent<DragItem>()).enabled = false;
		itemGameObject.SetActive(false);
		((Behaviour)((Component)itemGameObject.transform.GetChild(1)).GetComponent<Text>()).enabled = true;
		((Transform)((Component)itemGameObject.transform.GetChild(1)).GetComponent<RectTransform>()).localPosition = Vector2.op_Implicit(new Vector2((float)GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().positionNumberX, (float)GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().positionNumberY));
	}

	private void Update()
	{
		if (craftSystem.possibleItems.Count != 0)
		{
			itemGameObject.GetComponent<ItemOnObject>().item = craftSystem.possibleItems[temp];
			itemGameObject.SetActive(true);
		}
		else
		{
			itemGameObject.SetActive(false);
		}
	}
}
