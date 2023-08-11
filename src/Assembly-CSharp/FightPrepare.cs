using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightPrepare : MonoBehaviour
{
	public GameObject monstarPlane;

	public GameObject avatarPlane;

	public GameObject useDanyaoPlane;

	public Inventory2 inventory;

	private int MonstarID;

	private GameObject slot;

	private void Start()
	{
		ref GameObject reference = ref slot;
		Object obj = Resources.Load("Slot");
		reference = (GameObject)(object)((obj is GameObject) ? obj : null);
	}

	public void initFightPrepare()
	{
		UINPCLeftList.ShoudHide = true;
		MonstarID = Tools.instance.MonstarID;
		((Component)((Component)this).transform.Find("Panel/monstarTexture/Canvas/ScrollViewFace/Viewport/Content/FaceBase/SkeletonGraphic (Doi)")).GetComponent<PlayerSetRandomFace>().randomAvatar(Tools.instance.MonstarID);
		((Component)((Component)this).transform.Find("MonstarPanel/Texture/Canvas/ScrollViewFace/Viewport/Content/FaceBase/SkeletonGraphic (Doi)")).GetComponent<PlayerSetRandomFace>().randomAvatar(Tools.instance.MonstarID);
		((Component)((Component)this).transform.Find("Panel/monstarTexture/GameObject/Label")).GetComponent<UILabel>().text = Tools.getMonstarTitle(MonstarID);
		((Component)((Component)this).transform.Find("MonstarPanel/Title")).GetComponent<UILabel>().text = Tools.getMonstarTitle(MonstarID);
		((Component)((Component)this).transform.Find("Panel/playerTexture/GameObject/Label")).GetComponent<UILabel>().text = Tools.getMonstarTitle(1);
		((Component)((Component)this).transform.Find("PlayerPanel/Title")).GetComponent<UILabel>().text = Tools.getMonstarTitle(1);
		initMonstarPlan();
	}

	public void initMonstarPlan()
	{
		ItemDatebase component = ((Component)((Component)jsonData.instance).transform).GetComponent<ItemDatebase>();
		Transform obj = ((Component)this).transform.Find("MonstarPanel");
		Transform val = ((Component)obj).transform.Find("short cut5").Find("Key");
		Transform val2 = ((Component)obj).transform.Find("short cut6").Find("Key");
		EquipCell component2 = ((Component)((Component)obj).transform.Find("GameObject/Weapon")).GetComponent<EquipCell>();
		EquipCell component3 = ((Component)((Component)obj).transform.Find("GameObject/Clothing")).GetComponent<EquipCell>();
		EquipCell component4 = ((Component)((Component)obj).transform.Find("GameObject/Ring")).GetComponent<EquipCell>();
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[string.Concat(MonstarID)];
		int num = (int)jSONObject["Level"].n;
		Dictionary<int, GUIPackage.Skill> dicSkills = SkillStaticDatebase.instence.dicSkills;
		Dictionary<int, GUIPackage.Skill> dicSkills2 = SkillDatebase.instence.dicSkills;
		int num2 = 0;
		foreach (JSONObject item in jSONObject["staticSkills"].list)
		{
			((Component)val.GetChild(num2)).GetComponent<KeyCellMapPassSkill>().keySkill = dicSkills[(int)item.n];
			num2++;
		}
		num2 = 0;
		foreach (JSONObject item2 in jSONObject["skills"].list)
		{
			int num3 = 0;
			foreach (JSONObject item3 in jsonData.instance._skillJsonData.list)
			{
				if (item3["Skill_ID"].I == item2.I && (num - 1) / 3 + 1 == item3["Skill_Lv"].I)
				{
					num3 = item3["id"].I;
				}
			}
			if (!dicSkills2.ContainsKey(num3))
			{
				Debug.LogError((object)(MonstarID + "配置的技能错误" + num3));
			}
			((Component)val2.GetChild(num2)).GetComponent<KeyCellMapSkill>().keySkill = dicSkills2[num3];
			num2++;
		}
		JSONObject jSONObject2 = jsonData.instance.AvatarJsonData[string.Concat(MonstarID)];
		if ((int)jSONObject2["equipWeapon"].n > 0)
		{
			component2.Item = component.items[(int)jSONObject2["equipWeapon"].n];
		}
		else
		{
			component2.Item = new item();
		}
		if ((int)jSONObject2["equipClothing"].n > 0)
		{
			component3.Item = component.items[(int)jSONObject2["equipClothing"].n];
		}
		else
		{
			component3.Item = new item();
		}
		if ((int)jSONObject2["equipRing"].n > 0)
		{
			component4.Item = component.items[(int)jSONObject2["equipRing"].n];
		}
		else
		{
			component4.Item = new item();
		}
	}

	public void initPlayerPlane()
	{
		Transform obj = ((Component)this).transform.Find("PlayerPanel");
		Key component = ((Component)((Component)obj).transform.Find("short cut5").Find("Key")).GetComponent<Key>();
		((Component)((Component)obj).transform.Find("short cut6").Find("Key")).GetComponent<Key>().LoadMapKey();
		component.LoadMapPassKey();
	}

	public void openMonstarPlane()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		monstarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		monstarPlane.SetActive(true);
	}

	public void openPlayerPlane()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		initPlayerPlane();
		avatarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		avatarPlane.SetActive(true);
	}

	public void openUseDanyaoPlan()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		useDanyaoPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		useDanyaoPlane.SetActive(true);
		initPreperDanYao();
	}

	public void initPreperDanYao()
	{
		Avatar player = Tools.instance.getPlayer();
		Transform transform = ((Component)inventory.InventoryUI.transform.Find("Win/item")).transform;
		for (int i = 0; i < inventory.inventory.Count; i++)
		{
			inventory.inventory[i] = new item();
			inventory.inventory[i].itemNum = 1;
			((Component)transform.GetChild(i)).GetComponent<ItemCell>().ISPrepare = true;
		}
		foreach (ITEM_INFO value in player.itemList.values)
		{
			if ((int)jsonData.instance.ItemJsonData[string.Concat(value.itemId)]["type"].n != 5)
			{
				continue;
			}
			for (int j = 0; j < inventory.inventory.Count; j++)
			{
				if (inventory.inventory[j].itemID == -1)
				{
					inventory.inventory[j] = inventory.datebase.items[value.itemId].Clone();
					inventory.inventory[j].itemNum = (int)value.itemCount;
					break;
				}
			}
		}
	}

	public void resetDanyaoPlan()
	{
		useDanyaoPlane.transform.Find("Scroll View/UIGrid");
		((Component)useDanyaoPlane.transform.Find("Scroll View")).GetComponent<UIScrollView>();
		Tools.instance.getPlayer();
		_ = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>().items;
		inventory.setItemLeiXin5();
	}

	public void closeMonstarPlane()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		monstarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	public void closePlayerPlane()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		avatarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	public void closeUseDanyaoPlan()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		useDanyaoPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	public void startFight()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		Tools instance = Tools.instance;
		Scene activeScene = SceneManager.GetActiveScene();
		instance.FinalScene = ((Scene)(ref activeScene)).name;
		Tools.instance.loadOtherScenes("YSFight");
	}
}
