using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027C RID: 636
public class UINPCTanCha : MonoBehaviour
{
	// Token: 0x06001725 RID: 5925 RVA: 0x0009E048 File Offset: 0x0009C248
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

	// Token: 0x06001726 RID: 5926 RVA: 0x0009E224 File Offset: 0x0009C424
	private void TanChaChengGong()
	{
		this.npc.IsTanChaUnlock = true;
		jsonData.instance.AvatarJsonData[this.npc.ID.ToString()].SetField("isTanChaUnlock", this.npc.IsTanChaUnlock);
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.ShowNPCInfoPanel(null);
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x0009E286 File Offset: 0x0009C486
	private void TanChaShiBai()
	{
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = true;
	}

	// Token: 0x040011D1 RID: 4561
	private UINPCData npc;

	// Token: 0x040011D2 RID: 4562
	public Slider TanChaSlider;

	// Token: 0x040011D3 RID: 4563
	public Text TanChaText;

	// Token: 0x040011D4 RID: 4564
	public GameObject GreenBG;

	// Token: 0x040011D5 RID: 4565
	public GameObject RedBG;

	// Token: 0x040011D6 RID: 4566
	private static Color Green = new Color(0.64705884f, 0.85882354f, 0.76862746f);

	// Token: 0x040011D7 RID: 4567
	private static Color Red = new Color(0.8392157f, 0.70980394f, 0.6039216f);

	// Token: 0x040011D8 RID: 4568
	private static float animTime = 2f;
}
