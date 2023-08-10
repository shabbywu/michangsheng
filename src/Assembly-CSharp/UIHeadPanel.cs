using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

public class UIHeadPanel : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public static UIHeadPanel Inst;

	public GameObject ScaleObj;

	public Text NameText;

	public Text HPText;

	public Text ExpText;

	public Slider HPSlider;

	public Slider ExpSlider;

	public PlayerSetRandomFace Face;

	public Image LevelImage;

	public GameObject TieJianRedPoint;

	public GameObject ChuanYinFuPoint;

	public GameObject TuJianPoint;

	public GameObject TianJieObj;

	public Text TianJieTime;

	public GameObject CunDangObj;

	public CanvasGroup CunDangAlpha;

	[HideInInspector]
	public bool IsMouseInUI;

	private bool saving;

	public List<Sprite> LevelSprite;

	private void Awake()
	{
		Inst = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if ((Object)(object)PanelMamager.inst != (Object)null && (Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
		{
			CheckHongDian();
		}
		else
		{
			ScaleObj.SetActive(false);
		}
	}

	public void CheckHongDian(bool checkChuanYin = false)
	{
		Avatar player = Tools.instance.getPlayer();
		if (player != null)
		{
			RefreshTieJianRedPoint();
			TuJianPoint.SetActive(!TuJianManager.Inst.IsUnlockedHongDian(1));
			if (checkChuanYin)
			{
				ChuanYinFuPoint.SetActive(player.emailDateMag.newEmailDictionary.Count > 0);
			}
		}
	}

	public void RefreshUI()
	{
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		if (player == null)
		{
			return;
		}
		NameText.text = player.name;
		HPSlider.value = (float)player.HP / (float)player.HP_Max;
		HPText.text = $"{player.HP}/{player.HP_Max}";
		float n = jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n;
		ExpSlider.value = (float)player.exp / n;
		ExpText.text = $"{player.exp}/{(int)n}";
		LevelImage.sprite = LevelSprite[player.level - 1];
		bool flag = player.TianJie.HasField("ShowTianJieCD") && player.TianJie["ShowTianJieCD"].b;
		if (TianJieObj.activeSelf)
		{
			if (!flag)
			{
				TianJieObj.SetActive(false);
			}
			else
			{
				TianJieTime.text = player.TianJie["ShengYuTime"].Str;
			}
		}
		else if (flag)
		{
			TianJieObj.SetActive(true);
		}
		bool flag2 = jsonData.instance.saveState == 1;
		if (CunDangObj.activeSelf && saving)
		{
			if (!flag2)
			{
				saving = false;
				((Tween)DOTween.To((DOGetter<float>)(() => CunDangAlpha.alpha), (DOSetter<float>)delegate(float x)
				{
					CunDangAlpha.alpha = x;
				}, 0f, 0.5f)).onComplete = (TweenCallback)delegate
				{
					CunDangObj.SetActive(false);
				};
			}
		}
		else if (flag2)
		{
			saving = true;
			CunDangAlpha.alpha = 1f;
			CunDangObj.SetActive(true);
		}
	}

	public void RefreshTieJianRedPoint()
	{
		JianLingManager jianLingManager = PlayerEx.Player.jianLingManager;
		bool active = false;
		int jiYiHuiFuDu = jianLingManager.GetJiYiHuiFuDu();
		foreach (JianLingQingJiao data in JianLingQingJiao.DataList)
		{
			if (jiYiHuiFuDu >= data.JiYi)
			{
				bool flag = false;
				if (data.SkillID > 0)
				{
					flag = PlayerEx.HasSkill(data.SkillID);
				}
				else if (data.StaticSkillID > 0)
				{
					flag = PlayerEx.HasStaticSkill(data.StaticSkillID);
				}
				if (!flag)
				{
					active = true;
					break;
				}
				continue;
			}
			break;
		}
		TieJianRedPoint.SetActive(active);
	}

	public bool BtnCanClick()
	{
		Tools.instance.canClick();
		if (CanClickManager.Inst.ResultCount == 0)
		{
			return true;
		}
		if (CanClickManager.Inst.ResultCount == 1 && CanClickManager.Inst.ResultCache[3])
		{
			return true;
		}
		return false;
	}

	public void OnTieJianBtnClick()
	{
		if (BtnCanClick())
		{
			UIJianLingPanel.OpenPanel();
		}
	}

	public void OnChuanYinFuBtnClick()
	{
		if (BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.传音符, 1);
		}
	}

	public void OnTuJianBtnClick()
	{
		if (BtnCanClick())
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.图鉴, 1);
		}
	}

	public void OnHeadBtnClick()
	{
		((MonoBehaviour)this).Invoke("OpenTab", 0.1f);
	}

	private void OpenTab()
	{
		IsMouseInUI = false;
		GameObject.Find("UI Root (2D)").GetComponent<Singleton>().ClickTab();
	}

	public void OnMapBtnClick()
	{
		if (BtnCanClick())
		{
			UIMapPanel.Inst.OpenDefaultMap();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		IsMouseInUI = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		IsMouseInUI = false;
	}
}
