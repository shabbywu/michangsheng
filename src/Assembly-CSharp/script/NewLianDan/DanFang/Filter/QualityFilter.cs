using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang.Filter
{
	// Token: 0x02000AD2 RID: 2770
	public class QualityFilter : UIBase
	{
		// Token: 0x060046B1 RID: 18097 RVA: 0x001E41EC File Offset: 0x001E23EC
		public QualityFilter(GameObject go, string name, int value, float x, float y)
		{
			this._go = go;
			this._go.name = name;
			this.Value = value;
			this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(delegate()
			{
				UnityAction<int> action = this.Action;
				if (action == null)
				{
					return;
				}
				action.Invoke(this.Value);
			});
			base.Get<Text>("Value").SetText(name);
			this._go.transform.localPosition = new Vector2(x, y);
			this._go.SetActive(true);
		}

		// Token: 0x04003ECC RID: 16076
		public int Value;

		// Token: 0x04003ECD RID: 16077
		public UnityAction<int> Action;
	}
}
