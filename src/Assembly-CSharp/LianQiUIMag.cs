using System;
using System.Collections.Generic;
using GUIPackage;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200057C RID: 1404
public class LianQiUIMag : MonoBehaviour
{
	// Token: 0x0600239A RID: 9114 RVA: 0x0001CBDB File Offset: 0x0001ADDB
	private void Awake()
	{
		LianQiUIMag.instance = this;
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x001249C8 File Offset: 0x00122BC8
	private void Start()
	{
		for (int i = 0; i < (int)this.inventory.count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x0001CBE3 File Offset: 0x0001ADE3
	public void openLianQiPanel()
	{
		LianQiUIMag.instance = this;
		this.OpenSelectEquip();
		this.selectCaiLiaoPageManager.init();
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x0001CBFC File Offset: 0x0001ADFC
	private void OpenSelectEquip()
	{
		this.selectEquipPage.SetActive(true);
		this.selectEquipType(1);
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x00124A00 File Offset: 0x00122C00
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

	// Token: 0x0600239F RID: 9119 RVA: 0x0001CC11 File Offset: 0x0001AE11
	public void closeSelectEquip()
	{
		this.selectEquipPage.SetActive(false);
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x00124B84 File Offset: 0x00122D84
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

	// Token: 0x060023A1 RID: 9121 RVA: 0x0001CC1F File Offset: 0x0001AE1F
	public void SetCurSelectEquipMuBanID(int itemID)
	{
		this.qiController.setCurSelectEquipMuBanID(itemID);
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x0001CC2D File Offset: 0x0001AE2D
	public int getCurSelectEquipMuBanID()
	{
		return this.qiController.getCurSelectEquipMuBanID();
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x0001CC3A File Offset: 0x0001AE3A
	public void setCurJinDu(int jindu)
	{
		this.qiController.setCurJinDu(jindu);
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x0001CC48 File Offset: 0x0001AE48
	public int getCurJinDu()
	{
		return this.qiController.getCurJinDu();
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x0001CC55 File Offset: 0x0001AE55
	public void StartLianQiBtn()
	{
		if (this.qiController.WhetherSuccess())
		{
			this.SuccessCell();
		}
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x00124C74 File Offset: 0x00122E74
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

	// Token: 0x060023A7 RID: 9127 RVA: 0x0001CC6A File Offset: 0x0001AE6A
	public void SuccessCell()
	{
		this.OpenSetName();
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x0001CC72 File Offset: 0x0001AE72
	public void OpenSetName()
	{
		this.SetNameObjtct.SetActive(true);
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x0001CC80 File Offset: 0x0001AE80
	public void SuccessSetNameBtn()
	{
		this.qiController.ItemName = this.inputField.text;
		this.qiController.LiQiSuccess();
		this.SetNameObjtct.SetActive(false);
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x0001CCAF File Offset: 0x0001AEAF
	public void SelectEquipXiFenType(int type)
	{
		this.qiController.ZhuangBeiIndexID = type;
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x00124CCC File Offset: 0x00122ECC
	public void SetWuWeiSlider()
	{
		this.uISliders[0].value = (float)this.qiController.QingHe / 500f;
		this.uISliders[1].value = (float)this.qiController.CaoKong / 500f;
		this.uISliders[2].value = (float)this.qiController.LingXing / 500f;
		this.uISliders[3].value = (float)this.qiController.JianGu / 500f;
		this.uISliders[4].value = (float)this.qiController.RenXing / 500f;
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x0001CCBD File Offset: 0x0001AEBD
	private void Update()
	{
		this.SetWuWeiSlider();
	}

	// Token: 0x04001E9E RID: 7838
	public List<UISlider> uISliders;

	// Token: 0x04001E9F RID: 7839
	public GameObject EquipCellBase;

	// Token: 0x04001EA0 RID: 7840
	public List<GameObject> EquipCellGridlist;

	// Token: 0x04001EA1 RID: 7841
	public LianQiController qiController;

	// Token: 0x04001EA2 RID: 7842
	public InputField inputField;

	// Token: 0x04001EA3 RID: 7843
	public GameObject SetNameObjtct;

	// Token: 0x04001EA4 RID: 7844
	public GameObject FailPanel;

	// Token: 0x04001EA5 RID: 7845
	public Inventory2 inventory;

	// Token: 0x04001EA6 RID: 7846
	public GameObject ChoiceEuipPanel;

	// Token: 0x04001EA7 RID: 7847
	public static LianQiUIMag instance;

	// Token: 0x04001EA8 RID: 7848
	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	// Token: 0x04001EA9 RID: 7849
	[SerializeField]
	private GameObject lianQiEquipCell;

	// Token: 0x04001EAA RID: 7850
	[SerializeField]
	private List<Sprite> weaponSprites = new List<Sprite>();

	// Token: 0x04001EAB RID: 7851
	[SerializeField]
	private List<Sprite> armorSprites = new List<Sprite>();

	// Token: 0x04001EAC RID: 7852
	[SerializeField]
	private List<Sprite> jewelrySprites = new List<Sprite>();

	// Token: 0x04001EAD RID: 7853
	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	// Token: 0x04001EAE RID: 7854
	[SerializeField]
	private GameObject selectEquipPage;

	// Token: 0x04001EAF RID: 7855
	public SelectCaiLiaoPageManager selectCaiLiaoPageManager;
}
