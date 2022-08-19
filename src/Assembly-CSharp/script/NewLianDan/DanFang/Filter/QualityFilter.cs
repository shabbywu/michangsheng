using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.NewLianDan.DanFang.Filter
{
	// Token: 0x02000A03 RID: 2563
	public class QualityFilter : UIBase
	{
		// Token: 0x06004701 RID: 18177 RVA: 0x001E1DB0 File Offset: 0x001DFFB0
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

		// Token: 0x0400484D RID: 18509
		public int Value;

		// Token: 0x0400484E RID: 18510
		public UnityAction<int> Action;
	}
}
