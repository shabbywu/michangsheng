using System;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Fight;

public class RunAwayUI : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Text Desc;

	private Avatar avatar;

	[SerializeField]
	private Transform Panel;

	public void Show()
	{
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		avatar = Tools.instance.getPlayer();
		int num = avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(Tools.instance.MonstarID)]["dunSu"].n;
		if (num > 0 && num <= jsonData.instance.RunawayJsonData.Count - 1)
		{
			setRunawayText(num);
			avatar.AddTime((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunTime"].n);
			try
			{
				randomMapIndex((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunDistance"].n);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
		}
		else if (num > 10)
		{
			setRunawayText(11);
		}
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(Panel, Vector3.one, 0.5f);
		Tools.canClickFlag = false;
		ESCCloseManager.Inst.RegisterClose(this);
	}

	private void setRunawayText(int type)
	{
		int staticDunSu = Tools.instance.getPlayer().getStaticDunSu();
		string newValue = ((staticDunSu > 0) ? ("运起" + Tools.instance.getStaticSkillName(staticDunSu) + "，") : "");
		RunawayJsonData runawayJsonData = RunawayJsonData.DataDict[type];
		string text = "\u3000\u3000" + runawayJsonData.Text;
		text = text.Replace("运起（dunshu），", newValue);
		text = text.Replace("X", runawayJsonData.RunTime.ToString());
		Desc.text = text;
	}

	private void randomMapIndex(int Num)
	{
		int nowMapIndex = avatar.NowMapIndex;
		for (int i = 0; i < Num; i++)
		{
			BaseMapCompont baseMapCompont = AllMapManage.instance.mapIndex[avatar.NowMapIndex];
			int num = 0;
			if (i == 0)
			{
				num = jsonData.instance.getRandom() % baseMapCompont.nextIndex.Count;
			}
			else
			{
				if (baseMapCompont.nextIndex.Count <= 1)
				{
					continue;
				}
				num = jsonData.instance.getRandom() % (baseMapCompont.nextIndex.Count - 1);
			}
			int num2 = 0;
			foreach (int item in baseMapCompont.nextIndex)
			{
				if (nowMapIndex != item)
				{
					if (num == num2)
					{
						nowMapIndex = avatar.NowMapIndex;
						avatar.NowMapIndex = item;
						break;
					}
					num2++;
				}
			}
		}
		AllMapManage.instance.mapIndex[avatar.NowMapIndex].AvatarMoveToThis();
	}

	public void Close()
	{
		Tools.canClickFlag = true;
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
