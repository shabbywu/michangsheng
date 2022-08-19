using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FA RID: 762
public class LingWenManager : MonoBehaviour
{
	// Token: 0x06001A95 RID: 6805 RVA: 0x000BD3D9 File Offset: 0x000BB5D9
	public int getSelectLinWenType()
	{
		return this.selcetLinWenType;
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x000BD3E1 File Offset: 0x000BB5E1
	public void setSelectLinWenID(int id)
	{
		this.selectLinWenID = id;
		LianQiTotalManager.inst.selectLingWenCiTiaoCallBack();
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x000BD3F4 File Offset: 0x000BB5F4
	public int getSelectLingWenID()
	{
		return this.selectLinWenID;
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x000BD3FC File Offset: 0x000BB5FC
	public void init()
	{
		this.selcetLinWenType = 0;
		this.selectLinWenID = -1;
		this.initToggle();
		this.nomalSeceltLingWen.gameObject.SetActive(false);
		this.lingWenImage.gameObject.SetActive(false);
		this.checkIsCanSeclet();
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x000BD43C File Offset: 0x000BB63C
	private void initToggle()
	{
		for (int i = 0; i < 4; i++)
		{
			this.lingWenToggles[i].isOn = false;
		}
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x000BD468 File Offset: 0x000BB668
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

	// Token: 0x06001A9B RID: 6811 RVA: 0x000BD7D0 File Offset: 0x000BB9D0
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

	// Token: 0x06001A9C RID: 6812 RVA: 0x000BD8B0 File Offset: 0x000BBAB0
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

	// Token: 0x06001A9D RID: 6813 RVA: 0x000BDA4D File Offset: 0x000BBC4D
	public void setLingWenXiaoGuo()
	{
		if (this.selcetLinWenType == 3)
		{
			this.xuanWuSelectLingWenCell.showSelect();
			return;
		}
		this.nomalSeceltLingWen.showSelect();
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x000BDA70 File Offset: 0x000BBC70
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

	// Token: 0x04001558 RID: 5464
	[SerializeField]
	private List<Sprite> selectToggleImage = new List<Sprite>();

	// Token: 0x04001559 RID: 5465
	[SerializeField]
	private List<Sprite> xianSprite = new List<Sprite>();

	// Token: 0x0400155A RID: 5466
	[SerializeField]
	private List<Toggle> lingWenToggles = new List<Toggle>();

	// Token: 0x0400155B RID: 5467
	private int selcetLinWenType = -1;

	// Token: 0x0400155C RID: 5468
	[SerializeField]
	private List<Sprite> lingWenSprites = new List<Sprite>();

	// Token: 0x0400155D RID: 5469
	[SerializeField]
	private Image lingWenImage;

	// Token: 0x0400155E RID: 5470
	private int selectLinWenID = -1;

	// Token: 0x0400155F RID: 5471
	public NomalSeceltLingWenCell nomalSeceltLingWen;

	// Token: 0x04001560 RID: 5472
	public XuanWuSelectLingWenCell xuanWuSelectLingWenCell;
}
