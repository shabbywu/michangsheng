using System;
using System.IO;
using System.Text;
using YSGame;

// Token: 0x020003F1 RID: 1009
public class SaveLoadFactory
{
	// Token: 0x06001B6E RID: 7022 RVA: 0x0001714B File Offset: 0x0001534B
	public SaveLoadFactory()
	{
		this.gamePath = Paths.GetSavePath();
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x000F5E74 File Offset: 0x000F4074
	public JSONObject GetJSONObject(string fileName)
	{
		try
		{
			string text = this.ReadOutTxt(fileName);
			if (text != null && text != "")
			{
				return new JSONObject(text, -2, false, false);
			}
		}
		catch (Exception)
		{
			return new JSONObject();
		}
		return new JSONObject();
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x000F5EC8 File Offset: 0x000F40C8
	public int GetInt(string fileName)
	{
		try
		{
			string text = this.ReadOutTxt(fileName);
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

	// Token: 0x06001B71 RID: 7025 RVA: 0x000F5F10 File Offset: 0x000F4110
	public string ReadOutTxt(string TextName)
	{
		string text = "";
		try
		{
			string path = this.gamePath + "/" + TextName + ".sav";
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

	// Token: 0x0400171B RID: 5915
	public string gamePath;
}
