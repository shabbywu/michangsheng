using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000278 RID: 632
public class UINPCShuangXiuAnim : MonoBehaviour
{
	// Token: 0x0600170A RID: 5898 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x0009D766 File Offset: 0x0009B966
	private void Update()
	{
		if (this.needPlay)
		{
			this.VideoImage.Play();
			this.needPlay = false;
		}
		this.VideoProcessSlider.value = this.VideoImage.PlayProcess;
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x0009D798 File Offset: 0x0009B998
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

	// Token: 0x040011B5 RID: 4533
	private UINPCData npc;

	// Token: 0x040011B6 RID: 4534
	public Sprite FallbackAnim;

	// Token: 0x040011B7 RID: 4535
	public VideoImage VideoImage;

	// Token: 0x040011B8 RID: 4536
	public Slider VideoProcessSlider;

	// Token: 0x040011B9 RID: 4537
	private bool needPlay;

	// Token: 0x040011BA RID: 4538
	public static string[] ningliantypes = new string[]
	{
		"修为",
		"心境",
		"神识",
		"血量上限"
	};
}
