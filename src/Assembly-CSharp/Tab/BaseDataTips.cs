using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006EA RID: 1770
	public class BaseDataTips : ITabTips
	{
		// Token: 0x06003904 RID: 14596 RVA: 0x00185520 File Offset: 0x00183720
		public BaseDataTips(GameObject go)
		{
			this._go = go;
			this._rect = base.Get<RectTransform>("Bg");
			this._sizeFitter = base.Get<ContentSizeFitter>("Bg");
			this._childSizeFitter = base.Get<ContentSizeFitter>("Bg/Content");
			this._text = base.Get<Text>("Bg/Content");
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x00185580 File Offset: 0x00183780
		protected override string Replace(string msg)
		{
			if (msg.Contains("[24a5d6]"))
			{
				msg = msg.Replace("[24a5d6]", "<color=#24a5d6>");
				msg = msg.Replace("[-]", "</color>");
			}
			if (msg.Contains("{LunDao}"))
			{
				msg = msg.Replace("{LunDao}", LunDaoStateData.DataDict[Tools.instance.getPlayer().LunDaoState].MiaoShu + "\n");
			}
			if (msg.Contains("{DanDu}"))
			{
				msg = msg.Replace("{DanDu}", DanduMiaoShu.DataDict[Tools.instance.getPlayer().GetDanDuLevel() + 1].desc);
			}
			return msg;
		}
	}
}
