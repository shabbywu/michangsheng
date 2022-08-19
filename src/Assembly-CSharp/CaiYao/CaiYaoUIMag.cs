using System;
using DG.Tweening;
using JSONClass;
using QiYu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CaiYao
{
	// Token: 0x02000729 RID: 1833
	public class CaiYaoUIMag : MonoBehaviour
	{
		// Token: 0x06003A6C RID: 14956 RVA: 0x00191360 File Offset: 0x0018F560
		public void ShowAfterFight(ItemData data)
		{
			this.Init();
			this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc4, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
			Tools.instance.getPlayer().AddTime(data.AddTime, 0, 0);
			this.Item.SetItem(data.ItemId);
			this.Item.Count = data.ItemNum;
			Tools.instance.getPlayer().addItem(data.ItemId, data.ItemNum, Tools.CreateItemSeid(data.ItemId), false);
			this.OkBtn.SetActive(true);
			this.OptionList.gameObject.SetActive(false);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x0019142C File Offset: 0x0018F62C
		public void ShowNomal()
		{
			this.Init();
			ItemData data = this.GetRandomYaoCai();
			this.Item.SetItem(data.ItemId);
			this.Item.Count = data.ItemNum;
			if (data.HasEnemy)
			{
				this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc2, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
				this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init("转身离开", new UnityAction(this.Close));
				this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init("出手抢夺", delegate
				{
					data.AddTime = 0;
					Tools.instance.SetCaiYaoData(data);
					Tools.instance.startNomalFight(data.FirstEnemyId);
					this.Close();
				});
				return;
			}
			this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc1, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
			UnityAction <>9__5;
			UnityAction <>9__6;
			UnityAction <>9__3;
			UnityAction <>9__4;
			this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init("继续等待", delegate
			{
				this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc5, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
				Tools.ClearObj(this.Option.transform);
				QiYuOption component = this.Option.Inst(this.OptionList).GetComponent<QiYuOption>();
				string name = "一旁潜伏";
				UnityAction action;
				if ((action = <>9__3) == null)
				{
					action = (<>9__3 = delegate()
					{
						this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc6, data.ItemId, data.ItemNum, data.ItemNum, data.AddTime);
						Tools.ClearObj(this.Option.transform);
						QiYuOption component3 = this.Option.Inst(this.OptionList).GetComponent<QiYuOption>();
						string name3 = "果断出手";
						UnityAction action3;
						if ((action3 = <>9__5) == null)
						{
							action3 = (<>9__5 = delegate()
							{
								data.ItemNum += data.AddNum;
								Tools.instance.SetCaiYaoData(data);
								Tools.instance.startNomalFight(data.ScondEnemyId);
								this.Close();
							});
						}
						component3.Init(name3, action3);
						QiYuOption component4 = this.Option.Inst(this.OptionList).GetComponent<QiYuOption>();
						string name4 = "还是算了";
						UnityAction action4;
						if ((action4 = <>9__6) == null)
						{
							action4 = (<>9__6 = delegate()
							{
								this.Close();
							});
						}
						component4.Init(name4, action4);
					});
				}
				component.Init(name, action);
				QiYuOption component2 = this.Option.Inst(this.OptionList).GetComponent<QiYuOption>();
				string name2 = "布阵等待";
				UnityAction action2;
				if ((action2 = <>9__4) == null)
				{
					action2 = (<>9__4 = delegate()
					{
						this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc7, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
						Tools.instance.getPlayer().AddTime(data.AddTime, 0, 0);
						this.Item.Count = data.ItemNum + data.AddNum;
						Tools.instance.getPlayer().addItem(data.ItemId, this.Item.Count, Tools.CreateItemSeid(data.ItemId), false);
						this.OkBtn.SetActive(true);
						this.OptionList.gameObject.SetActive(false);
					});
				}
				component2.Init(name2, action2);
			});
			this.Option.Inst(this.OptionList).GetComponent<QiYuOption>().Init("立刻采集", delegate
			{
				this.Desc.text = this.GetCaiJiText(AllMapCaiJiMiaoShuBiao.DataDict[1].desc3, data.ItemId, data.ItemNum, data.AddNum, data.AddTime);
				Tools.instance.getPlayer().addItem(data.ItemId, data.ItemNum, Tools.CreateItemSeid(data.ItemId), false);
				this.OkBtn.SetActive(true);
				this.OptionList.gameObject.SetActive(false);
			});
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x001915E0 File Offset: 0x0018F7E0
		private void Init()
		{
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			this.Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.Panel, Vector3.one, 0.5f);
			PanelMamager.CanOpenOrClose = false;
			Tools.canClickFlag = false;
			CaiYaoUIMag.Inst = this;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0019167C File Offset: 0x0018F87C
		private ItemData GetRandomYaoCai()
		{
			int id = AllMapCaiJiBiao.DataList[NpcJieSuanManager.inst.getRandomInt(0, AllMapCaiJiBiao.DataList.Count - 1)].ID;
			int item = AllMapCaiJiBiao.DataDict[id].Item;
			int randomInt = NpcJieSuanManager.inst.getRandomInt(AllMapCaiJiBiao.DataDict[id].Num[0], AllMapCaiJiBiao.DataDict[id].Num[1]);
			int quality = _ItemJsonData.DataDict[item].quality;
			int num = 1 + randomInt * (AllMapCaiJiAddItemBiao.DataDict[quality].percent / 100);
			int addTime = num * AllMapCaiJiAddItemBiao.DataDict[quality].time;
			bool hasEnemy = false;
			int firstEnemyId = 0;
			if (AllMapCaiJiBiao.DataDict[id].percent >= NpcJieSuanManager.inst.getRandomInt(0, 100))
			{
				firstEnemyId = AllMapCaiJiBiao.DataDict[id].Monstar;
				hasEnemy = true;
			}
			int maiFuMonstar = AllMapCaiJiBiao.DataDict[id].MaiFuMonstar;
			return new ItemData(item, randomInt, num, addTime, hasEnemy, firstEnemyId, maiFuMonstar);
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x00191794 File Offset: 0x0018F994
		private void OnDestroy()
		{
			CaiYaoUIMag.Inst = null;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x0019179C File Offset: 0x0018F99C
		public void Close()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x001917B5 File Offset: 0x0018F9B5
		private void Update()
		{
			if (Input.GetKeyUp(27) && this.OkBtn.activeSelf)
			{
				this.Close();
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x001917D4 File Offset: 0x0018F9D4
		private string GetCaiJiText(string desc, int itemId, int num, int addNum, int addTime)
		{
			return desc.Replace("{ItemName}", _ItemJsonData.DataDict[itemId].name).Replace("{ItemNum}", num.ToString()).Replace("{AddNum}", addNum.ToString()).Replace("{AddTime}", addTime.ToString());
		}

		// Token: 0x04003291 RID: 12945
		[SerializeField]
		private Transform Panel;

		// Token: 0x04003292 RID: 12946
		[SerializeField]
		private GameObject OkBtn;

		// Token: 0x04003293 RID: 12947
		[SerializeField]
		private UIIconShow Item;

		// Token: 0x04003294 RID: 12948
		[SerializeField]
		private Text Desc;

		// Token: 0x04003295 RID: 12949
		[SerializeField]
		private Transform OptionList;

		// Token: 0x04003296 RID: 12950
		[SerializeField]
		private GameObject Option;

		// Token: 0x04003297 RID: 12951
		public static CaiYaoUIMag Inst;
	}
}
