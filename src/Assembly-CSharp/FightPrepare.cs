using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000188 RID: 392
public class FightPrepare : MonoBehaviour
{
	// Token: 0x060010CB RID: 4299 RVA: 0x00063B67 File Offset: 0x00061D67
	private void Start()
	{
		this.slot = (Resources.Load("Slot") as GameObject);
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x00063B80 File Offset: 0x00061D80
	public void initFightPrepare()
	{
		UINPCLeftList.ShoudHide = true;
		this.MonstarID = Tools.instance.MonstarID;
		base.transform.Find("Panel/monstarTexture/Canvas/ScrollViewFace/Viewport/Content/FaceBase/SkeletonGraphic (Doi)").GetComponent<PlayerSetRandomFace>().randomAvatar(Tools.instance.MonstarID);
		base.transform.Find("MonstarPanel/Texture/Canvas/ScrollViewFace/Viewport/Content/FaceBase/SkeletonGraphic (Doi)").GetComponent<PlayerSetRandomFace>().randomAvatar(Tools.instance.MonstarID);
		base.transform.Find("Panel/monstarTexture/GameObject/Label").GetComponent<UILabel>().text = Tools.getMonstarTitle(this.MonstarID);
		base.transform.Find("MonstarPanel/Title").GetComponent<UILabel>().text = Tools.getMonstarTitle(this.MonstarID);
		base.transform.Find("Panel/playerTexture/GameObject/Label").GetComponent<UILabel>().text = Tools.getMonstarTitle(1);
		base.transform.Find("PlayerPanel/Title").GetComponent<UILabel>().text = Tools.getMonstarTitle(1);
		this.initMonstarPlan();
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00063C7C File Offset: 0x00061E7C
	public void initMonstarPlan()
	{
		ItemDatebase component = jsonData.instance.transform.GetComponent<ItemDatebase>();
		Transform transform = base.transform.Find("MonstarPanel");
		Transform transform2 = transform.transform.Find("short cut5").Find("Key");
		Transform transform3 = transform.transform.Find("short cut6").Find("Key");
		EquipCell component2 = transform.transform.Find("GameObject/Weapon").GetComponent<EquipCell>();
		EquipCell component3 = transform.transform.Find("GameObject/Clothing").GetComponent<EquipCell>();
		EquipCell component4 = transform.transform.Find("GameObject/Ring").GetComponent<EquipCell>();
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[string.Concat(this.MonstarID)];
		int num = (int)jsonobject["Level"].n;
		Dictionary<int, GUIPackage.Skill> dicSkills = SkillStaticDatebase.instence.dicSkills;
		Dictionary<int, GUIPackage.Skill> dicSkills2 = SkillDatebase.instence.dicSkills;
		int num2 = 0;
		foreach (JSONObject jsonobject2 in jsonobject["staticSkills"].list)
		{
			transform2.GetChild(num2).GetComponent<KeyCellMapPassSkill>().keySkill = dicSkills[(int)jsonobject2.n];
			num2++;
		}
		num2 = 0;
		foreach (JSONObject jsonobject3 in jsonobject["skills"].list)
		{
			int num3 = 0;
			foreach (JSONObject jsonobject4 in jsonData.instance._skillJsonData.list)
			{
				if (jsonobject4["Skill_ID"].I == jsonobject3.I && (num - 1) / 3 + 1 == jsonobject4["Skill_Lv"].I)
				{
					num3 = jsonobject4["id"].I;
				}
			}
			if (!dicSkills2.ContainsKey(num3))
			{
				Debug.LogError(this.MonstarID + "配置的技能错误" + num3);
			}
			transform3.GetChild(num2).GetComponent<KeyCellMapSkill>().keySkill = dicSkills2[num3];
			num2++;
		}
		JSONObject jsonobject5 = jsonData.instance.AvatarJsonData[string.Concat(this.MonstarID)];
		if ((int)jsonobject5["equipWeapon"].n > 0)
		{
			component2.Item = component.items[(int)jsonobject5["equipWeapon"].n];
		}
		else
		{
			component2.Item = new item();
		}
		if ((int)jsonobject5["equipClothing"].n > 0)
		{
			component3.Item = component.items[(int)jsonobject5["equipClothing"].n];
		}
		else
		{
			component3.Item = new item();
		}
		if ((int)jsonobject5["equipRing"].n > 0)
		{
			component4.Item = component.items[(int)jsonobject5["equipRing"].n];
			return;
		}
		component4.Item = new item();
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x00064008 File Offset: 0x00062208
	public void initPlayerPlane()
	{
		Transform transform = base.transform.Find("PlayerPanel");
		Key component = transform.transform.Find("short cut5").Find("Key").GetComponent<Key>();
		transform.transform.Find("short cut6").Find("Key").GetComponent<Key>().LoadMapKey();
		component.LoadMapPassKey();
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0006406E File Offset: 0x0006226E
	public void openMonstarPlane()
	{
		this.monstarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.monstarPlane.SetActive(true);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x000640A0 File Offset: 0x000622A0
	public void openPlayerPlane()
	{
		this.initPlayerPlane();
		this.avatarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.avatarPlane.SetActive(true);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000640D8 File Offset: 0x000622D8
	public void openUseDanyaoPlan()
	{
		this.useDanyaoPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.useDanyaoPlane.SetActive(true);
		this.initPreperDanYao();
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x00064110 File Offset: 0x00062310
	public void initPreperDanYao()
	{
		Avatar player = Tools.instance.getPlayer();
		Transform transform = this.inventory.InventoryUI.transform.Find("Win/item").transform;
		for (int i = 0; i < this.inventory.inventory.Count; i++)
		{
			this.inventory.inventory[i] = new item();
			this.inventory.inventory[i].itemNum = 1;
			transform.GetChild(i).GetComponent<ItemCell>().ISPrepare = true;
		}
		foreach (ITEM_INFO item_INFO in player.itemList.values)
		{
			if ((int)jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["type"].n == 5)
			{
				for (int j = 0; j < this.inventory.inventory.Count; j++)
				{
					if (this.inventory.inventory[j].itemID == -1)
					{
						this.inventory.inventory[j] = this.inventory.datebase.items[item_INFO.itemId].Clone();
						this.inventory.inventory[j].itemNum = (int)item_INFO.itemCount;
						break;
					}
				}
			}
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000642B4 File Offset: 0x000624B4
	public void resetDanyaoPlan()
	{
		this.useDanyaoPlane.transform.Find("Scroll View/UIGrid");
		this.useDanyaoPlane.transform.Find("Scroll View").GetComponent<UIScrollView>();
		Tools.instance.getPlayer();
		Dictionary<int, item> items = jsonData.instance.gameObject.GetComponent<ItemDatebase>().items;
		this.inventory.setItemLeiXin5();
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0006431D File Offset: 0x0006251D
	public void closeMonstarPlane()
	{
		this.monstarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00064343 File Offset: 0x00062543
	public void closePlayerPlane()
	{
		this.avatarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x00064369 File Offset: 0x00062569
	public void closeUseDanyaoPlan()
	{
		this.useDanyaoPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00064390 File Offset: 0x00062590
	public void startFight()
	{
		Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
		Tools.instance.loadOtherScenes("YSFight");
	}

	// Token: 0x04000C10 RID: 3088
	public GameObject monstarPlane;

	// Token: 0x04000C11 RID: 3089
	public GameObject avatarPlane;

	// Token: 0x04000C12 RID: 3090
	public GameObject useDanyaoPlane;

	// Token: 0x04000C13 RID: 3091
	public Inventory2 inventory;

	// Token: 0x04000C14 RID: 3092
	private int MonstarID;

	// Token: 0x04000C15 RID: 3093
	private GameObject slot;
}
