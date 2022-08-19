using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class CreateAvatarMag : MonoBehaviour
{
	// Token: 0x06002061 RID: 8289 RVA: 0x000E3C7C File Offset: 0x000E1E7C
	private void Awake()
	{
		CreateAvatarMag.inst = this;
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x000B5E62 File Offset: 0x000B4062
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x000E3C84 File Offset: 0x000E1E84
	public void resetRandomFace()
	{
		this.isChangeSex = true;
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", this.faceUI.faceDatabase.ListType);
		this.player.randomAvatar(1);
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x000E3CE0 File Offset: 0x000E1EE0
	public void startGameClick(CreateAvatarMag.createAvatardelegate aa)
	{
		LevelSelectManager component = GameObject.Find("Main Menu/MainMenuCanvas").GetComponent<LevelSelectManager>();
		string text = component.getFirstName() + component.getLastName();
		if (text.Length > 10)
		{
			UIPopTip.Inst.Pop("名称字数过长", PopTipIconType.叹号);
			return;
		}
		if (!Tools.instance.CheckBadWord(text))
		{
			UIPopTip.Inst.Pop("名称不合法,请换个名称", PopTipIconType.叹号);
			return;
		}
		this.Eventdel = aa;
		base.gameObject.SetActive(true);
		this.showSetFace();
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x000E3D61 File Offset: 0x000E1F61
	public void resetAvatarFace()
	{
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", this.faceUI.faceDatabase.ListType);
		this.player.randomAvatar(1);
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x000E3D9D File Offset: 0x000E1F9D
	public void setMan()
	{
		this.faceUI.faceDatabase.ListType = 1;
		this.faceUI.resetList();
		this.isChangeSex = true;
		this.resetRandomFace();
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x000E3DC8 File Offset: 0x000E1FC8
	public void setWoman()
	{
		this.faceUI.faceDatabase.ListType = 2;
		this.faceUI.resetList();
		this.isChangeSex = true;
		this.resetRandomFace();
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x000E3DF3 File Offset: 0x000E1FF3
	public IEnumerator DengDaiToSet(GameObject obj)
	{
		yield return new WaitForSeconds(0.01f);
		obj.SetActive(false);
		yield break;
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x000E3E02 File Offset: 0x000E2002
	public void ChuangjueHome()
	{
		selectBox.instence.setChoice("是否直接退出创角", new EventDelegate(new EventDelegate.Callback(this.close)), null);
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x000E3E28 File Offset: 0x000E2028
	public void showSetFace()
	{
		this.tianfuUI.gameObject.SetActive(false);
		this.lingenUI.gameObject.SetActive(false);
		this.finalUI.gameObject.SetActive(false);
		this.faceUI.gameObject.SetActive(true);
		this.playerInfoText.SetActive(false);
		this.Sea2.SetActive(false);
		this.Sea1.SetActive(false);
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x000E3EA0 File Offset: 0x000E20A0
	public void showTianfuUI()
	{
		this.tianfuUI.gameObject.SetActive(true);
		this.lingenUI.gameObject.SetActive(false);
		this.finalUI.gameObject.SetActive(false);
		this.faceUI.gameObject.SetActive(false);
		this.playerInfoText.SetActive(true);
		this.tianfuUI.showPage(this.tianfuUI.nowPage);
		if (this.faceUI.faceDatabase.ListType == 1)
		{
			this.Sea1.SetActive(true);
			this.Sea2.SetActive(false);
			return;
		}
		this.Sea2.SetActive(true);
		this.Sea1.SetActive(false);
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x000E3F58 File Offset: 0x000E2158
	public void showLingenUI()
	{
		if (this.tianfuUI.TianFuDian < 0)
		{
			UIPopTip.Inst.Pop("剩余天赋点数不能为负数", PopTipIconType.叹号);
			return;
		}
		this.tianfuUI.gameObject.SetActive(false);
		this.lingenUI.gameObject.SetActive(true);
		this.finalUI.gameObject.SetActive(false);
		this.faceUI.gameObject.SetActive(false);
		this.lingenUI.resetLinGen();
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x000E3FD4 File Offset: 0x000E21D4
	public void TianFuNext()
	{
		if (this.tianfuUI.nowPage < this.tianfuUI.MaxPage)
		{
			if (this.tianfuUI.nowPage != 5)
			{
				bool flag = false;
				foreach (object obj in this.tianfuUI.grid.transform)
				{
					Transform transform = (Transform)obj;
					createAvatarChoice componentInChildren = transform.GetComponentInChildren<createAvatarChoice>();
					if (jsonData.instance.CreateAvatarJsonData.HasField(componentInChildren.id.ToString()) && (int)jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["fenLeiGuanLian"].n == this.tianfuUI.nowPage && transform.GetComponentInChildren<UIToggle>().value)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					UIPopTip.Inst.Pop("必须选择一个该类型天赋", PopTipIconType.叹号);
					return;
				}
			}
			this.tianfuUI.nowPage++;
			this.tianfuUI.showPage(this.tianfuUI.nowPage);
			return;
		}
		if (this.tianfuUI.TianFuDian < 0)
		{
			UIPopTip.Inst.Pop("剩余天赋点数不能为负数", PopTipIconType.叹号);
			return;
		}
		this.showFinalUI();
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x000E4130 File Offset: 0x000E2330
	public void TianFuLaset()
	{
		if (this.tianfuUI.nowPage > 1)
		{
			this.tianfuUI.nowPage--;
			this.tianfuUI.showPage(this.tianfuUI.nowPage);
			return;
		}
		this.showSetFace();
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x000E4170 File Offset: 0x000E2370
	public void showFinalUI()
	{
		this.tianfuUI.gameObject.SetActive(false);
		this.lingenUI.gameObject.SetActive(false);
		this.finalUI.gameObject.SetActive(true);
		this.faceUI.gameObject.SetActive(false);
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x000E41C1 File Offset: 0x000E23C1
	public void OK()
	{
		if (!this.isStart)
		{
			this.Eventdel();
			this.isStart = true;
		}
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x000E41E0 File Offset: 0x000E23E0
	private void Update()
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData["1"];
		this.zizhi.text = (this.setTextColor(this.tianfuUI.ZiZhi, jsonobject["ziZhi"].I) ?? "");
		this.linggeng.text = (Tools.instance.Code64ToString(jsonData.instance.LinGenZiZhiJsonData[this.tianfuUI.LinGengZiZhi.ToString()]["Title"].str) ?? "");
		this.shenshi.text = (this.setTextColor(this.tianfuUI.ShenShi, jsonobject["shengShi"].I) ?? "");
		this.dunsu.text = (this.setTextColor(this.tianfuUI.DunSu, jsonobject["dunSu"].I) ?? "");
		this.xinjin.text = "[FFFCFB]" + Tools.Code64(jsonData.instance.XinJinJsonData[this.getXinJinType().ToString()]["Text"].str) + "（[-]" + this.setTextColor(this.tianfuUI.XinJin, 0) + "[CDBDAC]）[-]";
		this.wuxing.text = (this.setTextColor(this.tianfuUI.WuXin, jsonobject["wuXin"].I) ?? "");
		this.showyuan.text = (this.setTextColor(this.tianfuUI.ShowYuan, jsonobject["shouYuan"].I) ?? "");
		this.qixie.text = (this.setTextColor(this.tianfuUI.HP_Max, jsonobject["HP"].I) ?? "");
		this.lingshi.text = (this.setTextColor(this.tianfuUI.Money, jsonobject["MoneyType"].I) ?? "");
		bool flag = false;
		int num = 0;
		int num2 = 0;
		foreach (createAvatarChoice createAvatarChoice in this.tianfuUI.getSelectChoice)
		{
			JSONObject jsonobject2 = jsonData.instance.CreateAvatarJsonData[createAvatarChoice.id.ToString()];
			int i = jsonobject2["fenLeiGuanLian"].I;
			int i2 = jsonobject2["feiYong"].I;
			if (i == 1)
			{
				flag = true;
			}
			if (i > 1 && i <= 3)
			{
				num2 += i2;
			}
			else if (i > 3)
			{
				num += i2;
			}
		}
		if (flag)
		{
			this.jinziup.gameObject.SetActive(true);
			this.jinziDown.gameObject.SetActive(true);
			int index = Mathf.Clamp(num2 / 5, 0, 2);
			int index2 = Mathf.Clamp(num / 4, 0, 2);
			Sprite sprite = this.jinziDownlist[index];
			Sprite sprite2 = this.jinziUplist[index2];
			this.jinziup.transform.localPosition = this.jinziUpPositon[index2];
			this.jinziDown.transform.localPosition = this.jinziDownPositon[index];
			this.jinziDown.sprite2D = sprite;
			this.jinziup.sprite2D = sprite2;
			this.jinziDown.width = (int)sprite.textureRect.width;
			this.jinziDown.height = (int)sprite.textureRect.height;
			this.jinziup.width = (int)sprite2.textureRect.width;
			this.jinziup.height = (int)sprite2.textureRect.height;
			return;
		}
		this.jinziup.gameObject.SetActive(false);
		this.jinziDown.gameObject.SetActive(false);
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x000E4620 File Offset: 0x000E2820
	public string setTextColor(int Num, int basenum)
	{
		if (Num - basenum > 0)
		{
			return "[CAF93F]" + Num + "[-]";
		}
		return "[FFFCFB]" + Num + "[-]";
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x000E4654 File Offset: 0x000E2854
	public int getXinJinType()
	{
		foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
		{
			if ((int)jsonobject["Max"].n > this.tianfuUI.XinJin)
			{
				return jsonobject["id"].I;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}

	// Token: 0x04001A4A RID: 6730
	public static CreateAvatarMag inst;

	// Token: 0x04001A4B RID: 6731
	public createTianfu tianfuUI;

	// Token: 0x04001A4C RID: 6732
	public CreatLinGen lingenUI;

	// Token: 0x04001A4D RID: 6733
	public CreateAvatarFinal finalUI;

	// Token: 0x04001A4E RID: 6734
	public Create_face faceUI;

	// Token: 0x04001A4F RID: 6735
	public UILabel zizhi;

	// Token: 0x04001A50 RID: 6736
	public UILabel linggeng;

	// Token: 0x04001A51 RID: 6737
	public UILabel gongfa;

	// Token: 0x04001A52 RID: 6738
	public UILabel shenshi;

	// Token: 0x04001A53 RID: 6739
	public UILabel dunsu;

	// Token: 0x04001A54 RID: 6740
	public UILabel xinjin;

	// Token: 0x04001A55 RID: 6741
	public UILabel wuxing;

	// Token: 0x04001A56 RID: 6742
	public UILabel showyuan;

	// Token: 0x04001A57 RID: 6743
	public UILabel qixie;

	// Token: 0x04001A58 RID: 6744
	public UILabel lingshi;

	// Token: 0x04001A59 RID: 6745
	public List<Vector3> playerPosition;

	// Token: 0x04001A5A RID: 6746
	public GameObject playerCavas;

	// Token: 0x04001A5B RID: 6747
	public GameObject playerInfoText;

	// Token: 0x04001A5C RID: 6748
	public PlayerSetRandomFace player;

	// Token: 0x04001A5D RID: 6749
	public List<Sprite> jinziUplist = new List<Sprite>();

	// Token: 0x04001A5E RID: 6750
	public List<Sprite> jinziDownlist = new List<Sprite>();

	// Token: 0x04001A5F RID: 6751
	public List<Vector3> jinziUpPositon = new List<Vector3>();

	// Token: 0x04001A60 RID: 6752
	public List<Vector3> jinziDownPositon = new List<Vector3>();

	// Token: 0x04001A61 RID: 6753
	public UI2DSprite jinziup;

	// Token: 0x04001A62 RID: 6754
	public UI2DSprite jinziDown;

	// Token: 0x04001A63 RID: 6755
	public GameObject Sea1;

	// Token: 0x04001A64 RID: 6756
	public GameObject Sea2;

	// Token: 0x04001A65 RID: 6757
	public bool isChangeSex;

	// Token: 0x04001A66 RID: 6758
	private bool isStart;

	// Token: 0x04001A67 RID: 6759
	public CreateAvatarMag.createAvatardelegate Eventdel;

	// Token: 0x04001A68 RID: 6760
	public int maxLevel;

	// Token: 0x04001A69 RID: 6761
	public Sprite LockImage;

	// Token: 0x02001383 RID: 4995
	// (Invoke) Token: 0x06007C30 RID: 31792
	public delegate void createAvatardelegate();
}
