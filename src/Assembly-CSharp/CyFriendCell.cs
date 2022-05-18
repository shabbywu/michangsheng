using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003DB RID: 987
public class CyFriendCell : MonoBehaviour
{
	// Token: 0x06001AFC RID: 6908 RVA: 0x000EED5C File Offset: 0x000ECF5C
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

	// Token: 0x06001AFD RID: 6909 RVA: 0x000EEFF4 File Offset: 0x000ED1F4
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

	// Token: 0x06001AFE RID: 6910 RVA: 0x000EF088 File Offset: 0x000ED288
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

	// Token: 0x06001AFF RID: 6911 RVA: 0x000EF1BC File Offset: 0x000ED3BC
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

	// Token: 0x040016A3 RID: 5795
	public Image bg;

	// Token: 0x040016A4 RID: 5796
	public Image tagImage;

	// Token: 0x040016A5 RID: 5797
	public Text npcName;

	// Token: 0x040016A6 RID: 5798
	public Text chengHao;

	// Token: 0x040016A7 RID: 5799
	public Image deathImage;

	// Token: 0x040016A8 RID: 5800
	public BtnCell tagBtnCell;

	// Token: 0x040016A9 RID: 5801
	public BtnCell btnCell;

	// Token: 0x040016AA RID: 5802
	public UINPCHeadFavor favor;

	// Token: 0x040016AB RID: 5803
	public int npcId = -1;

	// Token: 0x040016AC RID: 5804
	public bool isTag;

	// Token: 0x040016AD RID: 5805
	public bool isSelect;

	// Token: 0x040016AE RID: 5806
	public bool isDeath;

	// Token: 0x040016AF RID: 5807
	public bool IsFly;

	// Token: 0x040016B0 RID: 5808
	public UINPCData npcData;

	// Token: 0x040016B1 RID: 5809
	public GameObject redDian;
}
