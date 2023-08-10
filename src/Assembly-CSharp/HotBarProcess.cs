using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotBarProcess : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public static HotBarProcess _instance;

	private Inventory inventory;

	private int itemId = -1;

	private Text text;

	private Image image_icon;

	private Image image_cool;

	private void Start()
	{
		GameObject val = GameObject.FindGameObjectWithTag("Canvas");
		if ((Object)(object)val.transform.Find("Panel - Inventory(Clone)") != (Object)null)
		{
			inventory = ((Component)val.transform.Find("Panel - Inventory(Clone)")).GetComponent<Inventory>();
		}
		((Component)this).gameObject.SetActive(false);
	}

	private void Awake()
	{
		_instance = this;
		image_icon = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Image>();
		text = ((Component)((Component)this).transform.GetChild(1)).GetComponent<Text>();
		image_cool = ((Component)((Component)this).transform.GetChild(2)).GetComponent<Image>();
	}

	private void Update()
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		if (itemId == -1)
		{
			return;
		}
		if ((Object)(object)inventory == (Object)null)
		{
			GameObject val = GameObject.FindGameObjectWithTag("Canvas");
			inventory = ((Component)val.transform.Find("Panel - Inventory(Clone)")).GetComponent<Inventory>();
		}
		if ((Object)(object)inventory == (Object)null)
		{
			return;
		}
		GameObject itemGameObject = inventory.getItemGameObject(itemId);
		if ((Object)(object)itemGameObject == (Object)null)
		{
			itemId = -1;
			((Component)this).gameObject.SetActive(false);
			return;
		}
		image_icon.sprite = itemGameObject.GetComponent<ItemOnObject>().item.itemIcon;
		((Transform)((Graphic)text).rectTransform).localPosition = ((Component)((Component)itemGameObject.transform.GetChild(1)).GetComponent<Text>()).transform.localPosition;
		((Behaviour)text).enabled = true;
		text.text = ((Component)itemGameObject.transform.GetChild(1)).GetComponent<Text>().text;
		if (ConsumeLimitCD.instance.isWaiting())
		{
			image_cool.fillAmount = ConsumeLimitCD.instance.restTime / ConsumeLimitCD.instance.totalTime;
		}
		else
		{
			image_cool.fillAmount = 0f;
		}
	}

	public void upItem(int itemId)
	{
		this.itemId = itemId;
		((Component)this).gameObject.SetActive(true);
	}

	public void OnPointerDown(PointerEventData data)
	{
		GameObject itemGameObject = inventory.getItemGameObject(itemId);
		if ((Object)(object)itemGameObject != (Object)null)
		{
			((Avatar)KBEngineApp.app.player())?.useItemRequest((ulong)itemGameObject.GetComponent<ItemOnObject>().item.itemIndex);
		}
	}
}
