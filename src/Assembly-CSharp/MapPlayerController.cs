using JSONClass;
using KBEngine;
using UnityEngine;

public class MapPlayerController : MonoBehaviour
{
	public static MapPlayerController Inst;

	[HideInInspector]
	public MapPlayerNormalShow NormalShow;

	[HideInInspector]
	public MapPlayerSeaShow SeaShow;

	[HideInInspector]
	public bool IsNan;

	[HideInInspector]
	public bool IsOnSea;

	[HideInInspector]
	public MapPlayerShowType ShowType;

	[HideInInspector]
	public _ItemJsonData EquipLingZhou;

	private void Awake()
	{
		Inst = this;
		NormalShow = ((Component)this).GetComponent<MapPlayerNormalShow>();
		SeaShow = ((Component)this).GetComponent<MapPlayerSeaShow>();
		NormalShow.Init(this);
		SeaShow.Init(this);
		IsOnSea = SceneEx.NowSceneName.StartsWith("Sea");
	}

	private void Start()
	{
		((MonoBehaviour)this).InvokeRepeating("Refresh", 0.1f, 0.5f);
	}

	public void Refresh()
	{
		Avatar player = PlayerEx.Player;
		IsNan = player.Sex == 1;
		ShowType = MapPlayerShowType.普通人物;
		bool flag = false;
		NormalShow.NowDunShuSpineSeid = FindSpineDunShuSkill();
		if (NormalShow.NowDunShuSpineSeid != null)
		{
			flag = !string.IsNullOrWhiteSpace(NormalShow.NowDunShuSpineSeid.Spine);
		}
		if (IsOnSea)
		{
			EquipLingZhou = player.GetEquipLingZhouData();
			if (EquipLingZhou != null)
			{
				ShowType = MapPlayerShowType.灵舟;
			}
			else if (flag)
			{
				ShowType = MapPlayerShowType.遁术;
			}
			else
			{
				ShowType = MapPlayerShowType.游泳;
			}
		}
		else if (flag)
		{
			ShowType = MapPlayerShowType.遁术;
		}
		NormalShow.Refresh();
		SeaShow.Refresh();
	}

	public StaticSkillSeidJsonData9 FindSpineDunShuSkill()
	{
		foreach (SkillItem equipStaticSkill in PlayerEx.Player.equipStaticSkillList)
		{
			int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			if (StaticSkillJsonData.DataDict[staticSkillKeyByID].seid.Contains(9))
			{
				return StaticSkillSeidJsonData9.DataDict[staticSkillKeyByID];
			}
		}
		return null;
	}

	public void SetSpeed(int speed)
	{
		NormalShow.Anim.SetInteger("speed", speed);
		SeaShow.Anim.SetInteger("speed", speed);
	}
}
