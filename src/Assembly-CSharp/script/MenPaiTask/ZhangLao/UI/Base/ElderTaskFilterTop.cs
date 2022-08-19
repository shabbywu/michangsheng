using System;
using System.Collections;
using Bag;
using JiaoYi;

namespace script.MenPaiTask.ZhangLao.UI.Base
{
	// Token: 0x02000A13 RID: 2579
	public class ElderTaskFilterTop : JiaoYiFilterTop
	{
		// Token: 0x06004765 RID: 18277 RVA: 0x001E37AC File Offset: 0x001E19AC
		public override void CreateQuality()
		{
			Array values = Enum.GetValues(typeof(ItemQuality));
			int num = values.Length - 3;
			int num2 = 0;
			this.ChildUp.gameObject.Inst(this.Select.transform).GetComponent<BaseFilterTopChild>().Init(this.BaseBag2.ItemQuality.ToString(), delegate
			{
				this.Select.SetActive(false);
				this.Unselect.gameObject.SetActive(true);
			});
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ItemQuality itemQuality = (ItemQuality)enumerator.Current;
					BaseFilterTopChild component;
					if (num2 == num)
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
					if (num2 == num)
					{
						break;
					}
					num2++;
				}
			}
		}
	}
}
