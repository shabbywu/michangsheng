using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YSGame.TuJian;

namespace YSGame.Fight;

public class UIFightPanel : MonoBehaviour
{
	public static UIFightPanel Inst;

	public List<Sprite> LingQiSprites;

	public GameObject MoveLingQiPrefab;

	public Transform MoveLingQiRoot;

	[HideInInspector]
	public List<UIFightMoveLingQi> MoveLingQiList = new List<UIFightMoveLingQi>();

	public UICacheLingQiController CacheLingQiController;

	public UIPlayerLingQiController PlayerLingQiController;

	public UIFightCenterButtonController FightCenterButtonController;

	public UIFightCenterTip FightCenterTip;

	public UIFightAvatarStatus PlayerStatus;

	public UIFightAvatarStatus DiRenStatus;

	public UIFightJiLu FightJiLu;

	public UIFightSelectLingQi FightSelectLingQi;

	public List<UIFightSkillItem> FightSkills;

	public List<UIFightWeaponItem> FightWeapon;

	public FpBtn HuaShenLingYuBtn;

	public FpBtn HelpBtn;

	public FpBtn JiLuBtn;

	public FpBtn TaoPaoBtn;

	public GameObject ScaleObj;

	public UIFightSkillTip FightSkillTip;

	public UIFightRoundCount FightRoundCount;

	public List<UILingQiImageData> LingQiImageDatas;

	public UIFightState UIFightState;

	public bool NowDoingLingQiAnim;

	public bool BanSkillAndWeapon;

	private float refreshCD;

	public bool NeedPlayLingQiSound;

	public int NeedYiSanCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				num += PlayerEx.Player.cardMag[i];
			}
			return num - (int)PlayerEx.Player.NowCard;
		}
	}

	private void Awake()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		Inst = this;
		JiLuBtn.mouseUpEvent.AddListener(new UnityAction(FightJiLu.ToggleOpen));
	}

	private void Start()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		TaoPaoBtn.mouseUpEvent.AddListener(new UnityAction(RoundManager.instance.PlayRunAway));
	}

	private void Update()
	{
		bool nowDoingLingQiAnim = NowDoingLingQiAnim;
		NowDoingLingQiAnim = false;
		foreach (UIFightMoveLingQi moveLingQi in MoveLingQiList)
		{
			if (moveLingQi.IsStart)
			{
				NowDoingLingQiAnim = true;
				break;
			}
		}
		if (nowDoingLingQiAnim != NowDoingLingQiAnim)
		{
			RefreshLingQiCount(!NowDoingLingQiAnim);
		}
		if (refreshCD < 0f)
		{
			refreshCD = 1f;
			RefreshCD();
		}
		else
		{
			refreshCD -= Time.deltaTime;
		}
		if (NeedPlayLingQiSound)
		{
			NeedPlayLingQiSound = false;
			PlayLingQiSound();
		}
	}

	public void RefreshLingQiCount(bool show)
	{
		CacheLingQiController.RefreshLingQiCountShow(show);
		PlayerLingQiController.RefreshLingQiCount(show);
	}

	public void Help()
	{
		TuJianManager.Inst.OnHyperlink("2_502_0");
	}

	public void Close()
	{
		ScaleObj.SetActive(false);
	}

	public void Clear()
	{
		((Component)((Component)HuaShenLingYuBtn).transform.parent).gameObject.SetActive(false);
		FightJiLu.JiLuText.text = "";
	}

	public UIFightMoveLingQi GetMoveLingQi()
	{
		for (int i = 0; i < MoveLingQiList.Count; i++)
		{
			if (!MoveLingQiList[i].IsStart)
			{
				return MoveLingQiList[i];
			}
		}
		UIFightMoveLingQi component = Object.Instantiate<GameObject>(MoveLingQiPrefab, MoveLingQiRoot).GetComponent<UIFightMoveLingQi>();
		MoveLingQiList.Add(component);
		return component;
	}

	public void CancelSkillHighlight()
	{
		foreach (UIFightSkillItem fightSkill in FightSkills)
		{
			fightSkill.IsSelected = false;
		}
	}

	public void RefreshCD()
	{
		foreach (UIFightSkillItem fightSkill in FightSkills)
		{
			fightSkill.RefreshCD();
		}
		foreach (UIFightWeaponItem item in FightWeapon)
		{
			item.RefreshCD();
		}
	}

	public void PlayLingQiSound()
	{
		int index = 18 + Random.Range(0, 5);
		MusicMag.instance.PlayEffectMusic(index);
	}
}
