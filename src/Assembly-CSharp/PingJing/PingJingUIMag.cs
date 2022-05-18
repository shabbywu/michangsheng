using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace PingJing
{
	// Token: 0x02000A7A RID: 2682
	public class PingJingUIMag : MonoBehaviour
	{
		// Token: 0x060044F2 RID: 17650 RVA: 0x001D7EE8 File Offset: 0x001D60E8
		public void Show()
		{
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			Tools.canClickFlag = false;
			this.Desc.text = "\u3000\u3000体内真元逐渐饱和，你的修为已经达到了<color=#ffface>" + LevelUpDataJsonData.DataDict[(int)Tools.instance.getPlayer().level].Name + "</color>的瓶颈，如果无法突破，就再难提升了";
			this.Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.Panel, Vector3.one, 0.5f);
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00031551 File Offset: 0x0002F751
		public void Close()
		{
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x00031564 File Offset: 0x0002F764
		private void Update()
		{
			if (Input.GetKeyUp(27))
			{
				this.Close();
			}
		}

		// Token: 0x04003D18 RID: 15640
		[SerializeField]
		private Text Desc;

		// Token: 0x04003D19 RID: 15641
		[SerializeField]
		private Transform Panel;
	}
}
