using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200026E RID: 622
public class FightPrepare : MonoBehaviour
{
	// Token: 0x06001332 RID: 4914 RVA: 0x00012070 File Offset: 0x00010270
	private void Start()
	{
		this.slot = (Resources.Load("Slot") as GameObject);
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000B25C4 File Offset: 0x000B07C4
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

	// Token: 0x06001334 RID: 4916 RVA: 0x000B26C0 File Offset: 0x000B08C0
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
				if ((int)jsonobject4["Skill_ID"].n == (int)jsonobject3.n && (num - 1) / 3 + 1 == (int)jsonobject4["Skill_Lv"].n)
				{
					num3 = (int)jsonobject4["id"].n;
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

	// Token: 0x06001335 RID: 4917 RVA: 0x000B2A50 File Offset: 0x000B0C50
	public void initPlayerPlane()
	{
		Transform transform = base.transform.Find("PlayerPanel");
		Key component = transform.transform.Find("short cut5").Find("Key").GetComponent<Key>();
		transform.transform.Find("short cut6").Find("Key").GetComponent<Key>().LoadMapKey();
		component.LoadMapPassKey();
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x00012087 File Offset: 0x00010287
	public void openMonstarPlane()
	{
		this.monstarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.monstarPlane.SetActive(true);
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000120B9 File Offset: 0x000102B9
	public void openPlayerPlane()
	{
		this.initPlayerPlane();
		this.avatarPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.avatarPlane.SetActive(true);
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000120F1 File Offset: 0x000102F1
	public void openUseDanyaoPlan()
	{
		this.useDanyaoPlane.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.useDanyaoPlane.SetActive(true);
		this.initPreperDanYao();
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000B2AB8 File Offset: 0x000B0CB8
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

	// Token: 0x0600133A RID: 4922 RVA: 0x000B2C5C File Offset: 0x000B0E5C
	public void resetDanyaoPlan()
	{
		this.useDanyaoPlane.transform.Find("Scroll View/UIGrid");
		this.useDanyaoPlane.transform.Find("Scroll View").GetComponent<UIScrollView>();
		Tools.instance.getPlayer();
		Dictionary<int, item> items = jsonData.instance.gameObject.GetComponent<ItemDatebase>().items;
		this.inventory.setItemLeiXin5();
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x00012129 File Offset: 0x00010329
	public void closeMonstarPlane()
	{
		this.monstarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x0001214F File Offset: 0x0001034F
	public void closePlayerPlane()
	{
		this.avatarPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x00012175 File Offset: 0x00010375
	public void closeUseDanyaoPlan()
	{
		this.useDanyaoPlane.transform.localPosition = new Vector3(0f, 10000f, 0f);
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000B2CC8 File Offset: 0x000B0EC8
	public void startFight()
	{
		Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
		Tools.instance.loadOtherScenes("YSFight");
	}

	// Token: 0x04000EF5 RID: 3829
	public GameObject monstarPlane;

	// Token: 0x04000EF6 RID: 3830
	public GameObject avatarPlane;

	// Token: 0x04000EF7 RID: 3831
	public GameObject useDanyaoPlane;

	// Token: 0x04000EF8 RID: 3832
	public Inventory2 inventory;

	// Token: 0x04000EF9 RID: 3833
	private int MonstarID;

	// Token: 0x04000EFA RID: 3834
	private GameObject slot;
}
