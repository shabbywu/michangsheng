using System;

namespace KBEngine;

public class ZulinContorl
{
	public Avatar entity;

	public string kezhanLastScence = "";

	public ZulinContorl(Entity avater)
	{
		entity = (Avatar)avater;
	}

	public JSONObject getKeZhan(string name)
	{
		if (!entity.ZuLin.HasField(name))
		{
			entity.ZuLin.AddField(name, new JSONObject(JSONObject.Type.OBJECT));
			setJsonObject(entity.ZuLin[name], name, "0001-1-1");
		}
		return entity.ZuLin[name];
	}

	public DateTime getResidueTime(string name)
	{
		if (jsonData.instance.WuXianBiGuanJsonData.list.Find((JSONObject aa) => aa["SceneName"].str == name) != null)
		{
			return DateTime.Parse("4999-12-12");
		}
		return DateTime.Parse(getKeZhan(name)[0].str);
	}

	public static int GetTimeSum(DateTime time)
	{
		return (time.Year - 1) * 12 + time.Month - 1;
	}

	public bool HasTime(string sName)
	{
		DateTime residueTime = getResidueTime(sName);
		return (residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + (residueTime.Day - 1) > 0;
	}

	public void addTime(int addday, int addMonth = 0, int Addyear = 0)
	{
		foreach (string key in entity.ZuLin.keys)
		{
			KZReduceTime(key, addday, addMonth, Addyear);
		}
	}

	public void KZReduceTime(string name, int addday, int addMonth, int Addyear)
	{
		JSONObject keZhan = getKeZhan(name);
		DateTime dateTime = DateTime.Parse(keZhan[0].str);
		DateTime dateTime2 = DateTime.Parse("0001-1-1").AddDays(addday).AddMonths(addMonth)
			.AddYears(Addyear);
		if (dateTime > dateTime2)
		{
			setJsonObject(keZhan, name, DateTime.Parse("0001-1-1").AddDays((dateTime - dateTime2).Days).ToString());
		}
		else
		{
			setJsonObject(keZhan, name, "0001-1-1");
		}
	}

	public void KZAddTime(string name, int addday, int addMonth, int Addyear)
	{
		JSONObject keZhan = getKeZhan(name);
		setJsonObject(keZhan, name, DateTime.Parse(keZhan[0].str).AddDays(addday).AddMonths(addMonth)
			.AddYears(Addyear)
			.ToString());
	}

	private void setJsonObject(JSONObject json, string name, string time)
	{
		json.SetField(name, time);
	}
}
