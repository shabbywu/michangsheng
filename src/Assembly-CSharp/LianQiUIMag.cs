using System.Collections.Generic;
using GUIPackage;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LianQiUIMag : MonoBehaviour
{
	public List<UISlider> uISliders;

	public GameObject EquipCellBase;

	public List<GameObject> EquipCellGridlist;

	public LianQiController qiController;

	public InputField inputField;

	public GameObject SetNameObjtct;

	public GameObject FailPanel;

	public Inventory2 inventory;

	public GameObject ChoiceEuipPanel;

	public static LianQiUIMag instance;

	[SerializeField]
	private List<Toggle> equipToggles = new List<Toggle>();

	[SerializeField]
	private GameObject lianQiEquipCell;

	[SerializeField]
	private List<Sprite> weaponSprites = new List<Sprite>();

	[SerializeField]
	private List<Sprite> armorSprites = new List<Sprite>();

	[SerializeField]
	private List<Sprite> jewelrySprites = new List<Sprite>();

	[SerializeField]
	private List<Sprite> equipTogglesNameIcon = new List<Sprite>();

	[SerializeField]
	private GameObject selectEquipPage;

	public SelectCaiLiaoPageManager selectCaiLiaoPageManager;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		for (int i = 0; i < (int)inventory.count; i++)
		{
			inventory.inventory.Add(new item());
		}
	}

	public void openLianQiPanel()
	{
		instance = this;
		OpenSelectEquip();
		selectCaiLiaoPageManager.init();
	}

	private void OpenSelectEquip()
	{
		selectEquipPage.SetActive(true);
		selectEquipType();
	}

	public void selectEquipType(int type = 1)
	{
		if (equipToggles[0].isOn)
		{
			((Component)((Component)equipToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[1];
		}
		else
		{
			((Component)((Component)equipToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[0];
		}
		if (equipToggles[1].isOn)
		{
			((Component)((Component)equipToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[3];
		}
		else
		{
			((Component)((Component)equipToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[2];
		}
		if (equipToggles[2].isOn)
		{
			((Component)((Component)equipToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[5];
		}
		else
		{
			((Component)((Component)equipToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = equipTogglesNameIcon[4];
		}
		if (equipToggles[type - 1].isOn)
		{
			qiController.setSelectZhuangBeiType(type);
			InitEquipType(type);
		}
	}

	public void closeSelectEquip()
	{
		selectEquipPage.SetActive(false);
	}

	private void InitEquipType(int type)
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		Tools.ClearObj(lianQiEquipCell.transform);
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.LianQiEquipType)
		{
			if ((int)item.Value[(object)"zhonglei"] == type)
			{
				GameObject obj = Tools.InstantiateGameObject(lianQiEquipCell, lianQiEquipCell.transform.parent);
				LianQiEquipCell component = obj.GetComponent<LianQiEquipCell>();
				component.setEquipName(((object)item.Value[(object)"desc"]).ToString());
				component.setEquipID((int)item.Value[(object)"ItemID"]);
				((UnityEvent)obj.GetComponent<Button>().onClick).AddListener(new UnityAction(component.Onclick));
			}
		}
	}

	public void SetCurSelectEquipMuBanID(int itemID)
	{
		qiController.setCurSelectEquipMuBanID(itemID);
	}

	public int getCurSelectEquipMuBanID()
	{
		return qiController.getCurSelectEquipMuBanID();
	}

	public void setCurJinDu(int jindu)
	{
		qiController.setCurJinDu(jindu);
	}

	public int getCurJinDu()
	{
		return qiController.getCurJinDu();
	}

	public void StartLianQiBtn()
	{
		if (qiController.WhetherSuccess())
		{
			SuccessCell();
		}
	}

	public void exitClick()
	{
		switch (getCurJinDu())
		{
		case -1:
		case 0:
			closeSelectEquip();
			((Component)this).gameObject.SetActive(false);
			break;
		case 1:
			OpenSelectEquip();
			setCurJinDu(0);
			break;
		case 2:
			setCurJinDu(1);
			break;
		}
	}

	public void SuccessCell()
	{
		OpenSetName();
	}

	public void OpenSetName()
	{
		SetNameObjtct.SetActive(true);
	}

	public void SuccessSetNameBtn()
	{
		qiController.ItemName = inputField.text;
		qiController.LiQiSuccess();
		SetNameObjtct.SetActive(false);
	}

	public void SelectEquipXiFenType(int type)
	{
		qiController.ZhuangBeiIndexID = type;
	}

	public void SetWuWeiSlider()
	{
		uISliders[0].value = (float)qiController.QingHe / 500f;
		uISliders[1].value = (float)qiController.CaoKong / 500f;
		uISliders[2].value = (float)qiController.LingXing / 500f;
		uISliders[3].value = (float)qiController.JianGu / 500f;
		uISliders[4].value = (float)qiController.RenXing / 500f;
	}

	private void Update()
	{
		SetWuWeiSlider();
	}
}
