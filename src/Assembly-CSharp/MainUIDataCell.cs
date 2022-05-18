using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x0200048A RID: 1162
public class MainUIDataCell : MonoBehaviour
{
	// Token: 0x06001F0A RID: 7946 RVA: 0x0010AC94 File Offset: 0x00108E94
	public void Init(int index, int id)
	{
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(index, id)))
		{
			if (YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(index, id), null).IsNull)
			{
				this.isHas = false;
			}
			else
			{
				this.isHas = true;
				if (FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(index, id)) > 4 && !YSSaveGame.HasFile(Paths.GetSavePath(), "IsComplete" + Tools.instance.getSaveID(index, id)))
				{
					this.isHas = false;
				}
			}
		}
		else
		{
			this.isHas = false;
		}
		if (this.isHas)
		{
			try
			{
				JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(index, id), null);
				int i = jsonObject["avatarLevel"].I;
				JSONObject jsonobject = jsonData.instance.LevelUpDataJsonData[i.ToString()];
				this.level.text = jsonobject["Name"].Str;
				this.level_Image.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", jsonObject["avatarLevel"].I));
				if (id == 0)
				{
					this.autoSaveTips.SetActive(true);
				}
				this.index = index;
				this.id = id;
				DateTime dateTime = DateTime.Parse(jsonObject["gameTime"].Str);
				this.time.text = string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
				string textNameData = YSSaveGame.GetTextNameData("AvatarSavetime" + Tools.instance.getSaveID(index, id));
				this.realSaveTime.text = textNameData;
				this.realSaveTime.gameObject.SetActive(true);
			}
			catch
			{
				this.level.text = "该存档已损坏";
				this.time.text = "???";
			}
			this.noData.SetActive(false);
			this.hasData.SetActive(true);
		}
		else
		{
			this.noData.SetActive(true);
			this.hasData.SetActive(false);
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x00019BAD File Offset: 0x00017DAD
	public void Click()
	{
		if (this.isHas)
		{
			TySelect.inst.Show("是否读取当前存档", delegate
			{
				if (FpUIMag.inst != null)
				{
					Object.Destroy(FpUIMag.inst.gameObject);
				}
				YSSaveGame.Reset();
				KBEngineApp.app.entities[10] = null;
				KBEngineApp.app.entities.Remove(10);
				try
				{
					MainUIMag.inst.startGame(this.index, this.id, -1);
				}
				catch (Exception ex)
				{
					Debug.LogError("读档失败");
					Debug.LogError(ex);
					UIPopTip.Inst.Pop("存档可能因云存档已损坏，无法读取", PopTipIconType.叹号);
				}
			}, null, false);
		}
	}

	// Token: 0x04001A7E RID: 6782
	public int index;

	// Token: 0x04001A7F RID: 6783
	public int id;

	// Token: 0x04001A80 RID: 6784
	public bool isHas;

	// Token: 0x04001A81 RID: 6785
	[SerializeField]
	private GameObject hasData;

	// Token: 0x04001A82 RID: 6786
	[SerializeField]
	private Text level;

	// Token: 0x04001A83 RID: 6787
	[SerializeField]
	private Text time;

	// Token: 0x04001A84 RID: 6788
	[SerializeField]
	private Text realSaveTime;

	// Token: 0x04001A85 RID: 6789
	[SerializeField]
	private GameObject autoSaveTips;

	// Token: 0x04001A86 RID: 6790
	[SerializeField]
	private Image level_Image;

	// Token: 0x04001A87 RID: 6791
	[SerializeField]
	private GameObject noData;
}
