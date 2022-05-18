using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024D RID: 589
public class DebugCanvasMag : MonoBehaviour
{
	// Token: 0x060011FE RID: 4606 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000AD368 File Offset: 0x000AB568
	public item getAvatarItem(ItemDatebase datebase, int itemID)
	{
		foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
		{
			if (item_INFO.itemId == itemID)
			{
				item item = datebase.items[itemID].Clone();
				item.UUID = item_INFO.uuid;
				return item;
			}
		}
		return null;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000AD3F0 File Offset: 0x000AB5F0
	public void TestLiandan(string text, string value)
	{
		string[] array = value.Split(new char[]
		{
			','
		});
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
		ItemDatebase component = jsonData.instance.GetComponent<ItemDatebase>();
		if (array[2] == "Nomel")
		{
			using (List<JSONObject>.Enumerator enumerator = jsonData.instance.LianDanDanFangBiao.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject jsonobject = enumerator.Current;
					for (int j = 1; j <= 5; j++)
					{
						if (jsonobject["value" + j].I != 0)
						{
							LianDanMag.instence.inventoryCaiLiao.inventory[23 + j] = this.getAvatarItem(component, jsonobject["value" + j].I);
							LianDanMag.instence.inventoryCaiLiao.inventory[23 + j].itemNum = jsonobject["num" + j].I;
						}
					}
					LianDanMag.instence.InventoryShowDanlu.inventory[0] = this.getAvatarItem(component, 11501);
					LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid = new JSONObject();
					LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid.SetField("NaiJiu", 1000);
					LianDanMag.instence.getDanFang();
				}
				return;
			}
		}
		for (int k = 0; k < 100; k++)
		{
			for (int l = 1; l <= 5; l++)
			{
				LianDanMag.instence.inventoryCaiLiao.inventory[23 + l] = this.getAvatarItem(component, list[jsonData.GetRandom() % list.Count]);
				LianDanMag.instence.inventoryCaiLiao.inventory[23 + l].itemNum = jsonData.GetRandom() % 3 + 1;
			}
			LianDanMag.instence.InventoryShowDanlu.inventory[0] = this.getAvatarItem(component, 11501);
			LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid = new JSONObject();
			LianDanMag.instence.InventoryShowDanlu.inventory[0].Seid.SetField("NaiJiu", 1000);
			LianDanMag.instence.getDanFang();
		}
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000AD734 File Offset: 0x000AB934
	public void setjinhao(string text, string value)
	{
		Avatar player = Tools.instance.getPlayer();
		string[] array = value.Split(new char[]
		{
			','
		});
		try
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2228299692U)
			{
				if (num != 91996362U)
				{
					if (num != 368483469U)
					{
						if (num == 2228299692U)
						{
							if (text == "TestLianDan")
							{
								this.TestLiandan(text, value);
							}
						}
					}
					else if (text == "LoadFuBen")
					{
						player.fubenContorl[array[0]].setFirstIndex(int.Parse(array[1]));
						player.zulinContorl.kezhanLastScence = Tools.getScreenName();
						player.lastFuBenScence = Tools.getScreenName();
						player.NowFuBen = array[0];
						Tools.instance.loadMapScenes(array[0], true);
					}
				}
				else if (text == "resetSteamstat")
				{
					SteamChengJiu.ints.ResetStatt();
				}
			}
			else if (num <= 3694102904U)
			{
				if (num != 2865688773U)
				{
					if (num == 3694102904U)
					{
						if (text == "LoadRandomFuBen")
						{
							Tools.instance.getPlayer().randomFuBenMag.GetInRandomFuBen(int.Parse(value), -1);
						}
					}
				}
				else if (text == "GetAchivement")
				{
					SteamChengJiu.ints.SetAchievement(value);
				}
			}
			else if (num != 3846977133U)
			{
				if (num == 4111883381U)
				{
					if (text == "CreateLuanLiuMap")
					{
						player.seaNodeMag.CreateLuanLiuMap();
					}
				}
			}
			else if (text == "CreateMonstar")
			{
				EndlessSeaMag.Inst.CreateMonstar(int.Parse(array[0]), int.Parse(array[1]), array[2], int.Parse(array[3]), false);
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000AD938 File Offset: 0x000ABB38
	public void run()
	{
		string[] array = this.inputField.text.Split(new char[]
		{
			'.'
		});
		object obj = Tools.instance.getPlayer();
		foreach (string text in array)
		{
			if (Regex.Matches(text, "#").Count > 0)
			{
				string text2 = text.Replace("#", "");
				MatchCollection matchCollection = Regex.Matches(text2, "\\(.*\\)");
				if (matchCollection.Count > 0)
				{
					text2 = text2.Replace(matchCollection[0].Value, "");
				}
				this.setjinhao(text2, (matchCollection.Count > 0) ? matchCollection[0].Value.Replace("(", "").Replace(")", "") : "");
			}
			else
			{
				MatchCollection matchCollection2 = Regex.Matches(text, "\\=.*");
				if (matchCollection2.Count > 0)
				{
					string text3 = matchCollection2[0].Value.Replace("=", "");
					FieldInfo[] fields = obj.GetType().GetFields();
					string b = text.Replace(matchCollection2[0].Value, "");
					for (int j = 0; j < fields.Length; j++)
					{
						if (fields[j].Name == b)
						{
							int num = 0;
							if (int.TryParse(text3, out num))
							{
								fields[j].SetValue(obj, num);
							}
							else
							{
								int num2 = 0;
								if (int.TryParse(text3, out num2))
								{
									fields[j].SetValue(obj, num2);
								}
								else
								{
									bool flag = false;
									if (bool.TryParse(text3, out flag))
									{
										fields[j].SetValue(obj, flag);
									}
								}
							}
						}
					}
				}
				else
				{
					MatchCollection matchCollection3 = Regex.Matches(text, "\\(.*\\)");
					if (matchCollection3.Count > 0)
					{
						string[] array3 = matchCollection3[0].Value.Replace("(", "").Replace(")", "").Split(new char[]
						{
							','
						});
						object[] array4 = new object[array3.Length];
						int num3 = -1;
						foreach (string text4 in array3)
						{
							int num4 = 0;
							num3++;
							if (int.TryParse(text4, out num4))
							{
								array4[num3] = num4;
							}
							else
							{
								bool flag2 = false;
								if (bool.TryParse(text4, out flag2))
								{
									array4[num3] = flag2;
								}
								else
								{
									array4[num3] = text4;
								}
							}
						}
						string methodName = text.Replace(matchCollection3[0].Value, "");
						using (IEnumerator<MethodInfo> enumerator = (from x in obj.GetType().GetMethods()
						where x.Name == methodName
						select x).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MethodInfo methodInfo = enumerator.Current;
								try
								{
									methodInfo.Invoke(obj, array4);
								}
								catch (Exception)
								{
								}
							}
							goto IL_344;
						}
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
			IL_344:;
		}
	}

	// Token: 0x04000E8D RID: 3725
	public InputField inputField;
}
