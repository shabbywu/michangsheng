using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000457 RID: 1111
public class LingWenManager : MonoBehaviour
{
	// Token: 0x06001DBB RID: 7611 RVA: 0x00018BBF File Offset: 0x00016DBF
	public int getSelectLinWenType()
	{
		return this.selcetLinWenType;
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x00018BC7 File Offset: 0x00016DC7
	public void setSelectLinWenID(int id)
	{
		this.selectLinWenID = id;
		LianQiTotalManager.inst.selectLingWenCiTiaoCallBack();
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x00018BDA File Offset: 0x00016DDA
	public int getSelectLingWenID()
	{
		return this.selectLinWenID;
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00018BE2 File Offset: 0x00016DE2
	public void init()
	{
		this.selcetLinWenType = 0;
		this.selectLinWenID = -1;
		this.initToggle();
		this.nomalSeceltLingWen.gameObject.SetActive(false);
		this.lingWenImage.gameObject.SetActive(false);
		this.checkIsCanSeclet();
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x001033D8 File Offset: 0x001015D8
	private void initToggle()
	{
		for (int i = 0; i < 4; i++)
		{
			this.lingWenToggles[i].isOn = false;
		}
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00103404 File Offset: 0x00101604
	private void checkIsCanSeclet()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.checkHasStudyWuDaoSkillByID(2201))
		{
			this.lingWenToggles[2].interactable = true;
			this.lingWenToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[0];
			this.lingWenToggles[2].transform.GetChild(2).gameObject.SetActive(true);
		}
		else
		{
			this.lingWenToggles[2].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[1];
			this.lingWenToggles[2].interactable = false;
			this.lingWenToggles[2].transform.GetChild(2).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2202))
		{
			this.lingWenToggles[1].interactable = true;
			this.lingWenToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[0];
			this.lingWenToggles[1].transform.GetChild(2).gameObject.SetActive(true);
		}
		else
		{
			this.lingWenToggles[1].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[1];
			this.lingWenToggles[1].interactable = false;
			this.lingWenToggles[1].transform.GetChild(2).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2211) && LianQiTotalManager.inst.getCurSelectEquipType() == 1)
		{
			this.lingWenToggles[0].interactable = true;
			this.lingWenToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[0];
			this.lingWenToggles[0].transform.GetChild(2).gameObject.SetActive(true);
		}
		else
		{
			this.lingWenToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[1];
			this.lingWenToggles[0].interactable = false;
			this.lingWenToggles[0].transform.GetChild(2).gameObject.SetActive(false);
		}
		if (player.checkHasStudyWuDaoSkillByID(2212))
		{
			this.lingWenToggles[3].interactable = true;
			this.lingWenToggles[3].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[0];
			this.lingWenToggles[3].transform.GetChild(2).gameObject.SetActive(true);
			return;
		}
		this.lingWenToggles[3].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[1];
		this.lingWenToggles[3].interactable = false;
		this.lingWenToggles[3].transform.GetChild(2).gameObject.SetActive(false);
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x0010376C File Offset: 0x0010196C
	private void checkSelectEquipType()
	{
		if (LianQiTotalManager.inst.getCurSelectEquipType() != 1)
		{
			this.lingWenToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[1];
			this.lingWenToggles[0].interactable = false;
			this.lingWenToggles[0].transform.GetChild(2).gameObject.SetActive(false);
			return;
		}
		this.lingWenToggles[0].interactable = true;
		this.lingWenToggles[0].transform.GetChild(1).GetComponent<Image>().sprite = this.selectToggleImage[0];
		this.lingWenToggles[0].transform.GetChild(2).gameObject.SetActive(true);
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x0010384C File Offset: 0x00101A4C
	public void selectLingWen(int index)
	{
		this.checkIsallUnSelect();
		switch (index)
		{
		case 0:
			if (this.lingWenToggles[0].isOn)
			{
				this.selcetLinWenType = 1;
				this.lingWenImage.sprite = this.lingWenSprites[0];
			}
			break;
		case 1:
			if (this.lingWenToggles[1].isOn)
			{
				this.selcetLinWenType = 2;
				this.lingWenImage.sprite = this.lingWenSprites[1];
			}
			break;
		case 2:
			if (this.lingWenToggles[2].isOn)
			{
				this.selcetLinWenType = 4;
				this.lingWenImage.sprite = this.lingWenSprites[2];
			}
			break;
		case 3:
			if (this.lingWenToggles[3].isOn)
			{
				this.selcetLinWenType = 3;
				this.lingWenImage.sprite = this.lingWenSprites[3];
			}
			else if (this.xuanWuSelectLingWenCell.gameObject.activeSelf)
			{
				this.xuanWuSelectLingWenCell.gameObject.SetActive(false);
			}
			break;
		}
		if (this.lingWenToggles[index].isOn)
		{
			this.lingWenToggles[index].transform.GetChild(0).GetComponent<Image>().sprite = this.xianSprite[1];
			this.setLingWenXiaoGuo();
			return;
		}
		this.lingWenToggles[index].transform.GetChild(0).GetComponent<Image>().sprite = this.xianSprite[0];
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x00018C20 File Offset: 0x00016E20
	public void setLingWenXiaoGuo()
	{
		if (this.selcetLinWenType == 3)
		{
			this.xuanWuSelectLingWenCell.showSelect();
			return;
		}
		this.nomalSeceltLingWen.showSelect();
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x001039EC File Offset: 0x00101BEC
	private void checkIsallUnSelect()
	{
		if (this.lingWenToggles[0].isOn || this.lingWenToggles[1].isOn || this.lingWenToggles[2].isOn)
		{
			this.lingWenImage.gameObject.SetActive(true);
			return;
		}
		this.nomalSeceltLingWen.gameObject.SetActive(false);
		if (this.lingWenToggles[3].isOn)
		{
			this.lingWenImage.gameObject.SetActive(true);
			return;
		}
		this.lingWenImage.gameObject.SetActive(false);
		this.setSelectLinWenID(-1);
		this.selcetLinWenType = -1;
	}

	// Token: 0x04001965 RID: 6501
	[SerializeField]
	private List<Sprite> selectToggleImage = new List<Sprite>();

	// Token: 0x04001966 RID: 6502
	[SerializeField]
	private List<Sprite> xianSprite = new List<Sprite>();

	// Token: 0x04001967 RID: 6503
	[SerializeField]
	private List<Toggle> lingWenToggles = new List<Toggle>();

	// Token: 0x04001968 RID: 6504
	private int selcetLinWenType = -1;

	// Token: 0x04001969 RID: 6505
	[SerializeField]
	private List<Sprite> lingWenSprites = new List<Sprite>();

	// Token: 0x0400196A RID: 6506
	[SerializeField]
	private Image lingWenImage;

	// Token: 0x0400196B RID: 6507
	private int selectLinWenID = -1;

	// Token: 0x0400196C RID: 6508
	public NomalSeceltLingWenCell nomalSeceltLingWen;

	// Token: 0x0400196D RID: 6509
	public XuanWuSelectLingWenCell xuanWuSelectLingWenCell;
}
