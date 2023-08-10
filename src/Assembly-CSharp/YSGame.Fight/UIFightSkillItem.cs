using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightSkillItem : MonoBehaviour
{
	public Image IconImage;

	public Image QualityUpImage;

	public Image SelectedImage;

	public GameObject CD;

	public Text CDText;

	public Text CDText2;

	public KeyCode HotKey;

	public FpBtn SkillBtn;

	public GameObject LianJiHighlight;

	private bool hasSkill;

	private GUIPackage.Skill nowSkill;

	private bool canClick;

	private float internalCD;

	private bool isSelected;

	public bool HasSkill => hasSkill;

	private bool CanClick
	{
		get
		{
			if (UIFightPanel.Inst.BanSkillAndWeapon)
			{
				return false;
			}
			if (UIFightPanel.Inst.UIFightState == UIFightState.自己回合普通状态 || UIFightPanel.Inst.UIFightState == UIFightState.释放技能准备灵气阶段)
			{
				return canClick;
			}
			return false;
		}
		set
		{
			canClick = value;
		}
	}

	public bool IsSelected
	{
		get
		{
			return isSelected;
		}
		set
		{
			isSelected = value;
			((Component)SelectedImage).gameObject.SetActive(value);
		}
	}

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		SkillBtn.mouseEnterEvent.AddListener(new UnityAction(OnEnter));
		SkillBtn.mouseOutEvent.AddListener(new UnityAction(OnExit));
		SkillBtn.mouseUpEvent.AddListener(new UnityAction(ClickSkill));
	}

	private void Start()
	{
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp(HotKey))
		{
			ClickSkill();
		}
		if (internalCD > 0f)
		{
			internalCD -= Time.deltaTime;
		}
	}

	public void OnEnter()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (CanClick && !IsSelected)
		{
			((Component)SelectedImage).gameObject.SetActive(true);
		}
		if (hasSkill)
		{
			UIFightPanel.Inst.FightSkillTip.SetSkill(nowSkill);
			((Component)UIFightPanel.Inst.FightSkillTip).gameObject.SetActive(true);
			((Component)UIFightPanel.Inst.FightSkillTip).transform.position = new Vector3(((Component)this).transform.position.x, ((Component)UIFightPanel.Inst.FightSkillTip).transform.position.y, ((Component)UIFightPanel.Inst.FightSkillTip).transform.position.z);
		}
	}

	public void OnExit()
	{
		if (CanClick && !IsSelected)
		{
			((Component)SelectedImage).gameObject.SetActive(false);
		}
		((Component)UIFightPanel.Inst.FightSkillTip).gameObject.SetActive(false);
	}

	private void ClickSkill()
	{
		if (!(internalCD <= 0f))
		{
			return;
		}
		internalCD = 0.3f;
		if (hasSkill)
		{
			if (CanClick)
			{
				Debug.Log((object)("点击了技能" + nowSkill.skill_Name));
				UIFightPanel.Inst.CancelSkillHighlight();
				IsSelected = true;
				RoundManager.instance.SetChoiceSkill(ref nowSkill);
			}
			else if (UIFightPanel.Inst.UIFightState == UIFightState.回合结束弃置灵气阶段)
			{
				UIPopTip.Inst.Pop("当前正在消散灵气阶段");
			}
			else
			{
				UIPopTip.Inst.Pop("不满足释放条件");
			}
		}
	}

	public void SetSkill(GUIPackage.Skill skill)
	{
		hasSkill = true;
		nowSkill = skill;
		((Component)IconImage).gameObject.SetActive(true);
		IconImage.sprite = skill.skillIconSprite;
		RefreshCD();
	}

	public void RefreshCD()
	{
		if (hasSkill)
		{
			if (nowSkill.CurCD != 0f)
			{
				CD.SetActive(true);
				CanClick = false;
				IsSelected = false;
				int num = (int)nowSkill.CurCD;
				Avatar player = PlayerEx.Player;
				if (player.SkillSeidFlag.ContainsKey(29) && player.SkillSeidFlag[29].ContainsKey(nowSkill.skill_ID) && player.SkillSeidFlag[29][nowSkill.skill_ID] > 0)
				{
					num = player.SkillSeidFlag[29][nowSkill.skill_ID];
				}
				if (num > 9)
				{
					CDText.text = "";
					((Component)CDText2).gameObject.SetActive(false);
				}
				else
				{
					CDText.text = num.ToString();
					((Component)CDText2).gameObject.SetActive(true);
				}
			}
			else
			{
				CD.SetActive(false);
				CanClick = true;
			}
			SkillCanUseType skillCanUseType = nowSkill.CanUse(PlayerEx.Player, PlayerEx.Player, showError: false);
			if (skillCanUseType != SkillCanUseType.可以使用 && skillCanUseType != SkillCanUseType.尚未冷却不能使用)
			{
				CD.SetActive(true);
				CDText.text = "";
				((Component)CDText2).gameObject.SetActive(false);
				CanClick = false;
				IsSelected = false;
			}
			ShowLianJiHightLight();
		}
		else
		{
			Clear();
		}
	}

	public void ShowLianJiHightLight()
	{
		if (nowSkill.skill_ID == -1)
		{
			return;
		}
		bool active = false;
		List<int> seid = _skillJsonData.DataDict[nowSkill.skill_ID].seid;
		bool flag = false;
		foreach (int item in seid)
		{
			if (jsonData.instance.hightLightSkillID.Contains(item))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Avatar player = Tools.instance.getPlayer();
			if (player == null || player.OtherAvatar == null)
			{
				return;
			}
			if (nowSkill.CanUse(player, player, showError: false) == SkillCanUseType.可以使用)
			{
				Avatar receiver = player;
				if (jsonData.instance.skillJsonData[string.Concat(nowSkill.skill_ID)]["script"].str == "SkillAttack")
				{
					receiver = player.OtherAvatar;
				}
				if (nowSkill.CanRealizeSeid(player, receiver))
				{
					active = true;
				}
			}
		}
		LianJiHighlight.SetActive(active);
	}

	public void Clear()
	{
		hasSkill = false;
		((Component)IconImage).gameObject.SetActive(false);
		CD.SetActive(false);
		CanClick = false;
	}
}
