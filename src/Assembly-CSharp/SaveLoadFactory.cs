using System;
using System.IO;
using System.Text;
using YSGame;

public class SaveLoadFactory
{
	public string gamePath;

	public SaveLoadFactory()
	{
		gamePath = Paths.GetSavePath();
	}

	public JSONObject GetJSONObject(string fileName)
	{
		try
		{
			string text = ReadOutTxt(fileName);
			if (text != null && text != "")
			{
				return new JSONObject(text);
			}
		}
		catch (Exception)
		{
			return new JSONObject();
		}
		return new JSONObject();
	}

	public int GetInt(string fileName)
	{
		try
		{
			string text = ReadOutTxt(fileName);
			if (text != null && text != "")
			{
				return int.Parse(text);
			}
		}
		catch (Exception)
		{
			return 0;
		}
		return 0;
	}

	public string ReadOutTxt(string TextName)
	{
		string text = "";
		try
		{
			string path = gamePath + "/" + TextName + ".sav";
			if (File.Exists(path))
			{
				StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
				text = streamReader.ReadToEnd();
				text = text.Replace(YSSaveGame.huanHangChar, '\n');
				streamReader.Dispose();
				streamReader.Close();
			}
		}
		catch (Exception)
		{
			return text;
		}
		return text;
	}
}
