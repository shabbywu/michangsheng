using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Fungus;
using PaiMai;
using Task;
using YSGame.TianJiDaBi;
using script.EventMsg;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource;
using script.ItemSource.Interface;
using script.MenPaiTask;
using script.NewNpcJiaoHu;
using script.YarnEditor.Manager;

[Serializable]
public class StreamData
{
	private PaiMaiDataMag _PaiMaiDataMag;

	private FungusSaveMgr _fungusSaveMgr;

	private FangAnData _fangAnData;

	private TaskMag _taskMag;

	private Dictionary<int, int> _danYaoBuFFDict;

	private TianJiDaBiSaveData _tianJiDaBiSaveData;

	private SaveValueManager _saveValueManager;

	private MenPaiTaskMag _menPaiTaskMag;

	private EventMag _eventMag;

	private NpcJieSuanData _npcJieSuanData;

	private ElderTaskMag _zhangLaoTaskMag;

	private IExchangeSource _exchangeSource;

	private ABItemSource _abItemSource;

	public List<int> HasDefeatNpcList = new List<int>();

	public PaiMaiDataMag PaiMaiDataMag
	{
		get
		{
			if (_PaiMaiDataMag == null)
			{
				_PaiMaiDataMag = new PaiMaiDataMag();
			}
			return _PaiMaiDataMag;
		}
	}

	public FungusSaveMgr FungusSaveMgr
	{
		get
		{
			if (_fungusSaveMgr == null)
			{
				_fungusSaveMgr = new FungusSaveMgr();
			}
			return _fungusSaveMgr;
		}
	}

	public FangAnData FangAnData
	{
		get
		{
			if (_fangAnData == null)
			{
				_fangAnData = new FangAnData();
			}
			return _fangAnData;
		}
	}

	public TaskMag TaskMag
	{
		get
		{
			if (_taskMag == null)
			{
				_taskMag = new TaskMag();
			}
			_taskMag.Init();
			return _taskMag;
		}
	}

	public Dictionary<int, int> DanYaoBuFFDict
	{
		get
		{
			if (_danYaoBuFFDict == null)
			{
				_danYaoBuFFDict = new Dictionary<int, int>();
			}
			return _danYaoBuFFDict;
		}
		set
		{
			_danYaoBuFFDict = value;
		}
	}

	public TianJiDaBiSaveData TianJiDaBiSaveData
	{
		get
		{
			if (_tianJiDaBiSaveData == null)
			{
				_tianJiDaBiSaveData = new TianJiDaBiSaveData();
			}
			return _tianJiDaBiSaveData;
		}
	}

	public SaveValueManager SaveValueManager
	{
		get
		{
			if (_saveValueManager == null)
			{
				_saveValueManager = new SaveValueManager();
			}
			_saveValueManager.Init();
			return _saveValueManager;
		}
	}

	public MenPaiTaskMag MenPaiTaskMag
	{
		get
		{
			if (_menPaiTaskMag == null)
			{
				_menPaiTaskMag = new MenPaiTaskMag();
			}
			_menPaiTaskMag.Init();
			return _menPaiTaskMag;
		}
	}

	public EventMag EventMag
	{
		get
		{
			if (_eventMag == null)
			{
				_eventMag = new EventMag();
			}
			_eventMag.Init();
			return _eventMag;
		}
	}

	public NpcJieSuanData NpcJieSuanData
	{
		get
		{
			if (_npcJieSuanData == null)
			{
				_npcJieSuanData = new NpcJieSuanData();
			}
			return _npcJieSuanData;
		}
	}

	public ElderTaskMag ZhangLaoTaskMag
	{
		get
		{
			if (_zhangLaoTaskMag == null)
			{
				_zhangLaoTaskMag = new ElderTaskMag();
			}
			_zhangLaoTaskMag.Init();
			return _zhangLaoTaskMag;
		}
	}

	public IExchangeSource ExchangeSource
	{
		get
		{
			if (_exchangeSource == null)
			{
				_exchangeSource = new ExchangeSource();
				_exchangeSource.Init();
			}
			return _exchangeSource;
		}
	}

	public ABItemSource ABItemSource
	{
		get
		{
			if (_abItemSource == null)
			{
				_abItemSource = new ItemSource();
				_abItemSource.Init();
			}
			return _abItemSource;
		}
	}

	[OnDeserialized]
	private void Init(StreamingContext context)
	{
		if (HasDefeatNpcList == null)
		{
			HasDefeatNpcList = new List<int>();
		}
	}
}
