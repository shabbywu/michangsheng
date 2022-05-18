using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A2 RID: 1954
public class WuDaoUIMag : MonoBehaviour
{
	// Token: 0x060031B3 RID: 12723 RVA: 0x000245E6 File Offset: 0x000227E6
	private void Awake()
	{
		WuDaoUIMag.inst = this;
	}

	// Token: 0x060031B4 RID: 12724 RVA: 0x000245EE File Offset: 0x000227EE
	private void OnDestroy()
	{
		WuDaoUIMag.inst = null;
	}

	// Token: 0x060031B5 RID: 12725 RVA: 0x000245F6 File Offset: 0x000227F6
	private void Start()
	{
		this.init();
	}

	// Token: 0x060031B6 RID: 12726 RVA: 0x000245FE File Offset: 0x000227FE
	public void open()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		base.gameObject.SetActive(true);
		this.init();
	}

	// Token: 0x060031B7 RID: 12727 RVA: 0x0002461D File Offset: 0x0002281D
	public void close()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060031B8 RID: 12728 RVA: 0x0018B80C File Offset: 0x00189A0C
	public void init()
	{
		foreach (object obj in this.TypeCell.transform.parent)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		int num = 0;
		bool flag = false;
		GameObject obj2 = null;
		wudaoTypeCell wudaocell = null;
		foreach (JSONObject jsonobject in jsonData.instance.WuDaoAllTypeJson.list)
		{
			GameObject gameObject = Tools.InstantiateGameObject(this.TypeCell, this.TypeCell.transform.parent);
			wudaoTypeCell component = gameObject.GetComponent<wudaoTypeCell>();
			component.TitleName.text = Tools.Code64(jsonobject["name"].str);
			if (!flag)
			{
				obj2 = gameObject;
				wudaocell = component;
				flag = true;
			}
			component.Type = jsonobject["id"].I;
			if (num % 2 == 1)
			{
				component.ImageBG.gameObject.SetActive(false);
			}
			component.typeIcon.sprite = this.TuBiaoList[num];
			num++;
		}
		Tools.InstantiateGameObject(this.wuDaoEmpty, this.wuDaoEmpty.transform.parent);
		if (this.NowType != -1)
		{
			Avatar player = Tools.instance.getPlayer();
			this.wuDaoHelp1.text = string.Concat(new object[]
			{
				"[24a5d6]大道感悟：[-]通过领悟功法、神通能够提升你对大道的感悟。\n[24a5d6]触类旁通：[-]任意一系悟道达到融会贯通时，获得悟道点*1\n\n[ffe34b]当前进度:[-]",
				player.wuDaoMag.getWuDaoEx(this.NowType),
				"/",
				player.wuDaoMag.getNowTypeExMax(this.NowType)
			});
		}
		this.setShengyuDianshu();
		this.ResetCellButton();
		base.StartCoroutine(this.selectDefault(obj2, wudaocell));
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x00024636 File Offset: 0x00022836
	private IEnumerator selectDefault(GameObject _obj, wudaoTypeCell wudaocell)
	{
		yield return new WaitForSeconds(0.01f);
		_obj.GetComponentInChildren<Toggle>().isOn = true;
		yield break;
	}

	// Token: 0x060031BA RID: 12730 RVA: 0x0018BA20 File Offset: 0x00189C20
	public static string getWuDaoTypeName(int type)
	{
		Avatar player = Tools.instance.getPlayer();
		return Tools.Code64(jsonData.instance.WuDaoJinJieJson[player.wuDaoMag.getWuDaoLevelByType(type).ToString()]["Text"].str);
	}

	// Token: 0x060031BB RID: 12731 RVA: 0x0018BA70 File Offset: 0x00189C70
	public void ClearCenten()
	{
		foreach (GameObject gameObject in this.WuDaoCententList)
		{
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					Object.Destroy(transform.gameObject);
				}
			}
		}
	}

	// Token: 0x060031BC RID: 12732 RVA: 0x0018BB14 File Offset: 0x00189D14
	public void ResetCellButton()
	{
		foreach (GameObject gameObject in this.WuDaoCententList)
		{
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					wuDaoUICell component = transform.gameObject.GetComponent<wuDaoUICell>();
					if (component.IsStudy())
					{
						component.bg.GetComponent<UIEffect>().effectFactor = 0f;
						component.icon.GetComponent<UIEffect>().effectFactor = 0f;
						component.bg.gameObject.SetActive(false);
						component.castNum.transform.parent.gameObject.SetActive(false);
					}
					else
					{
						component.icon.GetComponent<UIEffect>().effectFactor = 0.85f;
						if (!component.CanStudyWuDao())
						{
							component.bg.gameObject.SetActive(false);
							component.castNum.transform.parent.gameObject.SetActive(false);
						}
						else
						{
							component.bg.gameObject.SetActive(true);
							component.castNum.transform.parent.gameObject.SetActive(true);
						}
					}
				}
			}
		}
		this.setShengyuDianshu();
	}

	// Token: 0x060031BD RID: 12733 RVA: 0x000042DD File Offset: 0x000024DD
	public void setShengyuDianshu()
	{
	}

	// Token: 0x060031BE RID: 12734 RVA: 0x0018BCC0 File Offset: 0x00189EC0
	public void ResetEx(int wudaoType)
	{
		this.slider.value = Tools.instance.getPlayer().wuDaoMag.getWuDaoExPercent(wudaoType) / 100f;
		int num = 0;
		foreach (Text text in this.labelText)
		{
			if (num + 1 > Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(wudaoType))
			{
				this.labelText[num].color = new Color(188f, 180f, 175f);
			}
			else
			{
				this.labelText[num].color = new Color(205f, 189f, 172f);
			}
			num++;
		}
	}

	// Token: 0x060031BF RID: 12735 RVA: 0x0018BDA0 File Offset: 0x00189FA0
	public void upWuDaoDate()
	{
		this.WuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
		this.ShiXu.text = Tools.instance.getPlayer().LingGuang.list.Count.ToString();
	}

	// Token: 0x060031C0 RID: 12736 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002DE8 RID: 11752
	public GameObject TypeCell;

	// Token: 0x04002DE9 RID: 11753
	public static WuDaoUIMag inst;

	// Token: 0x04002DEA RID: 11754
	public List<Sprite> TuBiaoList = new List<Sprite>();

	// Token: 0x04002DEB RID: 11755
	public List<Sprite> BGList = new List<Sprite>();

	// Token: 0x04002DEC RID: 11756
	public List<UI2DSprite> BGs = new List<UI2DSprite>();

	// Token: 0x04002DED RID: 11757
	public Text WuDaoDian;

	// Token: 0x04002DEE RID: 11758
	public Text ShiXu;

	// Token: 0x04002DEF RID: 11759
	[SerializeField]
	private GameObject wuDaoEmpty;

	// Token: 0x04002DF0 RID: 11760
	public List<Sprite> Sprites_Lv = new List<Sprite>();

	// Token: 0x04002DF1 RID: 11761
	public Slider slider;

	// Token: 0x04002DF2 RID: 11762
	public Text shengyudianshu;

	// Token: 0x04002DF3 RID: 11763
	public List<GameObject> WuDaoCententList;

	// Token: 0x04002DF4 RID: 11764
	public List<Text> labelText;

	// Token: 0x04002DF5 RID: 11765
	public WuDaoCellTooltip wuDaoCellTooltip;

	// Token: 0x04002DF6 RID: 11766
	public WuDaoHelp wuDaoHelp1;

	// Token: 0x04002DF7 RID: 11767
	public List<Sprite> IconBgSprite;

	// Token: 0x04002DF8 RID: 11768
	public int NowType = -1;
}
