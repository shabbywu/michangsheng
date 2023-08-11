using Fungus;
using QiYu;
using UnityEngine;

namespace KBEngine;

public class AddOption : MonoBehaviour
{
	public JSONObject AllMapShiJianOptionJsonData;

	public JSONObject AllMapOptionJsonData;

	private void Awake()
	{
		AllMapShiJianOptionJsonData = jsonData.instance.AllMapShiJianOptionJsonData;
		AllMapOptionJsonData = jsonData.instance.AllMapOptionJsonData;
	}

	private void Start()
	{
	}

	public void addOption(int optionID)
	{
		if (Tools.getScreenName() == "AllMaps")
		{
			ResManager.inst.LoadPrefab("QiYuPanel").Inst().GetComponent<QiYuUIMag>()
				.Show(optionID);
			return;
		}
		JSONObject jSONObject = new JSONObject();
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		jSONObject = jsonData.instance.FuBenJsonData[key][1][optionID.ToString()];
		Flowchart component = ((Component)((GameObject)((jSONObject["optionID"].I != 0) ? /*isinst with value type is only supported in some contexts*/: /*isinst with value type is only supported in some contexts*/)).transform.Find("FlowChat")).GetComponent<Flowchart>();
		Block block = component.FindBlock("addOption");
		YSOpenOptions ySOption = getYSOption<YSOpenOptions>(block);
		optionSet(ySOption, jSONObject);
		for (int i = 1; i <= 3; i++)
		{
			if (component.GetBooleanVariable("btn" + i))
			{
				optionSetBtn(ySOption, jSONObject["optionDesc" + i].str, (int)jSONObject["option" + i].n, i - 1);
			}
			else
			{
				optionSetBtn(ySOption, jSONObject["optionDesc" + i].str, -1, i - 1);
			}
		}
	}

	public void addChoiceOtion(int choicID)
	{
		JSONObject jSONObject = new JSONObject();
		if (Tools.getScreenName() == "AllMaps")
		{
			jSONObject = jsonData.instance.AllMapOptionJsonData[choicID.ToString()];
		}
		else
		{
			int key = int.Parse(Tools.getScreenName().Replace("F", ""));
			jSONObject = jsonData.instance.FuBenJsonData[key][2][choicID.ToString()];
		}
		Object obj = Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/OptionChoiceBase"));
		Flowchart component = ((Component)((GameObject)((obj is GameObject) ? obj : null)).transform.Find("FlowChat")).GetComponent<Flowchart>();
		Block block = component.FindBlock("addOption");
		YSOpenOptions ySOption = getYSOption<YSOpenOptions>(block);
		Avatar player = Tools.instance.getPlayer();
		optionSet(ySOption, jSONObject);
		for (int i = 1; i <= 10; i++)
		{
			if (jSONObject.HasField("value" + i))
			{
				component.SetIntegerVariable("Var" + i, (int)jSONObject["value" + i].n);
			}
		}
		Block block2 = component.FindBlock("addValue");
		getYSOption<AddTime>(block2).Day = component.GetIntegerVariable("Var1");
		getYSOption<AddMoney>(block2).AddNum = component.GetIntegerVariable("Var2");
		getYSOption<AddEXP>(block2).AddEXPNum = component.GetIntegerVariable("Var3");
		getYSOption<AddXinJin>(block2).AddXinjinNum = component.GetIntegerVariable("Var4");
		getYSOption<AddHP>(block2).AddHpNum = component.GetIntegerVariable("Var5");
		int num = 0;
		foreach (JSONObject item in jSONObject["value6"].list)
		{
			_ = item;
			player.addItem((int)jSONObject["value6"][num].n, (int)jSONObject["value7"][num].n, Tools.CreateItemSeid((int)jSONObject["value6"][num].n), ShowText: true);
			num++;
		}
		if (component.GetIntegerVariable("Var10") > 0)
		{
			player.wuDaoMag.AddLingGuangByJsonID(component.GetIntegerVariable("Var10"));
		}
		optionSetBtn(ySOption, "确定", 0, 1);
	}

	public void optionSet(YSOpenOptions ysOption, JSONObject json)
	{
		string str = json["EventName"].str;
		ysOption.Title = Tools.instance.Code64ToString(str);
		ysOption.Desc = Tools.instance.Code64ToString(json["desc"].str).Replace("\\n", "\n");
	}

	public void optionSetBtn(YSOpenOptions ysOption, string name, int jump, int index)
	{
		ysOption.Optin[index] = Tools.instance.Code64ToString(name);
		ysOption.OptinJump[index] = jump;
	}

	public T getYSOption<T>(Block block) where T : Command
	{
		foreach (Command command in block.CommandList)
		{
			if (((object)command).GetType() == typeof(T))
			{
				return (T)command;
			}
		}
		return null;
	}
}
