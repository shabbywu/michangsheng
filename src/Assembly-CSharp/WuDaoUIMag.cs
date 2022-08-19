using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000510 RID: 1296
public class WuDaoUIMag : MonoBehaviour
{
	// Token: 0x060029AC RID: 10668 RVA: 0x0013E6B3 File Offset: 0x0013C8B3
	private void Awake()
	{
		WuDaoUIMag.inst = this;
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x0013E6BB File Offset: 0x0013C8BB
	private void OnDestroy()
	{
		WuDaoUIMag.inst = null;
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x0013E6C3 File Offset: 0x0013C8C3
	private void Start()
	{
		this.init();
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x0013E6CB File Offset: 0x0013C8CB
	public void open()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		base.gameObject.SetActive(true);
		this.init();
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x0013E6EA File Offset: 0x0013C8EA
	public void close()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x0013E704 File Offset: 0x0013C904
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

	// Token: 0x060029B2 RID: 10674 RVA: 0x0013E918 File Offset: 0x0013CB18
	private IEnumerator selectDefault(GameObject _obj, wudaoTypeCell wudaocell)
	{
		yield return new WaitForSeconds(0.01f);
		_obj.GetComponentInChildren<Toggle>().isOn = true;
		yield break;
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x0013E928 File Offset: 0x0013CB28
	public static string getWuDaoTypeName(int type)
	{
		Avatar player = Tools.instance.getPlayer();
		return Tools.Code64(jsonData.instance.WuDaoJinJieJson[player.wuDaoMag.getWuDaoLevelByType(type).ToString()]["Text"].str);
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x0013E978 File Offset: 0x0013CB78
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

	// Token: 0x060029B5 RID: 10677 RVA: 0x0013EA1C File Offset: 0x0013CC1C
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

	// Token: 0x060029B6 RID: 10678 RVA: 0x00004095 File Offset: 0x00002295
	public void setShengyuDianshu()
	{
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x0013EBC8 File Offset: 0x0013CDC8
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

	// Token: 0x060029B8 RID: 10680 RVA: 0x0013ECA8 File Offset: 0x0013CEA8
	public void upWuDaoDate()
	{
		this.WuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
		this.ShiXu.text = Tools.instance.getPlayer().LingGuang.list.Count.ToString();
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x040025FF RID: 9727
	public GameObject TypeCell;

	// Token: 0x04002600 RID: 9728
	public static WuDaoUIMag inst;

	// Token: 0x04002601 RID: 9729
	public List<Sprite> TuBiaoList = new List<Sprite>();

	// Token: 0x04002602 RID: 9730
	public List<Sprite> BGList = new List<Sprite>();

	// Token: 0x04002603 RID: 9731
	public List<UI2DSprite> BGs = new List<UI2DSprite>();

	// Token: 0x04002604 RID: 9732
	public Text WuDaoDian;

	// Token: 0x04002605 RID: 9733
	public Text ShiXu;

	// Token: 0x04002606 RID: 9734
	[SerializeField]
	private GameObject wuDaoEmpty;

	// Token: 0x04002607 RID: 9735
	public List<Sprite> Sprites_Lv = new List<Sprite>();

	// Token: 0x04002608 RID: 9736
	public Slider slider;

	// Token: 0x04002609 RID: 9737
	public Text shengyudianshu;

	// Token: 0x0400260A RID: 9738
	public List<GameObject> WuDaoCententList;

	// Token: 0x0400260B RID: 9739
	public List<Text> labelText;

	// Token: 0x0400260C RID: 9740
	public WuDaoCellTooltip wuDaoCellTooltip;

	// Token: 0x0400260D RID: 9741
	public WuDaoHelp wuDaoHelp1;

	// Token: 0x0400260E RID: 9742
	public List<Sprite> IconBgSprite;

	// Token: 0x0400260F RID: 9743
	public int NowType = -1;
}
