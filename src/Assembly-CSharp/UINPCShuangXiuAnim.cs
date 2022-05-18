using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039D RID: 925
public class UINPCShuangXiuAnim : MonoBehaviour
{
	// Token: 0x060019D6 RID: 6614 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0001638A File Offset: 0x0001458A
	private void Update()
	{
		if (this.needPlay)
		{
			this.VideoImage.Play();
			this.needPlay = false;
		}
		this.VideoProcessSlider.value = this.VideoImage.PlayProcess;
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000E50EC File Offset: 0x000E32EC
	public void RefreshUI()
	{
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.VideoImage.FallbackSprites.Clear();
		this.VideoImage.FallbackSprites.Add(this.FallbackAnim);
		this.VideoImage.GroupName = "ShuangXiu";
		this.VideoImage.OnPlayFinshed.RemoveAllListeners();
		string resultTip;
		if (PlayerEx.Player.ShuangXiuData.HasField("JingYuan"))
		{
			JSONObject jsonobject = PlayerEx.Player.ShuangXiuData["JingYuan"];
			ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jsonobject["Skill"].I];
			this.VideoImage.TargetFileName = shuangXiuMiShu.name;
			int num = jsonobject["Count"].I / ShuangXiuLianHuaSuDu.DataDict[jsonobject["PinJie"].I].speed;
			if (jsonobject["Count"].I % ShuangXiuLianHuaSuDu.DataDict[jsonobject["PinJie"].I].speed != 0)
			{
				num++;
			}
			resultTip = string.Format("获得精元{0}\n闭关{1}年{2}月后，可将精元凝练为{3}{4}", new object[]
			{
				jsonobject["Count"].I,
				num / 12,
				num % 12,
				jsonobject["Reward"].I,
				UINPCShuangXiuAnim.ningliantypes[shuangXiuMiShu.ningliantype - 1]
			});
		}
		else
		{
			this.VideoImage.TargetFileName = ShuangXiuMiShu.DataDict[1].name;
			resultTip = "获得精元0";
		}
		this.VideoImage.OnPlayFinshed.AddListener(delegate()
		{
			UINPCJiaoHu.Inst.HideNPCShuangXiuAnim();
			ResManager.inst.LoadPrefab("UIShuangXiuResultPanel").Inst(null).GetComponent<UIShuangXiuResultPanel>().Show(resultTip, null);
		});
		this.needPlay = true;
	}

	// Token: 0x04001525 RID: 5413
	private UINPCData npc;

	// Token: 0x04001526 RID: 5414
	public Sprite FallbackAnim;

	// Token: 0x04001527 RID: 5415
	public VideoImage VideoImage;

	// Token: 0x04001528 RID: 5416
	public Slider VideoProcessSlider;

	// Token: 0x04001529 RID: 5417
	private bool needPlay;

	// Token: 0x0400152A RID: 5418
	public static string[] ningliantypes = new string[]
	{
		"修为",
		"心境",
		"神识",
		"血量上限"
	};
}
