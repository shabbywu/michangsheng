using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace PingJing
{
	// Token: 0x02000723 RID: 1827
	public class PingJingUIMag : MonoBehaviour
	{
		// Token: 0x06003A4F RID: 14927 RVA: 0x0019088C File Offset: 0x0018EA8C
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

		// Token: 0x06003A50 RID: 14928 RVA: 0x00190951 File Offset: 0x0018EB51
		public void Close()
		{
			Tools.canClickFlag = true;
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x00190964 File Offset: 0x0018EB64
		private void Update()
		{
			if (Input.GetKeyUp(27))
			{
				this.Close();
			}
		}

		// Token: 0x0400327D RID: 12925
		[SerializeField]
		private Text Desc;

		// Token: 0x0400327E RID: 12926
		[SerializeField]
		private Transform Panel;
	}
}
