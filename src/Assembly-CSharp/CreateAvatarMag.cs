using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058A RID: 1418
public class CreateAvatarMag : MonoBehaviour
{
	// Token: 0x060023EA RID: 9194 RVA: 0x0001CFAE File Offset: 0x0001B1AE
	private void Awake()
	{
		CreateAvatarMag.inst = this;
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x00017C2D File Offset: 0x00015E2D
	private void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x00126124 File Offset: 0x00124324
	public void resetRandomFace()
	{
		this.isChangeSex = true;
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", this.faceUI.faceDatabase.ListType);
		this.player.randomAvatar(1);
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x00126180 File Offset: 0x00124380
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

	// Token: 0x060023EE RID: 9198 RVA: 0x0001CFB6 File Offset: 0x0001B1B6
	public void resetAvatarFace()
	{
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", this.faceUI.faceDatabase.ListType);
		this.player.randomAvatar(1);
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x0001CFF2 File Offset: 0x0001B1F2
	public void setMan()
	{
		this.faceUI.faceDatabase.ListType = 1;
		this.faceUI.resetList();
		this.isChangeSex = true;
		this.resetRandomFace();
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x0001D01D File Offset: 0x0001B21D
	public void setWoman()
	{
		this.faceUI.faceDatabase.ListType = 2;
		this.faceUI.resetList();
		this.isChangeSex = true;
		this.resetRandomFace();
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x0001D048 File Offset: 0x0001B248
	public IEnumerator DengDaiToSet(GameObject obj)
	{
		yield return new WaitForSeconds(0.01f);
		obj.SetActive(false);
		yield break;
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x0001D057 File Offset: 0x0001B257
	public void ChuangjueHome()
	{
		selectBox.instence.setChoice("是否直接退出创角", new EventDelegate(new EventDelegate.Callback(this.close)), null);
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x00126204 File Offset: 0x00124404
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

	// Token: 0x060023F5 RID: 9205 RVA: 0x0012627C File Offset: 0x0012447C
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

	// Token: 0x060023F6 RID: 9206 RVA: 0x00126334 File Offset: 0x00124534
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

	// Token: 0x060023F7 RID: 9207 RVA: 0x001263B0 File Offset: 0x001245B0
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

	// Token: 0x060023F8 RID: 9208 RVA: 0x0001D07A File Offset: 0x0001B27A
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

	// Token: 0x060023F9 RID: 9209 RVA: 0x0012650C File Offset: 0x0012470C
	public void showFinalUI()
	{
		this.tianfuUI.gameObject.SetActive(false);
		this.lingenUI.gameObject.SetActive(false);
		this.finalUI.gameObject.SetActive(true);
		this.faceUI.gameObject.SetActive(false);
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x0001D0BA File Offset: 0x0001B2BA
	public void OK()
	{
		if (!this.isStart)
		{
			this.Eventdel();
			this.isStart = true;
		}
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x00126560 File Offset: 0x00124760
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

	// Token: 0x060023FC RID: 9212 RVA: 0x0001D0D6 File Offset: 0x0001B2D6
	public string setTextColor(int Num, int basenum)
	{
		if (Num - basenum > 0)
		{
			return "[CAF93F]" + Num + "[-]";
		}
		return "[FFFCFB]" + Num + "[-]";
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x001269A0 File Offset: 0x00124BA0
	public int getXinJinType()
	{
		foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
		{
			if ((int)jsonobject["Max"].n > this.tianfuUI.XinJin)
			{
				return (int)jsonobject["id"].n;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}

	// Token: 0x04001EEF RID: 7919
	public static CreateAvatarMag inst;

	// Token: 0x04001EF0 RID: 7920
	public createTianfu tianfuUI;

	// Token: 0x04001EF1 RID: 7921
	public CreatLinGen lingenUI;

	// Token: 0x04001EF2 RID: 7922
	public CreateAvatarFinal finalUI;

	// Token: 0x04001EF3 RID: 7923
	public Create_face faceUI;

	// Token: 0x04001EF4 RID: 7924
	public UILabel zizhi;

	// Token: 0x04001EF5 RID: 7925
	public UILabel linggeng;

	// Token: 0x04001EF6 RID: 7926
	public UILabel gongfa;

	// Token: 0x04001EF7 RID: 7927
	public UILabel shenshi;

	// Token: 0x04001EF8 RID: 7928
	public UILabel dunsu;

	// Token: 0x04001EF9 RID: 7929
	public UILabel xinjin;

	// Token: 0x04001EFA RID: 7930
	public UILabel wuxing;

	// Token: 0x04001EFB RID: 7931
	public UILabel showyuan;

	// Token: 0x04001EFC RID: 7932
	public UILabel qixie;

	// Token: 0x04001EFD RID: 7933
	public UILabel lingshi;

	// Token: 0x04001EFE RID: 7934
	public List<Vector3> playerPosition;

	// Token: 0x04001EFF RID: 7935
	public GameObject playerCavas;

	// Token: 0x04001F00 RID: 7936
	public GameObject playerInfoText;

	// Token: 0x04001F01 RID: 7937
	public PlayerSetRandomFace player;

	// Token: 0x04001F02 RID: 7938
	public List<Sprite> jinziUplist = new List<Sprite>();

	// Token: 0x04001F03 RID: 7939
	public List<Sprite> jinziDownlist = new List<Sprite>();

	// Token: 0x04001F04 RID: 7940
	public List<Vector3> jinziUpPositon = new List<Vector3>();

	// Token: 0x04001F05 RID: 7941
	public List<Vector3> jinziDownPositon = new List<Vector3>();

	// Token: 0x04001F06 RID: 7942
	public UI2DSprite jinziup;

	// Token: 0x04001F07 RID: 7943
	public UI2DSprite jinziDown;

	// Token: 0x04001F08 RID: 7944
	public GameObject Sea1;

	// Token: 0x04001F09 RID: 7945
	public GameObject Sea2;

	// Token: 0x04001F0A RID: 7946
	public bool isChangeSex;

	// Token: 0x04001F0B RID: 7947
	private bool isStart;

	// Token: 0x04001F0C RID: 7948
	public CreateAvatarMag.createAvatardelegate Eventdel;

	// Token: 0x04001F0D RID: 7949
	public int maxLevel;

	// Token: 0x04001F0E RID: 7950
	public Sprite LockImage;

	// Token: 0x0200058B RID: 1419
	// (Invoke) Token: 0x06002400 RID: 9216
	public delegate void createAvatardelegate();
}
