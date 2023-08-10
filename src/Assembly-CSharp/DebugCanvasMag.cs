using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvasMag : MonoBehaviour
{
	public InputField inputField;

	private void Start()
	{
	}

	public item getAvatarItem(ItemDatebase datebase, int itemID)
	{
		foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
		{
			if (value.itemId == itemID)
			{
				item item = datebase.items[itemID].Clone();
				item.UUID = value.uuid;
				return item;
			}
		}
		return null;
	}

	public void TestLiandan(string text, string value)
	{
		string[] array = value.Split(new char[1] { ',' });
		Avatar player = Tools.instance.getPlayer();
		player._HP_Max = 999999999;
		player.setHP(999999999);
		int num = int.Parse(array[0]);
		int num2 = int.Parse(array[1]);
		List<int> list = new List<int>();
		for (int i = num; i <= num2; i++)
		{
			if (jsonData.instance.ItemJsonData.HasField(i.ToString()) && jsonData.instance.ItemJsonData[i.ToString()]["type"].I == 6)
			{
				list.Add(i);
			}
		}
		ItemDatebase component = ((Component)jsonData.instance).GetComponent<ItemDatebase>();
		if (array[2] == "Nomel")
		{
			foreach (JSONObject item in jsonData.instance.LianDanDanFangBiao.list)
			{
				for (int j = 1; j <= 5; j++)
				{
					if (item["value" + j].I != 0)
					{
						LianDanMag.instence.inventoryCaiLiao.inventory[23 + j] = getAvatarItem(component, item["value" + j].I);
						LianDanMag.instence.inventoryCaiLiao.inventory[23 + j].itemNum = item["num" + j].I;
					}
				}
				LianDanMag.instence.InventoryShowDanlu.inventory[0] = getAvatarItem(component, 11501);
				LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid = new JSONObject();
				LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid.SetField("NaiJiu", 1000);
				LianDanMag.instence.getDanFang();
			}
			return;
		}
		for (int k = 0; k < 100; k++)
		{
			for (int l = 1; l <= 5; l++)
			{
				LianDanMag.instence.inventoryCaiLiao.inventory[23 + l] = getAvatarItem(component, list[jsonData.GetRandom() % list.Count]);
				LianDanMag.instence.inventoryCaiLiao.inventory[23 + l].itemNum = jsonData.GetRandom() % 3 + 1;
			}
			LianDanMag.instence.InventoryShowDanlu.inventory[0] = getAvatarItem(component, 11501);
			LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid = new JSONObject();
			LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid.SetField("NaiJiu", 1000);
			LianDanMag.instence.getDanFang();
		}
	}

	public void setjinhao(string text, string value)
	{
		Avatar player = Tools.instance.getPlayer();
		string[] array = value.Split(new char[1] { ',' });
		try
		{
			switch (text)
			{
			case "resetSteamstat":
				SteamChengJiu.ints.ResetStatt();
				break;
			case "GetAchivement":
				SteamChengJiu.ints.SetAchievement(value);
				break;
			case "TestLianDan":
				TestLiandan(text, value);
				break;
			case "LoadRandomFuBen":
				Tools.instance.getPlayer().randomFuBenMag.GetInRandomFuBen(int.Parse(value));
				break;
			case "LoadFuBen":
				player.fubenContorl[array[0]].setFirstIndex(int.Parse(array[1]));
				player.zulinContorl.kezhanLastScence = Tools.getScreenName();
				player.lastFuBenScence = Tools.getScreenName();
				player.NowFuBen = array[0];
				Tools.instance.loadMapScenes(array[0]);
				break;
			case "CreateLuanLiuMap":
				player.seaNodeMag.CreateLuanLiuMap();
				break;
			case "CreateMonstar":
				EndlessSeaMag.Inst.CreateMonstar(int.Parse(array[0]), int.Parse(array[1]), array[2], int.Parse(array[3]));
				break;
			}
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
		}
	}

	public void run()
	{
		string[] array = inputField.text.Split(new char[1] { '.' });
		object obj = Tools.instance.getPlayer();
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (Regex.Matches(text, "#").Count > 0)
			{
				string text2 = text.Replace("#", "");
				MatchCollection matchCollection = Regex.Matches(text2, "\\(.*\\)");
				if (matchCollection.Count > 0)
				{
					text2 = text2.Replace(matchCollection[0].Value, "");
				}
				setjinhao(text2, (matchCollection.Count > 0) ? matchCollection[0].Value.Replace("(", "").Replace(")", "") : "");
				continue;
			}
			MatchCollection matchCollection2 = Regex.Matches(text, "\\=.*");
			if (matchCollection2.Count > 0)
			{
				string text3 = matchCollection2[0].Value.Replace("=", "");
				FieldInfo[] fields = obj.GetType().GetFields();
				string text4 = text.Replace(matchCollection2[0].Value, "");
				for (int j = 0; j < fields.Length; j++)
				{
					if (!(fields[j].Name == text4))
					{
						continue;
					}
					int result = 0;
					if (int.TryParse(text3, out result))
					{
						fields[j].SetValue(obj, result);
						continue;
					}
					int result2 = 0;
					if (int.TryParse(text3, out result2))
					{
						fields[j].SetValue(obj, result2);
						continue;
					}
					bool result3 = false;
					if (bool.TryParse(text3, out result3))
					{
						fields[j].SetValue(obj, result3);
					}
				}
				continue;
			}
			MatchCollection matchCollection3 = Regex.Matches(text, "\\(.*\\)");
			if (matchCollection3.Count > 0)
			{
				string[] array3 = matchCollection3[0].Value.Replace("(", "").Replace(")", "").Split(new char[1] { ',' });
				object[] array4 = new object[array3.Length];
				int num = -1;
				string[] array5 = array3;
				foreach (string text5 in array5)
				{
					int result4 = 0;
					num++;
					if (int.TryParse(text5, out result4))
					{
						array4[num] = result4;
						continue;
					}
					bool result5 = false;
					if (bool.TryParse(text5, out result5))
					{
						array4[num] = result5;
					}
					else
					{
						array4[num] = text5;
					}
				}
				string methodName = text.Replace(matchCollection3[0].Value, "");
				foreach (MethodInfo item in from x in obj.GetType().GetMethods()
					where x.Name == methodName
					select x)
				{
					try
					{
						item.Invoke(obj, array4);
					}
					catch (Exception)
					{
					}
				}
				continue;
			}
			FieldInfo[] fields2 = obj.GetType().GetFields();
			for (int l = 0; l < fields2.Length; l++)
			{
				if (fields2[l].Name == text)
				{
					obj = fields2[l].GetValue(obj);
				}
			}
		}
	}
}
