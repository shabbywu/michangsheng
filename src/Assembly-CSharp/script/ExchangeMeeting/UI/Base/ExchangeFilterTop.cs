using System;
using Bag;
using JiaoYi;

namespace script.ExchangeMeeting.UI.Base
{
	// Token: 0x02000A36 RID: 2614
	public class ExchangeFilterTop : JiaoYiFilterTop
	{
		// Token: 0x060047EC RID: 18412 RVA: 0x001E61B4 File Offset: 0x001E43B4
		public override void CreateQuality()
		{
			Array values = Enum.GetValues(typeof(ItemQuality));
			int length = values.Length;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.ItemQuality.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			for (int i = 0; i < length; i++)
			{
				if (i <= 0 || i >= 5)
				{
					ItemQuality itemQuality = (ItemQuality)values.GetValue(i);
					BaseFilterTopChild component;
					if (i == length)
					{
						component = this.ChildDown.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					else
					{
						component = this.ChildCenter.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>();
					}
					component.Init(itemQuality.ToString(), delegate
					{
						this.BaseBag2.ItemQuality = itemQuality;
						this.CurType.SetText((this.BaseBag2.ItemQuality == ItemQuality.全部) ? "品质" : itemQuality.ToString());
						this.BaseBag2.UpdateItem(false);
						this.Select.SetActive(false);
						this.Unselect.gameObject.SetActive(true);
					});
				}
			}
		}
	}
}
