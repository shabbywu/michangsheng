using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINPCShuangXiuAnim : MonoBehaviour
{
	private UINPCData npc;

	public Sprite FallbackAnim;

	public VideoImage VideoImage;

	public Slider VideoProcessSlider;

	private bool needPlay;

	public static string[] ningliantypes = new string[4] { "修为", "心境", "神识", "血量上限" };

	private void Start()
	{
	}

	private void Update()
	{
		if (needPlay)
		{
			VideoImage.Play();
			needPlay = false;
		}
		VideoProcessSlider.value = VideoImage.PlayProcess;
	}

	public void RefreshUI()
	{
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		npc.RefreshData();
		VideoImage.FallbackSprites.Clear();
		VideoImage.FallbackSprites.Add(FallbackAnim);
		VideoImage.GroupName = "ShuangXiu";
		((UnityEventBase)VideoImage.OnPlayFinshed).RemoveAllListeners();
		string resultTip;
		if (PlayerEx.Player.ShuangXiuData.HasField("JingYuan"))
		{
			JSONObject jSONObject = PlayerEx.Player.ShuangXiuData["JingYuan"];
			ShuangXiuMiShu shuangXiuMiShu = ShuangXiuMiShu.DataDict[jSONObject["Skill"].I];
			VideoImage.TargetFileName = shuangXiuMiShu.name;
			int num = jSONObject["Count"].I / ShuangXiuLianHuaSuDu.DataDict[jSONObject["PinJie"].I].speed;
			if (jSONObject["Count"].I % ShuangXiuLianHuaSuDu.DataDict[jSONObject["PinJie"].I].speed != 0)
			{
				num++;
			}
			resultTip = string.Format("获得精元{0}\n闭关{1}年{2}月后，可将精元凝练为{3}{4}", jSONObject["Count"].I, num / 12, num % 12, jSONObject["Reward"].I, ningliantypes[shuangXiuMiShu.ningliantype - 1]);
		}
		else
		{
			VideoImage.TargetFileName = ShuangXiuMiShu.DataDict[1].name;
			resultTip = "获得精元0";
		}
		VideoImage.OnPlayFinshed.AddListener((UnityAction)delegate
		{
			UINPCJiaoHu.Inst.HideNPCShuangXiuAnim();
			ResManager.inst.LoadPrefab("UIShuangXiuResultPanel").Inst().GetComponent<UIShuangXiuResultPanel>()
				.Show(resultTip);
		});
		needPlay = true;
	}
}
