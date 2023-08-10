using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CyFriendCell : MonoBehaviour
{
	public Image bg;

	public Image tagImage;

	public Text npcName;

	public Text chengHao;

	public Image deathImage;

	public BtnCell tagBtnCell;

	public BtnCell btnCell;

	public UINPCHeadFavor favor;

	public int npcId = -1;

	public bool isTag;

	public bool isSelect;

	public bool isDeath;

	public bool IsFly;

	public UINPCData npcData;

	public GameObject redDian;

	public void Init(int npcId)
	{
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		isSelect = false;
		this.npcId = npcId;
		isDeath = NpcJieSuanManager.inst.IsDeath(npcId);
		if (!isDeath)
		{
			IsFly = NpcJieSuanManager.inst.IsFly(npcId);
		}
		if (isDeath)
		{
			((Component)favor).gameObject.SetActive(false);
			((Component)deathImage).gameObject.SetActive(true);
			btnCell = ((Component)this).GetComponent<BtnCell>();
			if (npcId < 20000)
			{
				npcName.text = AvatarJsonData.DataDict[npcId].Name;
				chengHao.text = AvatarJsonData.DataDict[npcId].Title;
			}
			else
			{
				JSONObject jSONObject = NpcJieSuanManager.inst.npcDeath.npcDeathJson[npcId.ToString()];
				npcName.text = jSONObject["deathName"].Str;
				chengHao.text = jSONObject["deathChengHao"].Str;
			}
		}
		else
		{
			npcData = new UINPCData(npcId);
			try
			{
				npcData.RefreshData();
			}
			catch (Exception)
			{
				Debug.LogError((object)npcId);
			}
			if (IsFly)
			{
				((Component)favor).gameObject.SetActive(false);
				((Component)deathImage).gameObject.SetActive(true);
			}
			else
			{
				favor.SetFavor(npcData.Favor);
			}
			this.npcId = npcId;
			btnCell = ((Component)this).GetComponent<BtnCell>();
			npcName.text = npcData.Name;
			chengHao.text = npcData.Title;
			isTag = Tools.instance.getPlayer().emailDateMag.TagNpcList.Contains(npcId);
		}
		if (isTag)
		{
			bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[2];
		}
		else
		{
			bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[0];
		}
		((Component)tagImage).gameObject.SetActive(false);
		btnCell.mouseUp.AddListener(new UnityAction(Click));
		tagBtnCell.mouseUp.AddListener(new UnityAction(ClickTag));
		updateState();
		((Component)this).gameObject.SetActive(true);
	}

	public void Click()
	{
		if (!isSelect)
		{
			isSelect = true;
			if (isSelect)
			{
				CyUIMag.inst.npcList.ClickCallBack();
				CyUIMag.inst.npcList.curSelectFriend = this;
				CyUIMag.inst.No.SetActive(false);
				updateState();
				CyUIMag.inst.cyEmail.cySendBtn.Hide();
				CyUIMag.inst.cyEmail.Init(npcId);
				redDian.SetActive(false);
			}
		}
	}

	public void updateState()
	{
		if (isSelect)
		{
			if (!isTag)
			{
				tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[0];
				bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[1];
				if (!isDeath)
				{
					((Component)tagImage).gameObject.SetActive(true);
				}
			}
			else
			{
				tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[1];
				bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[3];
				if (!isDeath)
				{
					((Component)tagImage).gameObject.SetActive(true);
				}
			}
		}
		else
		{
			((Component)tagImage).gameObject.SetActive(false);
			if (isTag)
			{
				bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[2];
			}
			else
			{
				bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[0];
			}
		}
	}

	public void ClickTag()
	{
		if (NpcJieSuanManager.inst.IsDeath(npcId))
		{
			UIPopTip.Inst.Pop("人死如灯灭,无法标记", PopTipIconType.传音符);
			return;
		}
		List<int> tagNpcList = Tools.instance.getPlayer().emailDateMag.TagNpcList;
		isTag = !isTag;
		npcData.IsTag = isTag;
		NpcJieSuanManager.inst.npcSetField.SetTag(npcId, isTag);
		if (!isTag)
		{
			tagNpcList.Remove(npcId);
			tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[0];
			bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[1];
			return;
		}
		if (!tagNpcList.Contains(npcId))
		{
			tagNpcList.Add(npcId);
		}
		tagImage.sprite = CyUIMag.inst.npcList.tagSpriteList[1];
		bg.sprite = CyUIMag.inst.npcList.npcCellSpriteList[3];
	}
}
