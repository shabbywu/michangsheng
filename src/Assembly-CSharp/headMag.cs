using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018A RID: 394
public class headMag : MonoBehaviour
{
	// Token: 0x060010E2 RID: 4322 RVA: 0x000646C0 File Offset: 0x000628C0
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

	// Token: 0x060010E3 RID: 4323 RVA: 0x00064723 File Offset: 0x00062923
	public void OldStart()
	{
		this.avatar = Tools.instance.getPlayer();
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x00064738 File Offset: 0x00062938
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

	// Token: 0x060010E5 RID: 4325 RVA: 0x00064800 File Offset: 0x00062A00
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

	// Token: 0x060010E6 RID: 4326 RVA: 0x00064838 File Offset: 0x00062A38
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

	// Token: 0x04000C18 RID: 3096
	public GameObject headUI;

	// Token: 0x04000C19 RID: 3097
	public UILabel time;

	// Token: 0x04000C1A RID: 3098
	[SerializeField]
	private List<Sprite> LevelSprites = new List<Sprite>();

	// Token: 0x04000C1B RID: 3099
	[SerializeField]
	private UI2DSprite LevelIcon;

	// Token: 0x04000C1C RID: 3100
	private Avatar avatar;

	// Token: 0x04000C1D RID: 3101
	[HideInInspector]
	public bool isOut;

	// Token: 0x04000C1E RID: 3102
	[HideInInspector]
	public bool isOut2;

	// Token: 0x04000C1F RID: 3103
	[SerializeField]
	public Button BtnChuanYingFu;

	// Token: 0x04000C20 RID: 3104
	public GameObject ChuanYingHongDian;

	// Token: 0x04000C21 RID: 3105
	public GameObject TuJianHongDian;

	// Token: 0x04000C22 RID: 3106
	public GameObject TieJianHongDian;

	// Token: 0x04000C23 RID: 3107
	[SerializeField]
	public Button BtnTuJian;
}
