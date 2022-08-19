using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Fungus;
using PaiMai;
using script.EventMsg;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource;
using script.ItemSource.Interface;
using script.MenPaiTask;
using script.NewNpcJiaoHu;
using script.YarnEditor.Manager;
using Task;
using YSGame.TianJiDaBi;

// Token: 0x020003B6 RID: 950
[Serializable]
public class StreamData
{
	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x000D8497 File Offset: 0x000D6697
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

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000D84B2 File Offset: 0x000D66B2
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

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x000D84CD File Offset: 0x000D66CD
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

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000D84E8 File Offset: 0x000D66E8
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

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000D850E File Offset: 0x000D670E
	// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x000D8529 File Offset: 0x000D6729
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

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000D8532 File Offset: 0x000D6732
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

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000D854D File Offset: 0x000D674D
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

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000D8573 File Offset: 0x000D6773
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

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000D8599 File Offset: 0x000D6799
	public EventMag EventMag
	{
		get
		{
			if (this._eventMag == null)
			{
				this._eventMag = new EventMag();
			}
			this._eventMag.Init();
			return this._eventMag;
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000D85BF File Offset: 0x000D67BF
	public NpcJieSuanData NpcJieSuanData
	{
		get
		{
			if (this._npcJieSuanData == null)
			{
				this._npcJieSuanData = new NpcJieSuanData();
			}
			return this._npcJieSuanData;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000D85DA File Offset: 0x000D67DA
	public ElderTaskMag ZhangLaoTaskMag
	{
		get
		{
			if (this._zhangLaoTaskMag == null)
			{
				this._zhangLaoTaskMag = new ElderTaskMag();
			}
			this._zhangLaoTaskMag.Init();
			return this._zhangLaoTaskMag;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000D8600 File Offset: 0x000D6800
	public IExchangeSource ExchangeSource
	{
		get
		{
			if (this._exchangeSource == null)
			{
				this._exchangeSource = new ExchangeSource();
				this._exchangeSource.Init();
			}
			return this._exchangeSource;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000D8626 File Offset: 0x000D6826
	public ABItemSource ABItemSource
	{
		get
		{
			if (this._abItemSource == null)
			{
				this._abItemSource = new ItemSource();
				this._abItemSource.Init();
			}
			return this._abItemSource;
		}
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000D864C File Offset: 0x000D684C
	[OnDeserialized]
	private void Init(StreamingContext context)
	{
		if (this.HasDefeatNpcList == null)
		{
			this.HasDefeatNpcList = new List<int>();
		}
	}

	// Token: 0x0400193C RID: 6460
	private PaiMaiDataMag _PaiMaiDataMag;

	// Token: 0x0400193D RID: 6461
	private FungusSaveMgr _fungusSaveMgr;

	// Token: 0x0400193E RID: 6462
	private FangAnData _fangAnData;

	// Token: 0x0400193F RID: 6463
	private TaskMag _taskMag;

	// Token: 0x04001940 RID: 6464
	private Dictionary<int, int> _danYaoBuFFDict;

	// Token: 0x04001941 RID: 6465
	private TianJiDaBiSaveData _tianJiDaBiSaveData;

	// Token: 0x04001942 RID: 6466
	private SaveValueManager _saveValueManager;

	// Token: 0x04001943 RID: 6467
	private MenPaiTaskMag _menPaiTaskMag;

	// Token: 0x04001944 RID: 6468
	private EventMag _eventMag;

	// Token: 0x04001945 RID: 6469
	private NpcJieSuanData _npcJieSuanData;

	// Token: 0x04001946 RID: 6470
	private ElderTaskMag _zhangLaoTaskMag;

	// Token: 0x04001947 RID: 6471
	private IExchangeSource _exchangeSource;

	// Token: 0x04001948 RID: 6472
	private ABItemSource _abItemSource;

	// Token: 0x04001949 RID: 6473
	public List<int> HasDefeatNpcList = new List<int>();
}
