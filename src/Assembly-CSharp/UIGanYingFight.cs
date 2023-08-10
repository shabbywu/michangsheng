using UnityEngine;
using UnityEngine.UI;

public class UIGanYingFight : MonoBehaviour
{
	public TianJieMiShuLingWuFightEventProcessor Processor;

	public Text RoundText;

	public Text DescText;

	public Text DangQianJinDuText;

	public Text LiShiJinDuText;

	public void Refresh()
	{
		int num = Processor.miShuData.RoundLimit - RoundManager.instance.StaticRoundNum + 1;
		RoundText.text = $"剩余{num}回合";
		DescText.text = Processor.miShuData.desc ?? "";
		DangQianJinDuText.text = $"当前进度：{Processor.recordValue}";
		LiShiJinDuText.text = $"历史进度：{Processor.GetSaveRecordValue()}";
	}

	private void Update()
	{
		if ((Object)(object)RoundManager.instance == (Object)null || Processor == null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}
}
