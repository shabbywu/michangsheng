using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class MapPlayerController : MonoBehaviour
{
	// Token: 0x060013B4 RID: 5044 RVA: 0x000B5C40 File Offset: 0x000B3E40
	private void Awake()
	{
		MapPlayerController.Inst = this;
		this.NormalShow = base.GetComponent<MapPlayerNormalShow>();
		this.SeaShow = base.GetComponent<MapPlayerSeaShow>();
		this.NormalShow.Init(this);
		this.SeaShow.Init(this);
		this.IsOnSea = SceneEx.NowSceneName.StartsWith("Sea");
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x000126DE File Offset: 0x000108DE
	private void Start()
	{
		base.InvokeRepeating("Refresh", 0.1f, 0.5f);
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x000B5C98 File Offset: 0x000B3E98
	public void Refresh()
	{
		Avatar player = PlayerEx.Player;
		this.IsNan = (player.Sex == 1);
		this.ShowType = MapPlayerShowType.普通人物;
		bool flag = false;
		this.NormalShow.NowDunShuSpineSeid = this.FindSpineDunShuSkill();
		if (this.NormalShow.NowDunShuSpineSeid != null)
		{
			flag = !string.IsNullOrWhiteSpace(this.NormalShow.NowDunShuSpineSeid.Spine);
		}
		if (this.IsOnSea)
		{
			this.EquipLingZhou = player.GetEquipLingZhouData();
			if (this.EquipLingZhou != null)
			{
				this.ShowType = MapPlayerShowType.灵舟;
			}
			else if (flag)
			{
				this.ShowType = MapPlayerShowType.遁术;
			}
			else
			{
				this.ShowType = MapPlayerShowType.游泳;
			}
		}
		else if (flag)
		{
			this.ShowType = MapPlayerShowType.遁术;
		}
		this.NormalShow.Refresh();
		this.SeaShow.Refresh();
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000B5D54 File Offset: 0x000B3F54
	public StaticSkillSeidJsonData9 FindSpineDunShuSkill()
	{
		foreach (SkillItem skillItem in PlayerEx.Player.equipStaticSkillList)
		{
			int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
			if (StaticSkillJsonData.DataDict[staticSkillKeyByID].seid.Contains(9))
			{
				return StaticSkillSeidJsonData9.DataDict[staticSkillKeyByID];
			}
		}
		return null;
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000126F5 File Offset: 0x000108F5
	public void SetSpeed(int speed)
	{
		this.NormalShow.Anim.SetInteger("speed", speed);
		this.SeaShow.Anim.SetInteger("speed", speed);
	}

	// Token: 0x04000F4C RID: 3916
	public static MapPlayerController Inst;

	// Token: 0x04000F4D RID: 3917
	[HideInInspector]
	public MapPlayerNormalShow NormalShow;

	// Token: 0x04000F4E RID: 3918
	[HideInInspector]
	public MapPlayerSeaShow SeaShow;

	// Token: 0x04000F4F RID: 3919
	[HideInInspector]
	public bool IsNan;

	// Token: 0x04000F50 RID: 3920
	[HideInInspector]
	public bool IsOnSea;

	// Token: 0x04000F51 RID: 3921
	[HideInInspector]
	public MapPlayerShowType ShowType;

	// Token: 0x04000F52 RID: 3922
	[HideInInspector]
	public _ItemJsonData EquipLingZhou;
}
