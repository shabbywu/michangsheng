using UnityEngine;
using UnityEngine.UI;

public class UINPCInfoPanel : MonoBehaviour, IESCClose
{
	[HideInInspector]
	public UINPCData npc;

	public PlayerSetRandomFace Face;

	public TabGroup TabGroup;

	public GameObject ShuXing;

	public Text NPCName;

	public Text Age;

	public Text HP;

	public Text QingFen;

	public Text XiuWei;

	public Text ZhuangTai;

	public Text ShouYuan;

	public Text ZiZhi;

	public Text WuXing;

	public Text DunSu;

	public Text ShenShi;

	public GameObject FightShuXing;

	public Text FightHP;

	public Text FightXiuWei;

	public Text FightDunSu;

	public Text FightShenShi;

	private void Start()
	{
	}

	private void Update()
	{
		AutoHide();
	}

	public bool CanShow()
	{
		if (UINPCJiaoHu.AllShouldHide)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && (Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && PanelMamager.inst.nowPanel != PanelMamager.PanelType.空)
		{
			return false;
		}
		return true;
	}

	private void AutoHide()
	{
		if (!CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCInfoPanel();
		}
	}

	public void RefreshUI(UINPCData data = null)
	{
		if (data == null)
		{
			npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		}
		else
		{
			npc = data;
		}
		Face.SetNPCFace(npc.ID);
		NPCName.text = npc.Name;
		if (npc.IsFight)
		{
			SetFightInfo();
		}
		else
		{
			SetNPCInfo();
		}
		TabGroup.SetFirstTab();
	}

	public void SetNPCInfo()
	{
		ShuXing.SetActive(true);
		FightShuXing.SetActive(false);
		Age.text = npc.Age.ToString();
		HP.text = npc.HP.ToString();
		if (npc.Favor >= 200)
		{
			QingFen.text = $"{npc.Favor} (满)";
		}
		else
		{
			QingFen.text = $"{npc.Favor} ({(int)(npc.FavorPer * 100f)}%)";
		}
		XiuWei.text = npc.LevelStr;
		ZhuangTai.text = npc.ZhuangTaiStr;
		ShouYuan.text = npc.ShouYuan.ToString();
		ZiZhi.text = npc.ZiZhi.ToString();
		WuXing.text = npc.WuXing.ToString();
		DunSu.text = npc.DunSu.ToString();
		ShenShi.text = npc.ShenShi.ToString();
	}

	public void SetFightInfo()
	{
		ShuXing.SetActive(false);
		FightShuXing.SetActive(true);
		FightHP.text = npc.HP.ToString();
		FightXiuWei.text = npc.LevelStr;
		FightDunSu.text = npc.DunSu.ToString();
		FightShenShi.text = npc.ShenShi.ToString();
	}

	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCInfoPanel();
		return true;
	}
}
