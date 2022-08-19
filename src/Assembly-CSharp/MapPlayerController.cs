using System;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class MapPlayerController : MonoBehaviour
{
	// Token: 0x06001131 RID: 4401 RVA: 0x00067648 File Offset: 0x00065848
	private void Awake()
	{
		MapPlayerController.Inst = this;
		this.NormalShow = base.GetComponent<MapPlayerNormalShow>();
		this.SeaShow = base.GetComponent<MapPlayerSeaShow>();
		this.NormalShow.Init(this);
		this.SeaShow.Init(this);
		this.IsOnSea = SceneEx.NowSceneName.StartsWith("Sea");
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000676A0 File Offset: 0x000658A0
	private void Start()
	{
		base.InvokeRepeating("Refresh", 0.1f, 0.5f);
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000676B8 File Offset: 0x000658B8
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

	// Token: 0x06001134 RID: 4404 RVA: 0x00067774 File Offset: 0x00065974
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

	// Token: 0x06001135 RID: 4405 RVA: 0x00067800 File Offset: 0x00065A00
	public void SetSpeed(int speed)
	{
		this.NormalShow.Anim.SetInteger("speed", speed);
		this.SeaShow.Anim.SetInteger("speed", speed);
	}

	// Token: 0x04000C4C RID: 3148
	public static MapPlayerController Inst;

	// Token: 0x04000C4D RID: 3149
	[HideInInspector]
	public MapPlayerNormalShow NormalShow;

	// Token: 0x04000C4E RID: 3150
	[HideInInspector]
	public MapPlayerSeaShow SeaShow;

	// Token: 0x04000C4F RID: 3151
	[HideInInspector]
	public bool IsNan;

	// Token: 0x04000C50 RID: 3152
	[HideInInspector]
	public bool IsOnSea;

	// Token: 0x04000C51 RID: 3153
	[HideInInspector]
	public MapPlayerShowType ShowType;

	// Token: 0x04000C52 RID: 3154
	[HideInInspector]
	public _ItemJsonData EquipLingZhou;
}
