using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightWeaponItem : MonoBehaviour
{
	public Image IconImage;

	public Image QualityUpImage;

	public Image SelectedImage;

	public GameObject CD;

	public Text CDText;

	public Text CDText2;

	public KeyCode HotKey;

	public FpBtn SkillBtn;

	public GameObject Lock;

	private bool hasSkill;

	private GUIPackage.Skill nowSkill;

	private ITEM_INFO nowWeapon;

	private bool canClick;

	private ItemDatebase itemDatebase;

	private float internalCD;

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
		itemDatebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
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
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (hasSkill)
		{
			UIFightPanel.Inst.FightSkillTip.SetWeapon(nowSkill, nowWeapon);
			((Component)UIFightPanel.Inst.FightSkillTip).gameObject.SetActive(true);
			((Component)UIFightPanel.Inst.FightSkillTip).transform.position = new Vector3(((Component)this).transform.position.x, ((Component)UIFightPanel.Inst.FightSkillTip).transform.position.y, ((Component)UIFightPanel.Inst.FightSkillTip).transform.position.z);
		}
	}

	public void OnExit()
	{
		((Component)UIFightPanel.Inst.FightSkillTip).gameObject.SetActive(false);
	}

	private void ClickSkill()
	{
		if (!(internalCD <= 0f))
		{
			return;
		}
		internalCD = 0.3f;
		if (!hasSkill)
		{
			return;
		}
		if (CanClick)
		{
			Debug.Log((object)("点击了武器" + nowSkill.skill_Name));
			UIFightPanel.Inst.CancelSkillHighlight();
			UIFightPanel.Inst.CacheLingQiController.MoveAllLingQiToPlayer();
			UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
			RoundManager.instance.ChoiceSkill = nowSkill;
			RoundManager.instance.UseSkill(nowWeapon.uuid);
			if ((Object)(object)this == (Object)(object)UIFightPanel.Inst.FightWeapon[0])
			{
				MessageMag.Instance.Send(FightFaBaoShow.PlayerUseWeaponMsgKey);
			}
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

	public void SetWeapon(GUIPackage.Skill skill, ITEM_INFO weapon)
	{
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		hasSkill = true;
		nowSkill = skill;
		nowWeapon = weapon;
		nowSkill.weaponuuid = weapon.uuid;
		((Component)IconImage).gameObject.SetActive(true);
		if ((Object)(object)IconImage.sprite.texture != (Object)(object)itemDatebase.items[nowWeapon.itemId].itemIcon)
		{
			IconImage.sprite = Sprite.Create(itemDatebase.items[nowWeapon.itemId].itemIcon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
			QualityUpImage.sprite = itemDatebase.items[nowWeapon.itemId].newitemPingZhiSprite;
		}
		else
		{
			IconImage.sprite = Sprite.Create(nowSkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
		}
		if (skill.ItemAddSeid != null)
		{
			IconImage.sprite = Sprite.Create(nowSkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
			int i = weapon.Seid["quality"].I;
			QualityUpImage.sprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_quality_" + (i + 1));
		}
		((Component)QualityUpImage).gameObject.SetActive(true);
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
				int num = (int)nowSkill.CurCD;
				_ = PlayerEx.Player;
				if (nowWeapon.uuid != "" && RoundManager.instance.WeaponSkillList.ContainsKey(nowWeapon.uuid))
				{
					num = RoundManager.instance.WeaponSkillList[nowWeapon.uuid];
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
			}
		}
		else
		{
			Clear();
		}
	}

	public void SetLock(bool isLock)
	{
		Lock.SetActive(isLock);
	}

	public void Clear()
	{
		hasSkill = false;
		((Component)IconImage).gameObject.SetActive(false);
		((Component)QualityUpImage).gameObject.SetActive(false);
		CD.SetActive(false);
		CDText.text = "";
		((Component)CDText2).gameObject.SetActive(false);
		CanClick = false;
	}
}
