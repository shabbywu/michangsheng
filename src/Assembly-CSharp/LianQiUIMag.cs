using System;
using System.Collections.Generic;
using GUIPackage;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003E1 RID: 993
public class LianQiUIMag : MonoBehaviour
{
	// Token: 0x0600201A RID: 8218 RVA: 0x000E205B File Offset: 0x000E025B
	private void Awake()
	{
		LianQiUIMag.instance = this;
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x000E2064 File Offset: 0x000E0264
	private void Start()
	{
		for (int i = 0; i < (int)this.inventory.count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x000E209C File Offset: 0x000E029C
	public void openLianQiPanel()
	{
		LianQiUIMag.instance = this;
		this.OpenSelectEquip();
		this.selectCaiLiaoPageManager.init();
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x000E20B5 File Offset: 0x000E02B5
	private void OpenSelectEquip()
	{
		this.selectEquipPage.SetActive(true);
		this.selectEquipType(1);
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x000E20CC File Offset: 0x000E02CC
	public void selectEquipType(int type = 1)
	{
		if (this.equipToggles[0].isOn)
		{
			this.equipToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[1];
		}
		else
		{
			this.equipToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[0];
		}
		if (this.equipToggles[1].isOn)
		{
			this.equipToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[3];
		}
		else
		{
			this.equipToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[2];
		}
		if (this.equipToggles[2].isOn)
		{
			this.equipToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[5];
		}
		else
		{
			this.equipToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.equipTogglesNameIcon[4];
		}
		if (this.equipToggles[type - 1].isOn)
		{
			this.qiController.setSelectZhuangBeiType(type);
			this.InitEquipType(type);
		}
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x000E224E File Offset: 0x000E044E
	public void closeSelectEquip()
	{
		this.selectEquipPage.SetActive(false);
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x000E225C File Offset: 0x000E045C
	private void InitEquipType(int type)
	{
		Tools.ClearObj(this.lianQiEquipCell.transform);
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.LianQiEquipType)
		{
			if ((int)keyValuePair.Value["zhonglei"] == type)
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.lianQiEquipCell, this.lianQiEquipCell.transform.parent);
				LianQiEquipCell component = gameObject.GetComponent<LianQiEquipCell>();
				component.setEquipName(keyValuePair.Value["desc"].ToString());
				component.setEquipID((int)keyValuePair.Value["ItemID"]);
				gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(component.Onclick));
			}
		}
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x000E234C File Offset: 0x000E054C
	public void SetCurSelectEquipMuBanID(int itemID)
	{
		this.qiController.setCurSelectEquipMuBanID(itemID);
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x000E235A File Offset: 0x000E055A
	public int getCurSelectEquipMuBanID()
	{
		return this.qiController.getCurSelectEquipMuBanID();
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x000E2367 File Offset: 0x000E0567
	public void setCurJinDu(int jindu)
	{
		this.qiController.setCurJinDu(jindu);
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x000E2375 File Offset: 0x000E0575
	public int getCurJinDu()
	{
		return this.qiController.getCurJinDu();
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x000E2382 File Offset: 0x000E0582
	public void StartLianQiBtn()
	{
		if (this.qiController.WhetherSuccess())
		{
			this.SuccessCell();
		}
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000E2398 File Offset: 0x000E0598
	public void exitClick()
	{
		switch (this.getCurJinDu())
		{
		case -1:
		case 0:
			this.closeSelectEquip();
			base.gameObject.SetActive(false);
			return;
		case 1:
			this.OpenSelectEquip();
			this.setCurJinDu(0);
			return;
		case 2:
			this.setCurJinDu(1);
			return;
		default:
			return;
		}
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x000E23ED File Offset: 0x000E05ED
	public void SuccessCell()
	{
		this.OpenSetName();
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x000E23F5 File Offset: 0x000E05F5
	public void OpenSetName()
	{
		this.SetNameObjtct.SetActive(true);
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x000E2403 File Offset: 0x000E0603
	public void SuccessSetNameBtn()
	{
		this.qiController.ItemName = this.inputField.text;
		this.qiController.LiQiSuccess();
		this.SetNameObjtct.SetActive(false);
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000E2432 File Offset: 0x000E0632
	public void SelectEquipXiFenType(int type)
	{
		this.qiController.ZhuangBeiIndexID = type;
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000E2440 File Offset: 0x000E0640
	public void SetWuWeiSlider()
	{
		this.uISliders[0].value = (float)this.qiController.QingHe / 500f;
		this.uISliders[1].value = (float)this.qiController.CaoKong / 500f;
		this.uISliders[2].value = (float)this.qiController.LingXing / 500f;
		this.uISliders[3].value = (float)this.qiController.JianGu / 500f;
		this.uISliders[4].value = (float)this.qiController.RenXing / 500f;
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x000E24FC File Offset: 0x000E06FC
	private void Update()
	{
		this.SetWuWeiSlider();
	}

	// Token: 0x04001A0C RID: 6668
	public List<UISlider> uISliders;

	// Token: 0x04001A0D RID: 6669
	public GameObject EquipCellBase;

	// Token: 0x04001A0E RID: 6670
	public List<GameObject> EquipCellGridlist;

	// Token: 0x04001A0F RID: 6671
	public LianQiController qiController;

	// Token: 0x04001A10 RID: 6672
	public InputField inputField;

	// Token: 0x04001A11 RID: 6673
	public GameObject SetNameObjtct;

	// Token: 0x04001A12 RID: 6674
	public GameObject FailPanel;

	// Token: 0x04001A13 RID: 6675
	public Inventory2 inventory;

	// Token: 0x04001A14 RID: 6676
	public GameObject ChoiceEuipPanel;

	// Token: 0x04001A15 RID: 6677
	public static LianQiUIMag instance;

	// Token: 0x04001A16 RID: 6678
	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	// Token: 0x04001A17 RID: 6679
	[SerializeField]
	private GameObject lianQiEquipCell;

	// Token: 0x04001A18 RID: 6680
	[SerializeField]
	private List<Sprite> weaponSprites = new List<Sprite>();

	// Token: 0x04001A19 RID: 6681
	[SerializeField]
	private List<Sprite> armorSprites = new List<Sprite>();

	// Token: 0x04001A1A RID: 6682
	[SerializeField]
	private List<Sprite> jewelrySprites = new List<Sprite>();

	// Token: 0x04001A1B RID: 6683
	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	// Token: 0x04001A1C RID: 6684
	[SerializeField]
	private GameObject selectEquipPage;

	// Token: 0x04001A1D RID: 6685
	public SelectCaiLiaoPageManager selectCaiLiaoPageManager;
}
