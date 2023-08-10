using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UINPCTanCha : MonoBehaviour
{
	private UINPCData npc;

	public Slider TanChaSlider;

	public Text TanChaText;

	public GameObject GreenBG;

	public GameObject RedBG;

	private static Color Green = new Color(0.64705884f, 73f / 85f, 0.76862746f);

	private static Color Red = new Color(0.8392157f, 0.70980394f, 0.6039216f);

	private static float animTime = 2f;

	public void RefreshUI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		animTime = 2f;
		GreenBG.SetActive(true);
		RedBG.SetActive(false);
		TanChaSlider.value = 0f;
		TanChaText.text = "正在探查中...";
		((Graphic)TanChaText).color = Green;
		Avatar player = Tools.instance.getPlayer();
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		bool flag = false;
		bool flag2;
		if (player.shengShi >= npc.ShenShi)
		{
			flag2 = true;
			if (jsonData.instance.AvatarJsonData[npc.ID.ToString()].HasField("isTanChaUnlock"))
			{
				jsonData.instance.AvatarJsonData[npc.ID.ToString()].SetField("isTanChaUnlock", val: true);
			}
			else
			{
				jsonData.instance.AvatarJsonData[npc.ID.ToString()].AddField("isTanChaUnlock", val: true);
			}
		}
		else
		{
			flag2 = false;
		}
		if (flag2)
		{
			float num = (1f - (float)(npc.ShenShi - player.shengShi) / (float)npc.ShenShi) * 2f;
			num *= 100f;
			flag = ((!((float)Random.Range(0, 100) < num)) ? true : false);
		}
		if (!flag2 || flag)
		{
			TweenerCore<float, float, FloatOptions> obj = DOTweenModuleUI.DOValue(TanChaSlider, 1f, animTime, false);
			((Tween)obj).onComplete = (TweenCallback)Delegate.Combine((Delegate?)(object)((Tween)obj).onComplete, (Delegate?)(TweenCallback)delegate
			{
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				Debug.Log((object)"探查失败");
				TanChaText.text = "探查失败";
				((Graphic)TanChaText).color = Red;
				GreenBG.SetActive(false);
				RedBG.SetActive(true);
				((MonoBehaviour)this).Invoke("TanChaShiBai", animTime);
			});
		}
		else
		{
			TweenerCore<float, float, FloatOptions> obj2 = DOTweenModuleUI.DOValue(TanChaSlider, 1f, animTime, false);
			((Tween)obj2).onComplete = (TweenCallback)Delegate.Combine((Delegate?)(object)((Tween)obj2).onComplete, (Delegate?)(TweenCallback)delegate
			{
				Debug.Log((object)"探查成功");
				TanChaText.text = "探查成功";
				((MonoBehaviour)this).Invoke("TanChaChengGong", animTime);
			});
		}
	}

	private void TanChaChengGong()
	{
		npc.IsTanChaUnlock = true;
		jsonData.instance.AvatarJsonData[npc.ID.ToString()].SetField("isTanChaUnlock", npc.IsTanChaUnlock);
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.ShowNPCInfoPanel();
	}

	private void TanChaShiBai()
	{
		UINPCJiaoHu.Inst.HideNPCTanChaPanel();
		UINPCJiaoHu.Inst.IsTanChaShiBaiOrFaXian = true;
	}
}
