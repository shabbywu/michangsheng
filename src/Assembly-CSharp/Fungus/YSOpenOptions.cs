using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("YSTools", "YSOpenOptions", "打开选项界面", 0)]
[AddComponentMenu("")]
public class YSOpenOptions : Command
{
	[Tooltip("标题")]
	[SerializeField]
	public string Title = "";

	[Tooltip("描述")]
	[SerializeField]
	public string Desc = "";

	[Tooltip("选项描述")]
	[SerializeField]
	public string[] Optin = new string[3] { "", "", "" };

	[Tooltip("选项跳转")]
	[SerializeField]
	public int[] OptinJump = new int[3];

	[Tooltip("选项prefab")]
	[SerializeField]
	public GameObject OptionObject;

	public override void OnEnter()
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		Tools.canClickFlag = false;
		OptionObject = OptionContral.GetOptionContral();
		OptionObject.gameObject.SetActive(true);
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.OpenLayer(OptionObject, _v: true);
		OptionContral component = OptionObject.GetComponent<OptionContral>();
		component.title.text = Title;
		component.Desc.text = Desc;
		Flowchart flowchat = GetFlowchart();
		int num = 0;
		string[] optin = Optin;
		foreach (string text in optin)
		{
			if (text != "")
			{
				int time = num;
				((Component)component.btn[num]).gameObject.SetActive(true);
				((Component)((Component)component.btn[num]).transform.Find("Text")).GetComponent<Text>().text = text;
				((UnityEventBase)component.btn[num].onClick).RemoveAllListeners();
				((UnityEvent)component.btn[num].onClick).AddListener((UnityAction)delegate
				{
					if (OptinJump[time] == 0)
					{
						UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
						OptionObject.gameObject.SetActive(false);
						Tools.canClickFlag = true;
						if (flowchat.GetIntegerVariable("Var8") > 0)
						{
							Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + flowchat.GetIntegerVariable("Var8")));
						}
					}
					else if (!flowchat.GetBooleanVariable("btn" + (time + 1)))
					{
						UIPopTip.Inst.Pop(flowchat.GetStringVariable("btn" + (time + 1) + "Str"));
					}
					else
					{
						JSONObject jSONObject = new JSONObject();
						if (Tools.getScreenName() == "AllMaps")
						{
							jSONObject = jsonData.instance.AllMapOptionJsonData[OptinJump[time].ToString()];
						}
						else
						{
							int key = int.Parse(Tools.getScreenName().Replace("F", ""));
							jSONObject = jsonData.instance.FuBenJsonData[key][2][OptinJump[time].ToString()];
						}
						if (jSONObject.HasField("value9") && (int)jSONObject["value9"].n > 0)
						{
							new AddOption().addOption((int)jSONObject["value9"].n);
						}
						else
						{
							new AddOption().addChoiceOtion(OptinJump[time]);
						}
					}
				});
			}
			else
			{
				((Component)component.btn[num]).gameObject.SetActive(false);
			}
			num++;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
