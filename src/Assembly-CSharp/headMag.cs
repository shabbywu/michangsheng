using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class headMag : MonoBehaviour
{
	public GameObject headUI;

	public UILabel time;

	[SerializeField]
	private List<Sprite> LevelSprites = new List<Sprite>();

	[SerializeField]
	private UI2DSprite LevelIcon;

	private Avatar avatar;

	[HideInInspector]
	public bool isOut;

	[HideInInspector]
	public bool isOut2;

	[SerializeField]
	public Button BtnChuanYingFu;

	public GameObject ChuanYingHongDian;

	public GameObject TuJianHongDian;

	public GameObject TieJianHongDian;

	[SerializeField]
	public Button BtnTuJian;

	private void Start()
	{
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			UIHeadPanel.Inst.ScaleObj.SetActive(true);
			UIHeadPanel.Inst.CheckHongDian(checkChuanYin: true);
		}
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			UIMiniTaskPanel.Inst.oldHead = this;
			UIMiniTaskPanel.Inst.ScaleObj.SetActive(true);
		}
		OldStart();
	}

	public void OldStart()
	{
		avatar = Tools.instance.getPlayer();
	}

	public void setHead()
	{
		try
		{
			string screenName = Tools.getScreenName();
			if (!jsonData.instance.FuBenInfoJsonData.HasField(screenName) || avatar.fubenContorl[screenName].ResidueTimeDay > 0 || isOut2)
			{
				return;
			}
			GameObject val = GameObject.Find("OutFuBenTalk");
			if ((Object)(object)val != (Object)null)
			{
				Flowchart component = val.GetComponent<Flowchart>();
				Block block = component.FindBlock("OutFuBen");
				component.ExecuteBlock(block, 0, delegate
				{
					AllMapManage.instance.backToLastInFuBenScene.Try();
				});
			}
			else
			{
				AllMapManage.instance.backToLastInFuBenScene.Try();
			}
			isOut2 = true;
		}
		catch (Exception)
		{
		}
	}

	private void Update()
	{
		if ((Object)(object)UIHeadPanel.Inst != (Object)null)
		{
			UIHeadPanel.Inst.RefreshUI();
		}
		if ((Object)(object)UIMiniTaskPanel.Inst != (Object)null)
		{
			UIMiniTaskPanel.Inst.RefreshUI();
		}
		OldUpdate();
	}

	public void OldUpdate()
	{
		try
		{
			setHead();
		}
		catch (Exception)
		{
		}
	}
}
