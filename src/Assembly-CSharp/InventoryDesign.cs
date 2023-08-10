using UnityEngine;
using UnityEngine.UI;

public class InventoryDesign : MonoBehaviour
{
	[SerializeField]
	public Image slotDesignTemp;

	[SerializeField]
	public Image slotDesign;

	[SerializeField]
	public Image inventoryDesign;

	[SerializeField]
	public bool showInventoryCross;

	[SerializeField]
	public Image inventoryCrossImage;

	[SerializeField]
	public RectTransform inventoryCrossRectTransform;

	[SerializeField]
	public int inventoryCrossPosX;

	[SerializeField]
	public int inventoryCrossPosY;

	[SerializeField]
	public string inventoryTitle;

	[SerializeField]
	public Text inventoryTitleText;

	[SerializeField]
	public int inventoryTitlePosX;

	[SerializeField]
	public int inventoryTitlePosY;

	[SerializeField]
	public int panelSizeX;

	[SerializeField]
	public int panelSizeY;

	public void setVariables()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		inventoryTitlePosX = (int)((Transform)((Component)((Component)this).transform.GetChild(0)).GetComponent<RectTransform>()).localPosition.x;
		inventoryTitlePosY = (int)((Transform)((Component)((Component)this).transform.GetChild(0)).GetComponent<RectTransform>()).localPosition.y;
		panelSizeX = (int)((Component)this).GetComponent<RectTransform>().sizeDelta.x;
		panelSizeY = (int)((Component)this).GetComponent<RectTransform>().sizeDelta.y;
		inventoryTitle = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Text>().text;
		inventoryTitleText = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Text>();
		if ((Object)(object)((Component)this).GetComponent<Hotbar>() == (Object)null)
		{
			inventoryCrossRectTransform = ((Component)((Component)this).transform.GetChild(2)).GetComponent<RectTransform>();
			inventoryCrossImage = ((Component)((Component)this).transform.GetChild(2)).GetComponent<Image>();
		}
		inventoryDesign = ((Component)this).GetComponent<Image>();
		slotDesign = ((Component)((Component)this).transform.GetChild(1).GetChild(0)).GetComponent<Image>();
		slotDesignTemp = slotDesign;
		slotDesignTemp.sprite = slotDesign.sprite;
		((Graphic)slotDesignTemp).color = ((Graphic)slotDesign).color;
		((Graphic)slotDesignTemp).material = ((Graphic)slotDesign).material;
		slotDesignTemp.type = slotDesign.type;
		slotDesignTemp.fillCenter = slotDesign.fillCenter;
	}

	public void updateEverything()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		((Transform)((Component)((Component)this).transform.GetChild(0)).GetComponent<RectTransform>()).localPosition = new Vector3((float)inventoryTitlePosX, (float)inventoryTitlePosY, 0f);
		((Component)((Component)this).transform.GetChild(0)).GetComponent<Text>().text = inventoryTitle;
	}

	public void changeCrossSettings()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = ((Component)((Component)this).transform.GetChild(2)).gameObject;
		if (showInventoryCross)
		{
			gameObject.SetActive(showInventoryCross);
			((Transform)inventoryCrossRectTransform).localPosition = new Vector3((float)inventoryCrossPosX, (float)inventoryCrossPosY, 0f);
		}
		else
		{
			gameObject.SetActive(showInventoryCross);
		}
	}

	public void updateAllSlots()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ((Component)this).transform.GetChild(1).childCount; i++)
		{
			Image component = ((Component)((Component)this).transform.GetChild(1).GetChild(i)).GetComponent<Image>();
			component.sprite = slotDesignTemp.sprite;
			((Graphic)component).color = ((Graphic)slotDesignTemp).color;
			((Graphic)component).material = ((Graphic)slotDesignTemp).material;
			component.type = slotDesignTemp.type;
			component.fillCenter = slotDesignTemp.fillCenter;
		}
	}
}
