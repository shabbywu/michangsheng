using System;
using System.IO;
using System.Text;
using YSGame;

// Token: 0x020002B5 RID: 693
public class SaveLoadFactory
{
	// Token: 0x0600187A RID: 6266 RVA: 0x000AF63B File Offset: 0x000AD83B
	public SaveLoadFactory()
	{
		this.gamePath = Paths.GetSavePath();
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x000AF650 File Offset: 0x000AD850
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

	// Token: 0x0600187C RID: 6268 RVA: 0x000AF6A4 File Offset: 0x000AD8A4
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

	// Token: 0x0600187D RID: 6269 RVA: 0x000AF6EC File Offset: 0x000AD8EC
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

	// Token: 0x04001375 RID: 4981
	public string gamePath;
}
