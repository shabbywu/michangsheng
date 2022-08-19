using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000325 RID: 805
public class MainUIDataCell : MonoBehaviour
{
	// Token: 0x06001BCA RID: 7114 RVA: 0x000C5BAC File Offset: 0x000C3DAC
	public void Init(int index, int id)
	{
		this.index = index;
		this.id = id;
		this.data = YSNewSaveSystem.GetAvatarSaveData(index, id);
		this.isHas = this.data.HasSave;
		if (this.data.HasSave)
		{
			if (this.data.IsBreak)
			{
				this.level.text = "该存档已损坏";
				this.time.text = "???";
			}
			else
			{
				this.level.text = this.data.AvatarLevelText;
				this.level_Image.sprite = this.data.AvatarLevelSprite;
				this.time.text = this.data.GameTime;
				this.realSaveTime.text = this.data.RealSaveTime;
				this.realSaveTime.gameObject.SetActive(true);
				if (!this.data.IsNewSaveSystem)
				{
					this.realSaveTime.text = this.realSaveTime.text + " <color=red><size=30>旧</size></color>";
				}
				if (id == 0)
				{
					this.autoSaveTips.SetActive(true);
				}
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

	// Token: 0x06001BCB RID: 7115 RVA: 0x000C5D0A File Offset: 0x000C3F0A
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
					if (this.data.IsNewSaveSystem)
					{
						YSNewSaveSystem.LoadSave(this.index, this.id, -1);
					}
					else
					{
						MainUIMag.inst.startGame(this.index, this.id, -1);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("读档失败");
					Debug.LogError(ex);
					UCheckBox.Show("存档读取失败，可能已损坏。如果订阅了模组，请检查是否有模组错误。", null);
				}
			}, null, false);
		}
	}

	// Token: 0x04001659 RID: 5721
	public int index;

	// Token: 0x0400165A RID: 5722
	public int id;

	// Token: 0x0400165B RID: 5723
	public bool isHas;

	// Token: 0x0400165C RID: 5724
	[SerializeField]
	private GameObject hasData;

	// Token: 0x0400165D RID: 5725
	[SerializeField]
	private Text level;

	// Token: 0x0400165E RID: 5726
	[SerializeField]
	private Text time;

	// Token: 0x0400165F RID: 5727
	[SerializeField]
	private Text realSaveTime;

	// Token: 0x04001660 RID: 5728
	[SerializeField]
	private GameObject autoSaveTips;

	// Token: 0x04001661 RID: 5729
	[SerializeField]
	private Image level_Image;

	// Token: 0x04001662 RID: 5730
	[SerializeField]
	private GameObject noData;

	// Token: 0x04001663 RID: 5731
	public SaveSlotData data;
}
