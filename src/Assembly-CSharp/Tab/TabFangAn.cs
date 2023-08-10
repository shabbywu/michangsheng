using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

public class TabFangAn : UIBase
{
	public Text Name;

	private FpBtn _fpBtn;

	public int Index;

	public TabFangAn(GameObject gameObject, int index)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		_go = gameObject;
		Name = Get<Text>("Text");
		Index = index;
		_go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(ClickEvent));
	}

	public void ClickEvent()
	{
		Avatar player = Tools.instance.getPlayer();
		switch (SingletonMono<TabUIMag>.Instance.TabBag.GetCurBagType())
		{
		case BagType.背包:
			player.StreamData.FangAnData.SwitchFangAn(Index);
			SingletonMono<TabUIMag>.Instance.WuPingPanel.LoadEquipData();
			SingletonMono<TabUIMag>.Instance.TabBag.UpdateItem();
			break;
		case BagType.技能:
			player.nowConfigEquipSkill = Index - 1;
			player.equipSkillList = player.configEquipSkill[player.nowConfigEquipSkill];
			SingletonMono<TabUIMag>.Instance.ShenTongPanel.LoadSkillData();
			break;
		case BagType.功法:
			player.nowConfigEquipStaticSkill = Index - 1;
			player.equipStaticSkillList = player.configEquipStaticSkill[player.nowConfigEquipStaticSkill];
			SingletonMono<TabUIMag>.Instance.GongFaPanel.LoadSkillData();
			StaticSkill.resetSeid(player);
			break;
		}
		SingletonMono<TabUIMag>.Instance.TabFangAnPanel.UpdateCurFanAn();
	}
}
