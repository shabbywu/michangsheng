﻿using System;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000FA6 RID: 4006
	[CommandInfo("YSTools", "YSOpenOptions", "打开选项界面", 0)]
	[AddComponentMenu("")]
	public class YSOpenOptions : Command
	{
		// Token: 0x06006FC2 RID: 28610 RVA: 0x002A7A54 File Offset: 0x002A5C54
		public override void OnEnter()
		{
			Tools.canClickFlag = false;
			this.OptionObject = OptionContral.GetOptionContral();
			this.OptionObject.gameObject.SetActive(true);
			UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.OpenLayer(this.OptionObject, true);
			OptionContral component = this.OptionObject.GetComponent<OptionContral>();
			component.title.text = this.Title;
			component.Desc.text = this.Desc;
			Flowchart flowchat = this.GetFlowchart();
			int num = 0;
			foreach (string text in this.Optin)
			{
				if (text != "")
				{
					int time = num;
					component.btn[num].gameObject.SetActive(true);
					component.btn[num].transform.Find("Text").GetComponent<Text>().text = text;
					component.btn[num].onClick.RemoveAllListeners();
					component.btn[num].onClick.AddListener(delegate()
					{
						if (this.OptinJump[time] == 0)
						{
							UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
							this.OptionObject.gameObject.SetActive(false);
							Tools.canClickFlag = true;
							if (flowchat.GetIntegerVariable("Var8") > 0)
							{
								Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + flowchat.GetIntegerVariable("Var8")));
							}
							return;
						}
						if (!flowchat.GetBooleanVariable("btn" + (time + 1)))
						{
							UIPopTip.Inst.Pop(flowchat.GetStringVariable("btn" + (time + 1) + "Str"), PopTipIconType.叹号);
							return;
						}
						JSONObject jsonobject = new JSONObject();
						if (Tools.getScreenName() == "AllMaps")
						{
							jsonobject = jsonData.instance.AllMapOptionJsonData[this.OptinJump[time].ToString()];
						}
						else
						{
							int key = int.Parse(Tools.getScreenName().Replace("F", ""));
							jsonobject = jsonData.instance.FuBenJsonData[key][2][this.OptinJump[time].ToString()];
						}
						if (jsonobject.HasField("value9") && (int)jsonobject["value9"].n > 0)
						{
							new AddOption().addOption((int)jsonobject["value9"].n);
							return;
						}
						new AddOption().addChoiceOtion(this.OptinJump[time]);
					});
				}
				else
				{
					component.btn[num].gameObject.SetActive(false);
				}
				num++;
			}
			this.Continue();
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C36 RID: 23606
		[Tooltip("标题")]
		[SerializeField]
		public string Title = "";

		// Token: 0x04005C37 RID: 23607
		[Tooltip("描述")]
		[SerializeField]
		public string Desc = "";

		// Token: 0x04005C38 RID: 23608
		[Tooltip("选项描述")]
		[SerializeField]
		public string[] Optin = new string[]
		{
			"",
			"",
			""
		};

		// Token: 0x04005C39 RID: 23609
		[Tooltip("选项跳转")]
		[SerializeField]
		public int[] OptinJump = new int[3];

		// Token: 0x04005C3A RID: 23610
		[Tooltip("选项prefab")]
		[SerializeField]
		public GameObject OptionObject;
	}
}
