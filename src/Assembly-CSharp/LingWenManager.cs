using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class LingWenManager : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> selectToggleImage = new List<Sprite>();

	[SerializeField]
	private List<Sprite> xianSprite = new List<Sprite>();

	[SerializeField]
	private List<Toggle> lingWenToggles = new List<Toggle>();

	private int selcetLinWenType = -1;

	[SerializeField]
	private List<Sprite> lingWenSprites = new List<Sprite>();

	[SerializeField]
	private Image lingWenImage;

	private int selectLinWenID = -1;

	public NomalSeceltLingWenCell nomalSeceltLingWen;

	public XuanWuSelectLingWenCell xuanWuSelectLingWenCell;

	public int getSelectLinWenType()
	{
		return selcetLinWenType;
	}

	public void setSelectLinWenID(int id)
	{
		selectLinWenID = id;
		LianQiTotalManager.inst.selectLingWenCiTiaoCallBack();
	}

	public int getSelectLingWenID()
	{
		return selectLinWenID;
	}

	public void init()
	{
		selcetLinWenType = 0;
		selectLinWenID = -1;
		initToggle();
		((Component)nomalSeceltLingWen).gameObject.SetActive(false);
		((Component)lingWenImage).gameObject.SetActive(false);
		checkIsCanSeclet();
	}

	private void initToggle()
	{
		for (int i = 0; i < 4; i++)
		{
			lingWenToggles[i].isOn = false;
		}
	}

	private void checkIsCanSeclet()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.checkHasStudyWuDaoSkillByID(2201))
		{
			((Selectable)lingWenToggles[2]).interactable = true;
			((Component)((Component)lingWenToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[0];
			((Component)((Component)lingWenToggles[2]).transform.GetChild(2)).gameObject.SetActive(true);
		}
		else
		{
			((Component)((Component)lingWenToggles[2]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[1];
			((Selectable)lingWenToggles[2]).interactable = false;
			((Component)((Component)lingWenToggles[2]).transform.GetChild(2)).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2202))
		{
			((Selectable)lingWenToggles[1]).interactable = true;
			((Component)((Component)lingWenToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[0];
			((Component)((Component)lingWenToggles[1]).transform.GetChild(2)).gameObject.SetActive(true);
		}
		else
		{
			((Component)((Component)lingWenToggles[1]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[1];
			((Selectable)lingWenToggles[1]).interactable = false;
			((Component)((Component)lingWenToggles[1]).transform.GetChild(2)).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2211) && LianQiTotalManager.inst.getCurSelectEquipType() == 1)
		{
			((Selectable)lingWenToggles[0]).interactable = true;
			((Component)((Component)lingWenToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[0];
			((Component)((Component)lingWenToggles[0]).transform.GetChild(2)).gameObject.SetActive(true);
		}
		else
		{
			((Component)((Component)lingWenToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[1];
			((Selectable)lingWenToggles[0]).interactable = false;
			((Component)((Component)lingWenToggles[0]).transform.GetChild(2)).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2212))
		{
			((Selectable)lingWenToggles[3]).interactable = true;
			((Component)((Component)lingWenToggles[3]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[0];
			((Component)((Component)lingWenToggles[3]).transform.GetChild(2)).gameObject.SetActive(true);
		}
		else
		{
			((Component)((Component)lingWenToggles[3]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[1];
			((Selectable)lingWenToggles[3]).interactable = false;
			((Component)((Component)lingWenToggles[3]).transform.GetChild(2)).gameObject.SetActive(false);
		}
	}

	private void checkSelectEquipType()
	{
		if (LianQiTotalManager.inst.getCurSelectEquipType() != 1)
		{
			((Component)((Component)lingWenToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[1];
			((Selectable)lingWenToggles[0]).interactable = false;
			((Component)((Component)lingWenToggles[0]).transform.GetChild(2)).gameObject.SetActive(false);
		}
		else
		{
			((Selectable)lingWenToggles[0]).interactable = true;
			((Component)((Component)lingWenToggles[0]).transform.GetChild(1)).GetComponent<Image>().sprite = selectToggleImage[0];
			((Component)((Component)lingWenToggles[0]).transform.GetChild(2)).gameObject.SetActive(true);
		}
	}

	public void selectLingWen(int index)
	{
		checkIsallUnSelect();
		switch (index)
		{
		case 0:
			if (lingWenToggles[0].isOn)
			{
				selcetLinWenType = 1;
				lingWenImage.sprite = lingWenSprites[0];
			}
			break;
		case 1:
			if (lingWenToggles[1].isOn)
			{
				selcetLinWenType = 2;
				lingWenImage.sprite = lingWenSprites[1];
			}
			break;
		case 2:
			if (lingWenToggles[2].isOn)
			{
				selcetLinWenType = 4;
				lingWenImage.sprite = lingWenSprites[2];
			}
			break;
		case 3:
			if (lingWenToggles[3].isOn)
			{
				selcetLinWenType = 3;
				lingWenImage.sprite = lingWenSprites[3];
			}
			else if (((Component)xuanWuSelectLingWenCell).gameObject.activeSelf)
			{
				((Component)xuanWuSelectLingWenCell).gameObject.SetActive(false);
			}
			break;
		}
		if (lingWenToggles[index].isOn)
		{
			((Component)((Component)lingWenToggles[index]).transform.GetChild(0)).GetComponent<Image>().sprite = xianSprite[1];
			setLingWenXiaoGuo();
		}
		else
		{
			((Component)((Component)lingWenToggles[index]).transform.GetChild(0)).GetComponent<Image>().sprite = xianSprite[0];
		}
	}

	public void setLingWenXiaoGuo()
	{
		if (selcetLinWenType == 3)
		{
			xuanWuSelectLingWenCell.showSelect();
		}
		else
		{
			nomalSeceltLingWen.showSelect();
		}
	}

	private void checkIsallUnSelect()
	{
		if (lingWenToggles[0].isOn || lingWenToggles[1].isOn || lingWenToggles[2].isOn)
		{
			((Component)lingWenImage).gameObject.SetActive(true);
			return;
		}
		((Component)nomalSeceltLingWen).gameObject.SetActive(false);
		if (lingWenToggles[3].isOn)
		{
			((Component)lingWenImage).gameObject.SetActive(true);
			return;
		}
		((Component)lingWenImage).gameObject.SetActive(false);
		setSelectLinWenID(-1);
		selcetLinWenType = -1;
	}
}
