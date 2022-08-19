using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002A2 RID: 674
public class CyFriendCell : MonoBehaviour
{
	// Token: 0x0600180A RID: 6154 RVA: 0x000A7B20 File Offset: 0x000A5D20
	public void Init(int npcId)
	{
		this.isSelect = false;
		this.npcId = npcId;
		this.isDeath = NpcJieSuanManager.inst.IsDeath(npcId);
		if (!this.isDeath)
		{
			this.IsFly = NpcJieSuanManager.inst.IsFly(npcId);
		}
		if (this.isDeath)
		{
			this.favor.gameObject.SetActive(false);
			this.deathImage.gameObject.SetActive(true);
			this.btnCell = base.GetComponent<BtnCell>();
			if (npcId < 20000)
			{
				this.npcName.text = AvatarJsonData.DataDict[npcId].Name;
				this.chengHao.text = AvatarJsonData.DataDict[npcId].Title;
			}
			else
			{
				JSONObject jsonobject = NpcJieSuanManager.inst.npcDeath.npcDeathJson[npcId.ToString()];
				this.npcName.text = jsonobject["deathName"].Str;
				this.chengHao.text = jsonobject["deathChengHao"].Str;
			}
		}
		else
		{
			this.npcData = new UINPCData(npcId, false);
			try
			{
				this.npcData.RefreshData();
			}
			catch (Exception)
			{
				Debug.LogError(npcId);
			}
			if (this.IsFly)
			{
				this.favor.gameObject.SetActive(false);
				this.deathImage.gameObject.SetActive(true);
			}
			else
			{
				this.favor.SetFavor(this.npcData.Favor);
			}
			this.npcId = npcId;
			this.btnCell = base.GetComponent<BtnCell>();
			this.npcName.text = this.npcData.Name;
			this.chengHao.text = this.npcData.Title;
			this.isTag = Tools.instance.getPlayer().emailDateMag.TagNpcList.Contains(npcId);
		}
		if (this.isTag)
		{
			this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[2];
		}
		else
		{
			this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[0];
		}
		this.tagImage.gameObject.SetActive(false);
		this.btnCell.mouseUp.AddListener(new UnityAction(this.Click));
		this.tagBtnCell.mouseUp.AddListener(new UnityAction(this.ClickTag));
		this.updateState();
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x000A7DB8 File Offset: 0x000A5FB8
	public void Click()
	{
		if (!this.isSelect)
		{
			this.isSelect = true;
			if (this.isSelect)
			{
				CyUIMag.inst.npcList.ClickCallBack();
				CyUIMag.inst.npcList.curSelectFriend = this;
				CyUIMag.inst.No.SetActive(false);
				this.updateState();
				CyUIMag.inst.cyEmail.cySendBtn.Hide();
				CyUIMag.inst.cyEmail.Init(this.npcId);
				this.redDian.SetActive(false);
			}
			return;
		}
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000A7E4C File Offset: 0x000A604C
	public void updateState()
	{
		if (this.isSelect)
		{
			if (!this.isTag)
			{
				this.tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[0];
				this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[1];
				if (!this.isDeath)
				{
					this.tagImage.gameObject.SetActive(true);
					return;
				}
			}
			else
			{
				this.tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[1];
				this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[3];
				if (!this.isDeath)
				{
					this.tagImage.gameObject.SetActive(true);
					return;
				}
			}
		}
		else
		{
			this.tagImage.gameObject.SetActive(false);
			if (this.isTag)
			{
				this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[2];
				return;
			}
			this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[0];
		}
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000A7F80 File Offset: 0x000A6180
	public void ClickTag()
	{
		if (NpcJieSuanManager.inst.IsDeath(this.npcId))
		{
			UIPopTip.Inst.Pop("人死如灯灭,无法标记", PopTipIconType.传音符);
			return;
		}
		List<int> tagNpcList = Tools.instance.getPlayer().emailDateMag.TagNpcList;
		this.isTag = !this.isTag;
		this.npcData.IsTag = this.isTag;
		NpcJieSuanManager.inst.npcSetField.SetTag(this.npcId, this.isTag);
		if (!this.isTag)
		{
			tagNpcList.Remove(this.npcId);
			this.tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[0];
			this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[1];
			return;
		}
		if (!tagNpcList.Contains(this.npcId))
		{
			tagNpcList.Add(this.npcId);
		}
		this.tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[1];
		this.bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[3];
	}

	// Token: 0x04001307 RID: 4871
	public Image bg;

	// Token: 0x04001308 RID: 4872
	public Image tagImage;

	// Token: 0x04001309 RID: 4873
	public Text npcName;

	// Token: 0x0400130A RID: 4874
	public Text chengHao;

	// Token: 0x0400130B RID: 4875
	public Image deathImage;

	// Token: 0x0400130C RID: 4876
	public BtnCell tagBtnCell;

	// Token: 0x0400130D RID: 4877
	public BtnCell btnCell;

	// Token: 0x0400130E RID: 4878
	public UINPCHeadFavor favor;

	// Token: 0x0400130F RID: 4879
	public int npcId = -1;

	// Token: 0x04001310 RID: 4880
	public bool isTag;

	// Token: 0x04001311 RID: 4881
	public bool isSelect;

	// Token: 0x04001312 RID: 4882
	public bool isDeath;

	// Token: 0x04001313 RID: 4883
	public bool IsFly;

	// Token: 0x04001314 RID: 4884
	public UINPCData npcData;

	// Token: 0x04001315 RID: 4885
	public GameObject redDian;
}
