using System;
using Fungus;
using QiYu;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C67 RID: 3175
	public class AddOption : MonoBehaviour
	{
		// Token: 0x06005679 RID: 22137 RVA: 0x0023E5A2 File Offset: 0x0023C7A2
		private void Awake()
		{
			this.AllMapShiJianOptionJsonData = jsonData.instance.AllMapShiJianOptionJsonData;
			this.AllMapOptionJsonData = jsonData.instance.AllMapOptionJsonData;
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x0023E5C4 File Offset: 0x0023C7C4
		public void addOption(int optionID)
		{
			if (Tools.getScreenName() == "AllMaps")
			{
				ResManager.inst.LoadPrefab("QiYuPanel").Inst(null).GetComponent<QiYuUIMag>().Show(optionID);
				return;
			}
			JSONObject jsonobject = new JSONObject();
			int key = int.Parse(Tools.getScreenName().Replace("F", ""));
			jsonobject = jsonData.instance.FuBenJsonData[key][1][optionID.ToString()];
			Flowchart component = ((jsonobject["optionID"].I != 0) ? (Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/Option" + jsonobject["optionID"].I)) as GameObject) : (Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/OptionBase")) as GameObject)).transform.Find("FlowChat").GetComponent<Flowchart>();
			Block block = component.FindBlock("addOption");
			YSOpenOptions ysoption = this.getYSOption<YSOpenOptions>(block);
			this.optionSet(ysoption, jsonobject);
			for (int i = 1; i <= 3; i++)
			{
				if (component.GetBooleanVariable("btn" + i))
				{
					this.optionSetBtn(ysoption, jsonobject["optionDesc" + i].str, (int)jsonobject["option" + i].n, i - 1);
				}
				else
				{
					this.optionSetBtn(ysoption, jsonobject["optionDesc" + i].str, -1, i - 1);
				}
			}
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x0023E770 File Offset: 0x0023C970
		public void addChoiceOtion(int choicID)
		{
			JSONObject jsonobject = new JSONObject();
			if (Tools.getScreenName() == "AllMaps")
			{
				jsonobject = jsonData.instance.AllMapOptionJsonData[choicID.ToString()];
			}
			else
			{
				int key = int.Parse(Tools.getScreenName().Replace("F", ""));
				jsonobject = jsonData.instance.FuBenJsonData[key][2][choicID.ToString()];
			}
			Flowchart component = (Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/OptionChoiceBase")) as GameObject).transform.Find("FlowChat").GetComponent<Flowchart>();
			Block block = component.FindBlock("addOption");
			YSOpenOptions ysoption = this.getYSOption<YSOpenOptions>(block);
			Avatar player = Tools.instance.getPlayer();
			this.optionSet(ysoption, jsonobject);
			for (int i = 1; i <= 10; i++)
			{
				if (jsonobject.HasField("value" + i))
				{
					component.SetIntegerVariable("Var" + i, (int)jsonobject["value" + i].n);
				}
			}
			Block block2 = component.FindBlock("addValue");
			this.getYSOption<AddTime>(block2).Day = component.GetIntegerVariable("Var1");
			this.getYSOption<AddMoney>(block2).AddNum = component.GetIntegerVariable("Var2");
			this.getYSOption<AddEXP>(block2).AddEXPNum = component.GetIntegerVariable("Var3");
			this.getYSOption<AddXinJin>(block2).AddXinjinNum = component.GetIntegerVariable("Var4");
			this.getYSOption<AddHP>(block2).AddHpNum = component.GetIntegerVariable("Var5");
			int num = 0;
			foreach (JSONObject jsonobject2 in jsonobject["value6"].list)
			{
				player.addItem((int)jsonobject["value6"][num].n, (int)jsonobject["value7"][num].n, Tools.CreateItemSeid((int)jsonobject["value6"][num].n), true);
				num++;
			}
			if (component.GetIntegerVariable("Var10") > 0)
			{
				player.wuDaoMag.AddLingGuangByJsonID(component.GetIntegerVariable("Var10"));
			}
			this.optionSetBtn(ysoption, "确定", 0, 1);
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x0023E9FC File Offset: 0x0023CBFC
		public void optionSet(YSOpenOptions ysOption, JSONObject json)
		{
			string str = json["EventName"].str;
			ysOption.Title = Tools.instance.Code64ToString(str);
			ysOption.Desc = Tools.instance.Code64ToString(json["desc"].str).Replace("\\n", "\n");
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x0023EA5A File Offset: 0x0023CC5A
		public void optionSetBtn(YSOpenOptions ysOption, string name, int jump, int index)
		{
			ysOption.Optin[index] = Tools.instance.Code64ToString(name);
			ysOption.OptinJump[index] = jump;
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x0023EA7C File Offset: 0x0023CC7C
		public T getYSOption<T>(Block block) where T : Command
		{
			foreach (Command command in block.CommandList)
			{
				if (command.GetType() == typeof(T))
				{
					return (T)((object)command);
				}
			}
			return default(T);
		}

		// Token: 0x04005126 RID: 20774
		public JSONObject AllMapShiJianOptionJsonData;

		// Token: 0x04005127 RID: 20775
		public JSONObject AllMapOptionJsonData;
	}
}
