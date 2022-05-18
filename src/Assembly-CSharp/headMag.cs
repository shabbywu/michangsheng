using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000270 RID: 624
public class headMag : MonoBehaviour
{
	// Token: 0x06001349 RID: 4937 RVA: 0x000B2F48 File Offset: 0x000B1148
	private void Start()
	{
		if (UIHeadPanel.Inst != null)
		{
			UIHeadPanel.Inst.ScaleObj.SetActive(true);
			UIHeadPanel.Inst.CheckHongDian(true);
		}
		if (UIMiniTaskPanel.Inst != null)
		{
			UIMiniTaskPanel.Inst.oldHead = this;
			UIMiniTaskPanel.Inst.ScaleObj.SetActive(true);
		}
		this.OldStart();
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x0001224A File Offset: 0x0001044A
	public void OldStart()
	{
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x000B2FAC File Offset: 0x000B11AC
	public void setHead()
	{
		try
		{
			string screenName = Tools.getScreenName();
			if (jsonData.instance.FuBenInfoJsonData.HasField(screenName) && this.avatar.fubenContorl[screenName].ResidueTimeDay <= 0)
			{
				if (!this.isOut2)
				{
					GameObject gameObject = GameObject.Find("OutFuBenTalk");
					if (gameObject != null)
					{
						Flowchart component = gameObject.GetComponent<Flowchart>();
						Block block = component.FindBlock("OutFuBen");
						component.ExecuteBlock(block, 0, delegate()
						{
							AllMapManage.instance.backToLastInFuBenScene.Try();
						});
					}
					else
					{
						AllMapManage.instance.backToLastInFuBenScene.Try();
					}
					this.isOut2 = true;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x0001225C File Offset: 0x0001045C
	private void Update()
	{
		if (UIHeadPanel.Inst != null)
		{
			UIHeadPanel.Inst.RefreshUI();
		}
		if (UIMiniTaskPanel.Inst != null)
		{
			UIMiniTaskPanel.Inst.RefreshUI();
		}
		this.OldUpdate();
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000B3074 File Offset: 0x000B1274
	public void OldUpdate()
	{
		try
		{
			this.setHead();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000EFD RID: 3837
	public GameObject headUI;

	// Token: 0x04000EFE RID: 3838
	public UILabel time;

	// Token: 0x04000EFF RID: 3839
	[SerializeField]
	private List<Sprite> LevelSprites = new List<Sprite>();

	// Token: 0x04000F00 RID: 3840
	[SerializeField]
	private UI2DSprite LevelIcon;

	// Token: 0x04000F01 RID: 3841
	private Avatar avatar;

	// Token: 0x04000F02 RID: 3842
	[HideInInspector]
	public bool isOut;

	// Token: 0x04000F03 RID: 3843
	[HideInInspector]
	public bool isOut2;

	// Token: 0x04000F04 RID: 3844
	[SerializeField]
	public Button BtnChuanYingFu;

	// Token: 0x04000F05 RID: 3845
	public GameObject ChuanYingHongDian;

	// Token: 0x04000F06 RID: 3846
	public GameObject TuJianHongDian;

	// Token: 0x04000F07 RID: 3847
	public GameObject TieJianHongDian;

	// Token: 0x04000F08 RID: 3848
	[SerializeField]
	public Button BtnTuJian;
}
