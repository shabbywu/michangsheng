using System;
using Fungus;
using QiYu;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FF2 RID: 4082
	public class AddOption : MonoBehaviour
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x0004318F File Offset: 0x0004138F
		private void Awake()
		{
			this.AllMapShiJianOptionJsonData = jsonData.instance.AllMapShiJianOptionJsonData;
			this.AllMapOptionJsonData = jsonData.instance.AllMapOptionJsonData;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0026AF40 File Offset: 0x00269140
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
			Flowchart component = (((int)jsonobject["optionID"].n != 0) ? (Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/Option" + (int)jsonobject["optionID"].n)) as GameObject) : (Object.Instantiate(Resources.Load("talkPrefab/OptionPrefab/OptionBase")) as GameObject)).transform.Find("FlowChat").GetComponent<Flowchart>();
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

		// Token: 0x060060CB RID: 24779 RVA: 0x0026B0EC File Offset: 0x002692EC
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

		// Token: 0x060060CC RID: 24780 RVA: 0x0026B378 File Offset: 0x00269578
		public void optionSet(YSOpenOptions ysOption, JSONObject json)
		{
			string str = json["EventName"].str;
			ysOption.Title = Tools.instance.Code64ToString(str);
			ysOption.Desc = Tools.instance.Code64ToString(json["desc"].str).Replace("\\n", "\n");
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x000431B1 File Offset: 0x000413B1
		public void optionSetBtn(YSOpenOptions ysOption, string name, int jump, int index)
		{
			ysOption.Optin[index] = Tools.instance.Code64ToString(name);
			ysOption.OptinJump[index] = jump;
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0026B3D8 File Offset: 0x002695D8
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

		// Token: 0x04005BE0 RID: 23520
		public JSONObject AllMapShiJianOptionJsonData;

		// Token: 0x04005BE1 RID: 23521
		public JSONObject AllMapOptionJsonData;
	}
}
