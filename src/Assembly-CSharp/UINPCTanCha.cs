using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A3 RID: 931
public class UINPCTanCha : MonoBehaviour
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x000E5928 File Offset: 0x000E3B28
	public void RefreshUI()
	{
		UINPCTanCha.animTime = 2f;
		this.GreenBG.SetActive(true);
		this.RedBG.SetActive(false);
		this.TanChaSlider.value = 0f;
		this.TanChaText.text = "正在探查中...";
		this.TanChaText.color = UINPCTanCha.Green;
		Avatar player = Tools.instance.getPlayer();
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		bool flag = false;
		bool flag2;
		if (player.shengShi >= this.npc.ShenShi)
		{
			flag2 = true;
			if (jsonData.instance.AvatarJsonData[this.npc.ID.ToString()].HasField("isTanChaUnlock"))
			{
				jsonData.instance.AvatarJsonData[this.npc.ID.ToString()].SetField("isTanChaUnlock", true);
			}
			else
			{
				jsonData.instance.AvatarJsonData[this.npc.ID.ToString()].AddField("isTanChaUnlock", true);
			}
		}
		else
		{
			flag2 = false;
		}
		if (flag2)
		{
			float num = (1f - (float)(this.npc.ShenShi - player.shengShi) / (float)this.npc.ShenShi) * 2f;
			num *= 100f;
			flag = ((float)Random.Range(0, 100) >= num);
		}
		if (!flag2 || flag)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTweenModuleUI.DOValue(this.TanChaSlider, 1f, UINPCTanCha.animTime, false);
			tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate()
			{
				Debug.Log("探查失败");
				this.TanChaText.text = "探查失败";
				this.TanChaText.color = UINPCTanCha.Red;
				this.GreenBG.SetActive(false);
				this.RedBG.SetActive(true);
				base.Invoke("TanChaShiBai", UINPCTanCha.animTime);
			}));
			return;
		}
		TweenerCore<float, float, FloatOptions> tweenerCore2 = DOTweenModuleUI.DOValue(this.TanChaSlider, 1f, UINPCTanCha.animTime, false);
		tweenerCore2.onComplete = (TweenCallback)Delegate.Combine(tweenerCore2.onComplete, new TweenCallback(delegate()
		{
			Debug.Log("探查成功");
			this.TanChaText.text = "探查成功";
			base.Invoke("TanChaChengGong", UINPCTanCha.animTime);
		}));
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x000E5B04 File Offset: 0x000E3D04
	private void TanChaChengGong()
	{
		this.npc.IsTanChaUnlock = true;
		jsonData.instance.AvatarJsonData[this.npc.ID.ToString()].SetField("isTanChaUnlock", this.npc.IsTanChaUnlock);
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.ShowNPCInfoPanel(null);
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x00016491 File Offset: 0x00014691
	private void TanChaShiBai()
	{
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = true;
	}

	// Token: 0x04001544 RID: 5444
	private UINPCData npc;

	// Token: 0x04001545 RID: 5445
	public Slider TanChaSlider;

	// Token: 0x04001546 RID: 5446
	public Text TanChaText;

	// Token: 0x04001547 RID: 5447
	public GameObject GreenBG;

	// Token: 0x04001548 RID: 5448
	public GameObject RedBG;

	// Token: 0x04001549 RID: 5449
	private static Color Green = new Color(0.64705884f, 0.85882354f, 0.76862746f);

	// Token: 0x0400154A RID: 5450
	private static Color Red = new Color(0.8392157f, 0.70980394f, 0.6039216f);

	// Token: 0x0400154B RID: 5451
	private static float animTime = 2f;
}
