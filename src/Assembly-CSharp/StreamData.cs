using System;
using System.Collections.Generic;
using Fungus;
using PaiMai;
using script.MenPaiTask;
using script.YarnEditor.Manager;
using Task;
using YSGame.TianJiDaBi;

// Token: 0x02000542 RID: 1346
[Serializable]
public class StreamData
{
	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06002258 RID: 8792 RVA: 0x0001C220 File Offset: 0x0001A420
	public PaiMaiDataMag PaiMaiDataMag
	{
		get
		{
			if (this._PaiMaiDataMag == null)
			{
				this._PaiMaiDataMag = new PaiMaiDataMag();
			}
			return this._PaiMaiDataMag;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06002259 RID: 8793 RVA: 0x0001C23B File Offset: 0x0001A43B
	public FungusSaveMgr FungusSaveMgr
	{
		get
		{
			if (this._fungusSaveMgr == null)
			{
				this._fungusSaveMgr = new FungusSaveMgr();
			}
			return this._fungusSaveMgr;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x0600225A RID: 8794 RVA: 0x0001C256 File Offset: 0x0001A456
	public FangAnData FangAnData
	{
		get
		{
			if (this._fangAnData == null)
			{
				this._fangAnData = new FangAnData();
			}
			return this._fangAnData;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x0600225B RID: 8795 RVA: 0x0001C271 File Offset: 0x0001A471
	public TaskMag TaskMag
	{
		get
		{
			if (this._taskMag == null)
			{
				this._taskMag = new TaskMag();
			}
			this._taskMag.Init();
			return this._taskMag;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x0600225C RID: 8796 RVA: 0x0001C297 File Offset: 0x0001A497
	// (set) Token: 0x0600225D RID: 8797 RVA: 0x0001C2B2 File Offset: 0x0001A4B2
	public Dictionary<int, int> DanYaoBuFFDict
	{
		get
		{
			if (this._danYaoBuFFDict == null)
			{
				this._danYaoBuFFDict = new Dictionary<int, int>();
			}
			return this._danYaoBuFFDict;
		}
		set
		{
			this._danYaoBuFFDict = value;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x0600225E RID: 8798 RVA: 0x0001C2BB File Offset: 0x0001A4BB
	public TianJiDaBiSaveData TianJiDaBiSaveData
	{
		get
		{
			if (this._tianJiDaBiSaveData == null)
			{
				this._tianJiDaBiSaveData = new TianJiDaBiSaveData();
			}
			return this._tianJiDaBiSaveData;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x0600225F RID: 8799 RVA: 0x0001C2D6 File Offset: 0x0001A4D6
	public SaveValueManager SaveValueManager
	{
		get
		{
			if (this._saveValueManager == null)
			{
				this._saveValueManager = new SaveValueManager();
			}
			this._saveValueManager.Init();
			return this._saveValueManager;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06002260 RID: 8800 RVA: 0x0001C2FC File Offset: 0x0001A4FC
	public MenPaiTaskMag MenPaiTaskMag
	{
		get
		{
			if (this._menPaiTaskMag == null)
			{
				this._menPaiTaskMag = new MenPaiTaskMag();
			}
			this._menPaiTaskMag.Init();
			return this._menPaiTaskMag;
		}
	}

	// Token: 0x04001DAF RID: 7599
	private PaiMaiDataMag _PaiMaiDataMag;

	// Token: 0x04001DB0 RID: 7600
	private FungusSaveMgr _fungusSaveMgr;

	// Token: 0x04001DB1 RID: 7601
	private FangAnData _fangAnData;

	// Token: 0x04001DB2 RID: 7602
	private TaskMag _taskMag;

	// Token: 0x04001DB3 RID: 7603
	private Dictionary<int, int> _danYaoBuFFDict;

	// Token: 0x04001DB4 RID: 7604
	private TianJiDaBiSaveData _tianJiDaBiSaveData;

	// Token: 0x04001DB5 RID: 7605
	private SaveValueManager _saveValueManager;

	// Token: 0x04001DB6 RID: 7606
	private MenPaiTaskMag _menPaiTaskMag;
}
