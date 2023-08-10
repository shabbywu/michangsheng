using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public Tooltip tooltip;

	public GameObject tooltipGameObject;

	public RectTransform canvasRectTransform;

	public RectTransform tooltipRectTransform;

	private Item item;

	private void Start()
	{
		GameObject val = GameObject.FindGameObjectWithTag("Canvas");
		if ((Object)(object)val.transform.Find("Tooltip - Inventory(Clone)") != (Object)null)
		{
			tooltip = ((Component)val.transform.Find("Tooltip - Inventory(Clone)")).GetComponent<Tooltip>();
			tooltipGameObject = ((Component)val.transform.Find("Tooltip - Inventory(Clone)")).gameObject;
			tooltipRectTransform = tooltipGameObject.GetComponent<RectTransform>();
		}
		canvasRectTransform = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
	}

	public void OnPointerDown(PointerEventData data)
	{
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)tooltip != (Object)null))
		{
			return;
		}
		tooltip.tooltipType = TooltipType.Inventory;
		Transform val = ((Component)this).transform;
		while (Object.op_Implicit((Object)(object)val.parent))
		{
			if (((Component)val).tag == "MainInventory")
			{
				tooltip.tooltipType = TooltipType.Inventory;
				break;
			}
			if (((Component)val).tag == "EquipmentSystem")
			{
				tooltip.tooltipType = TooltipType.Equipment;
				break;
			}
			val = val.parent;
		}
		item = ((Component)this).GetComponent<ItemOnObject>().item;
		tooltip.item = item;
		tooltip.activateTooltip();
		if ((Object)(object)canvasRectTransform == (Object)null)
		{
			return;
		}
		Vector3[] array = (Vector3[])(object)new Vector3[4];
		((Component)this).GetComponent<RectTransform>().GetWorldCorners(array);
		Vector2 val2 = default(Vector2);
		Vector2 val3 = default(Vector2);
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Vector2.op_Implicit(array[3]), data.pressEventCamera, ref val2) && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Vector2.op_Implicit(array[1]), data.pressEventCamera, ref val3))
		{
			float num = val2.x;
			float num2 = val3.y;
			float num3 = num;
			Rect rect = tooltipRectTransform.rect;
			float num4 = num3 + ((Rect)(ref rect)).width;
			rect = canvasRectTransform.rect;
			if (num4 > ((Rect)(ref rect)).xMax)
			{
				float x = val3.x;
				rect = tooltipRectTransform.rect;
				num = x - ((Rect)(ref rect)).width;
			}
			float num5 = num2;
			rect = tooltipRectTransform.rect;
			float num6 = num5 - ((Rect)(ref rect)).height;
			rect = canvasRectTransform.rect;
			if (num6 < ((Rect)(ref rect)).yMin)
			{
				float num7 = num2;
				rect = canvasRectTransform.rect;
				float num8 = ((Rect)(ref rect)).yMin - num2;
				rect = tooltipRectTransform.rect;
				num2 = num7 + (num8 + ((Rect)(ref rect)).height);
			}
			((Transform)tooltipRectTransform).localPosition = new Vector3(num, num2);
		}
	}
}
public class showTooltip : MonoBehaviour
{
	private static showTooltip instence;

	public bool ISshowTooltip;

	public static showTooltip Instence
	{
		get
		{
			_ = (Object)(object)instence == (Object)null;
			return instence;
		}
	}

	private void Start()
	{
		instence = this;
	}

	private void OnDestroy()
	{
		instence = null;
	}

	private void Update()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (ISshowTooltip)
		{
			Vector3 val = default(Vector3);
			((Vector3)(ref val))._002Ector(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			if (Input.mousePosition.x > (float)(Screen.width / 2))
			{
				val.x -= ((Component)this).GetComponentInChildren<UISprite>().width;
			}
			if (Input.mousePosition.y < (float)(Screen.height / 2))
			{
				val.y += ((Component)this).GetComponentInChildren<UISprite>().height;
			}
			((Component)this).transform.position = UICamera.currentCamera.ScreenToWorldPoint(val);
		}
		else
		{
			((Component)this).transform.position = new Vector3(0f, 10000f, 0f);
		}
	}
}
