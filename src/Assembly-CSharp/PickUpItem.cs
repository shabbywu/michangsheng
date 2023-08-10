using KBEngine;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
	public Item item;

	private Inventory _inventory;

	private GameObject _player;

	private int nowAttakBtnStatus;

	private void Start()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		_player = (GameObject)KBEngineApp.app.player().renderObj;
		if ((Object)(object)_player != (Object)null)
		{
			_inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		((MonoBehaviour)this).InvokeRepeating("cheakPlayer", 0.5f, 0.5f);
	}

	public void cheakPlayer()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (Vector3.Distance(((Component)this).gameObject.transform.position, _player.transform.position) <= 3f)
		{
			if (UI_MainUI.inst.skill1Text.text != "拾取")
			{
				nowAttakBtnStatus = 1;
				UI_MainUI.inst.setSkill1("拾取");
			}
		}
		else if (nowAttakBtnStatus == 1)
		{
			UI_MainUI.inst.skill1Text.text = "攻击";
			nowAttakBtnStatus = 0;
		}
	}

	public void OnEnable()
	{
		UI_Game.ItemPick += OnItemPick;
	}

	public void OnDisable()
	{
		UI_Game.ItemPick -= OnItemPick;
		UI_MainUI.inst.skill1Text.text = "攻击";
	}

	private void OnItemPick()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_player == (Object)null)
		{
			_player = (GameObject)KBEngineApp.app.player().renderObj;
		}
		if ((Object)(object)_inventory == (Object)null && (Object)(object)_player != (Object)null)
		{
			_inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
		}
		if ((Object)(object)_inventory != (Object)null && Vector3.Distance(((Component)this).gameObject.transform.position, _player.transform.position) <= 3f && _inventory.ItemsInInventory.Count < _inventory.width * _inventory.height)
		{
			((DroppedItem)KBEngineApp.app.findEntity(Utility.getPostInt(((Object)((Component)this).gameObject).name)))?.pickUpRequest();
		}
	}
}
