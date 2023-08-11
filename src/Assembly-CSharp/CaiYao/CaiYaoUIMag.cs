using DG.Tweening;
using JSONClass;
using QiYu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CaiYao;

public class CaiYaoUIMag : MonoBehaviour
{
	[SerializeField]
	private Transform Panel;

	[SerializeField]
	private GameObject OkBtn;

	[SerializeField]
	private UIIconShow Item;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Transform OptionList;

	[SerializeField]
	private GameObject Option;

	public static CaiYaoUIMag Inst;

	public void ShowAfterFight(ItemData data)
	{
		Init();
		Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc4, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
		Tools.instance.getPlayer().AddTime(data.AddTime);
		Item.SetItem(data.ItemId);
		Item.Count = data.ItemNum;
		Tools.instance.getPlayer().addItem(data.ItemId, data.ItemNum, Tools.CreateItemSeid(data.ItemId));
		OkBtn.SetActive(true);
		((Component)OptionList).gameObject.SetActive(false);
	}

	public void ShowNomal()
	{
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		Init();
		ItemData data = GetRandomYaoCai();
		Item.SetItem(data.ItemId);
		Item.Count = data.ItemNum;
		if (data.HasEnemy)
		{
			Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc2, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
			Option.Inst(OptionList).GetComponent<QiYuOption>().Init("转身离开", new UnityAction(Close));
			Option.Inst(OptionList).GetComponent<QiYuOption>().Init("出手抢夺", (UnityAction)delegate
			{
				data.AddTime = 0;
				Tools.instance.SetCaiYaoData(data);
				Tools.instance.startNomalFight(data.FirstEnemyId);
				Close();
			});
			return;
		}
		Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc1, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
		UnityAction val = default(UnityAction);
		UnityAction val2 = default(UnityAction);
		UnityAction val3 = default(UnityAction);
		UnityAction val4 = default(UnityAction);
		Option.Inst(OptionList).GetComponent<QiYuOption>().Init("继续等待", (UnityAction)delegate
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00af: Expected O, but got Unknown
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Expected O, but got Unknown
			//IL_00f8: Expected O, but got Unknown
			Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc5, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
			Tools.ClearObj(Option.transform);
			QiYuOption component = Option.Inst(OptionList).GetComponent<QiYuOption>();
			UnityAction obj = val;
			if (obj == null)
			{
				UnityAction val5 = delegate
				{
					//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
					//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
					//IL_00aa: Expected O, but got Unknown
					//IL_00af: Expected O, but got Unknown
					//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
					//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
					//IL_00f3: Expected O, but got Unknown
					//IL_00f8: Expected O, but got Unknown
					Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc6, data.ItemId, data.ItemNum, data.ItemNum, data.AddTime);
					Tools.ClearObj(Option.transform);
					QiYuOption component3 = Option.Inst(OptionList).GetComponent<QiYuOption>();
					UnityAction obj3 = val2;
					if (obj3 == null)
					{
						UnityAction val8 = delegate
						{
							data.ItemNum += data.AddNum;
							Tools.instance.SetCaiYaoData(data);
							Tools.instance.startNomalFight(data.ScondEnemyId);
							Close();
						};
						UnityAction val9 = val8;
						val2 = val8;
						obj3 = val9;
					}
					component3.Init("果断出手", obj3);
					QiYuOption component4 = Option.Inst(OptionList).GetComponent<QiYuOption>();
					UnityAction obj4 = val3;
					if (obj4 == null)
					{
						UnityAction val10 = delegate
						{
							Close();
						};
						UnityAction val9 = val10;
						val3 = val10;
						obj4 = val9;
					}
					component4.Init("还是算了", obj4);
				};
				UnityAction val6 = val5;
				val = val5;
				obj = val6;
			}
			component.Init("一旁潜伏", obj);
			QiYuOption component2 = Option.Inst(OptionList).GetComponent<QiYuOption>();
			UnityAction obj2 = val4;
			if (obj2 == null)
			{
				UnityAction val7 = delegate
				{
					Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc7, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
					Tools.instance.getPlayer().AddTime(data.AddTime);
					Item.Count = data.ItemNum + data.AddNum;
					Tools.instance.getPlayer().addItem(data.ItemId, Item.Count, Tools.CreateItemSeid(data.ItemId));
					OkBtn.SetActive(true);
					((Component)OptionList).gameObject.SetActive(false);
				};
				UnityAction val6 = val7;
				val4 = val7;
				obj2 = val6;
			}
			component2.Init("布阵等待", obj2);
		});
		Option.Inst(OptionList).GetComponent<QiYuOption>().Init("立刻采集", (UnityAction)delegate
		{
			Desc.text = GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc3, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
			Tools.instance.getPlayer().addItem(data.ItemId, data.ItemNum, Tools.CreateItemSeid(data.ItemId));
			OkBtn.SetActive(true);
			((Component)OptionList).gameObject.SetActive(false);
		});
	}

	private void Init()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(Panel, Vector3.one, 0.5f);
		PanelMamager.CanOpenOrClose = false;
		Tools.canClickFlag = false;
		Inst = this;
	}

	private ItemData GetRandomYaoCai()
	{
		int iD = AllMapCaiJiBiao.DataList[NpcJieSuanManager.inst.getRandomInt(0, AllMapCaiJiBiao.DataList.Count - 1)].ID;
		int item = AllMapCaiJiBiao.DataDict[iD].Item;
		int randomInt = NpcJieSuanManager.inst.getRandomInt(AllMapCaiJiBiao.DataDict[iD].Num[0], AllMapCaiJiBiao.DataDict[iD].Num[1]);
		int quality = _ItemJsonData.DataDict[item].quality;
		int num = 1 + randomInt * (AllMapCaiJiAddItemBiao.DataDict[quality].percent / 100);
		int addTime = num * AllMapCaiJiAddItemBiao.DataDict[quality].time;
		bool hasEnemy = false;
		int firstEnemyId = 0;
		if (AllMapCaiJiBiao.DataDict[iD].percent >= NpcJieSuanManager.inst.getRandomInt(0, 100))
		{
			firstEnemyId = AllMapCaiJiBiao.DataDict[iD].Monstar;
			hasEnemy = true;
		}
		int maiFuMonstar = AllMapCaiJiBiao.DataDict[iD].MaiFuMonstar;
		return new ItemData(item, randomInt, num, addTime, hasEnemy, firstEnemyId, maiFuMonstar);
	}

	private void OnDestroy()
	{
		Inst = null;
	}

	public void Close()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.canClickFlag = true;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)27) && OkBtn.activeSelf)
		{
			Close();
		}
	}

	private string GetCaiJiText(string desc, int itemId, int num, int addNum, int addTime)
	{
		return desc.Replace("{ItemName}", _ItemJsonData.DataDict[itemId].name).Replace("{ItemNum}", num.ToString()).Replace("{AddNum}", addNum.ToString())
			.Replace("{AddTime}", addTime.ToString());
	}
}
