using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAvatarMag : MonoBehaviour
{
	public delegate void createAvatardelegate();

	public static CreateAvatarMag inst;

	public createTianfu tianfuUI;

	public CreatLinGen lingenUI;

	public CreateAvatarFinal finalUI;

	public Create_face faceUI;

	public UILabel zizhi;

	public UILabel linggeng;

	public UILabel gongfa;

	public UILabel shenshi;

	public UILabel dunsu;

	public UILabel xinjin;

	public UILabel wuxing;

	public UILabel showyuan;

	public UILabel qixie;

	public UILabel lingshi;

	public List<Vector3> playerPosition;

	public GameObject playerCavas;

	public GameObject playerInfoText;

	public PlayerSetRandomFace player;

	public List<Sprite> jinziUplist = new List<Sprite>();

	public List<Sprite> jinziDownlist = new List<Sprite>();

	public List<Vector3> jinziUpPositon = new List<Vector3>();

	public List<Vector3> jinziDownPositon = new List<Vector3>();

	public UI2DSprite jinziup;

	public UI2DSprite jinziDown;

	public GameObject Sea1;

	public GameObject Sea2;

	public bool isChangeSex;

	private bool isStart;

	public createAvatardelegate Eventdel;

	public int maxLevel;

	public Sprite LockImage;

	private void Awake()
	{
		inst = this;
	}

	private void Start()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void resetRandomFace()
	{
		isChangeSex = true;
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", faceUI.faceDatabase.ListType);
		player.randomAvatar(1);
	}

	public void startGameClick(createAvatardelegate aa)
	{
		LevelSelectManager component = GameObject.Find("Main Menu/MainMenuCanvas").GetComponent<LevelSelectManager>();
		string text = component.getFirstName() + component.getLastName();
		if (text.Length > 10)
		{
			UIPopTip.Inst.Pop("名称字数过长");
			return;
		}
		if (!Tools.instance.CheckBadWord(text))
		{
			UIPopTip.Inst.Pop("名称不合法,请换个名称");
			return;
		}
		Eventdel = aa;
		((Component)this).gameObject.SetActive(true);
		showSetFace();
	}

	public void resetAvatarFace()
	{
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Sex", faceUI.faceDatabase.ListType);
		player.randomAvatar(1);
	}

	public void setMan()
	{
		faceUI.faceDatabase.ListType = 1;
		faceUI.resetList();
		isChangeSex = true;
		resetRandomFace();
	}

	public void setWoman()
	{
		faceUI.faceDatabase.ListType = 2;
		faceUI.resetList();
		isChangeSex = true;
		resetRandomFace();
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public IEnumerator DengDaiToSet(GameObject obj)
	{
		yield return (object)new WaitForSeconds(0.01f);
		obj.SetActive(false);
	}

	public void ChuangjueHome()
	{
		selectBox.instence.setChoice("是否直接退出创角", new EventDelegate(close), null);
	}

	public void showSetFace()
	{
		((Component)tianfuUI).gameObject.SetActive(false);
		((Component)lingenUI).gameObject.SetActive(false);
		((Component)finalUI).gameObject.SetActive(false);
		((Component)faceUI).gameObject.SetActive(true);
		playerInfoText.SetActive(false);
		Sea2.SetActive(false);
		Sea1.SetActive(false);
	}

	public void showTianfuUI()
	{
		((Component)tianfuUI).gameObject.SetActive(true);
		((Component)lingenUI).gameObject.SetActive(false);
		((Component)finalUI).gameObject.SetActive(false);
		((Component)faceUI).gameObject.SetActive(false);
		playerInfoText.SetActive(true);
		tianfuUI.showPage(tianfuUI.nowPage);
		if (faceUI.faceDatabase.ListType == 1)
		{
			Sea1.SetActive(true);
			Sea2.SetActive(false);
		}
		else
		{
			Sea2.SetActive(true);
			Sea1.SetActive(false);
		}
	}

	public void showLingenUI()
	{
		if (tianfuUI.TianFuDian < 0)
		{
			UIPopTip.Inst.Pop("剩余天赋点数不能为负数");
			return;
		}
		((Component)tianfuUI).gameObject.SetActive(false);
		((Component)lingenUI).gameObject.SetActive(true);
		((Component)finalUI).gameObject.SetActive(false);
		((Component)faceUI).gameObject.SetActive(false);
		lingenUI.resetLinGen();
	}

	public void TianFuNext()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		if (tianfuUI.nowPage < tianfuUI.MaxPage)
		{
			if (tianfuUI.nowPage != 5)
			{
				bool flag = false;
				foreach (Transform item in tianfuUI.grid.transform)
				{
					Transform val = item;
					createAvatarChoice componentInChildren = ((Component)val).GetComponentInChildren<createAvatarChoice>();
					if (jsonData.instance.CreateAvatarJsonData.HasField(componentInChildren.id.ToString()) && (int)jsonData.instance.CreateAvatarJsonData[componentInChildren.id.ToString()]["fenLeiGuanLian"].n == tianfuUI.nowPage && ((Component)val).GetComponentInChildren<UIToggle>().value)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					UIPopTip.Inst.Pop("必须选择一个该类型天赋");
					return;
				}
			}
			tianfuUI.nowPage++;
			tianfuUI.showPage(tianfuUI.nowPage);
		}
		else if (tianfuUI.TianFuDian < 0)
		{
			UIPopTip.Inst.Pop("剩余天赋点数不能为负数");
		}
		else
		{
			showFinalUI();
		}
	}

	public void TianFuLaset()
	{
		if (tianfuUI.nowPage > 1)
		{
			tianfuUI.nowPage--;
			tianfuUI.showPage(tianfuUI.nowPage);
		}
		else
		{
			showSetFace();
		}
	}

	public void showFinalUI()
	{
		((Component)tianfuUI).gameObject.SetActive(false);
		((Component)lingenUI).gameObject.SetActive(false);
		((Component)finalUI).gameObject.SetActive(true);
		((Component)faceUI).gameObject.SetActive(false);
	}

	public void OK()
	{
		if (!isStart)
		{
			Eventdel();
			isStart = true;
		}
	}

	private void Update()
	{
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		JSONObject jSONObject = jsonData.instance.AvatarJsonData["1"];
		zizhi.text = setTextColor(tianfuUI.ZiZhi, jSONObject["ziZhi"].I) ?? "";
		linggeng.text = Tools.instance.Code64ToString(jsonData.instance.LinGenZiZhiJsonData[tianfuUI.LinGengZiZhi.ToString()]["Title"].str) ?? "";
		shenshi.text = setTextColor(tianfuUI.ShenShi, jSONObject["shengShi"].I) ?? "";
		dunsu.text = setTextColor(tianfuUI.DunSu, jSONObject["dunSu"].I) ?? "";
		xinjin.text = string.Concat("[FFFCFB]" + Tools.Code64(jsonData.instance.XinJinJsonData[getXinJinType().ToString()]["Text"].str), "（[-]", setTextColor(tianfuUI.XinJin, 0), "[CDBDAC]）[-]");
		wuxing.text = setTextColor(tianfuUI.WuXin, jSONObject["wuXin"].I) ?? "";
		showyuan.text = setTextColor(tianfuUI.ShowYuan, jSONObject["shouYuan"].I) ?? "";
		qixie.text = setTextColor(tianfuUI.HP_Max, jSONObject["HP"].I) ?? "";
		lingshi.text = setTextColor(tianfuUI.Money, jSONObject["MoneyType"].I) ?? "";
		bool flag = false;
		int num = 0;
		int num2 = 0;
		foreach (createAvatarChoice item in tianfuUI.getSelectChoice)
		{
			JSONObject jSONObject2 = jsonData.instance.CreateAvatarJsonData[item.id.ToString()];
			int i = jSONObject2["fenLeiGuanLian"].I;
			int i2 = jSONObject2["feiYong"].I;
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
			((Component)jinziup).gameObject.SetActive(true);
			((Component)jinziDown).gameObject.SetActive(true);
			int index = Mathf.Clamp(num2 / 5, 0, 2);
			int index2 = Mathf.Clamp(num / 4, 0, 2);
			Sprite val = jinziDownlist[index];
			Sprite val2 = jinziUplist[index2];
			((Component)jinziup).transform.localPosition = jinziUpPositon[index2];
			((Component)jinziDown).transform.localPosition = jinziDownPositon[index];
			jinziDown.sprite2D = val;
			jinziup.sprite2D = val2;
			UI2DSprite uI2DSprite = jinziDown;
			Rect textureRect = val.textureRect;
			uI2DSprite.width = (int)((Rect)(ref textureRect)).width;
			UI2DSprite uI2DSprite2 = jinziDown;
			textureRect = val.textureRect;
			uI2DSprite2.height = (int)((Rect)(ref textureRect)).height;
			UI2DSprite uI2DSprite3 = jinziup;
			textureRect = val2.textureRect;
			uI2DSprite3.width = (int)((Rect)(ref textureRect)).width;
			UI2DSprite uI2DSprite4 = jinziup;
			textureRect = val2.textureRect;
			uI2DSprite4.height = (int)((Rect)(ref textureRect)).height;
		}
		else
		{
			((Component)jinziup).gameObject.SetActive(false);
			((Component)jinziDown).gameObject.SetActive(false);
		}
	}

	public string setTextColor(int Num, int basenum)
	{
		if (Num - basenum > 0)
		{
			return "[CAF93F]" + Num + "[-]";
		}
		return "[FFFCFB]" + Num + "[-]";
	}

	public int getXinJinType()
	{
		foreach (JSONObject item in jsonData.instance.XinJinJsonData.list)
		{
			if ((int)item["Max"].n > tianfuUI.XinJin)
			{
				return item["id"].I;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}
}
