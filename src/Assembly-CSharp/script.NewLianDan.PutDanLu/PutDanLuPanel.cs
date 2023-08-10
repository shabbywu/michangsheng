using UnityEngine;
using UnityEngine.Events;
using script.NewLianDan.Base;

namespace script.NewLianDan.PutDanLu;

public class PutDanLuPanel : BasePanel
{
	public PutDanLuPanel(GameObject go)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		_go = go;
		Get<FpBtn>("放入按钮").mouseUpEvent.AddListener(new UnityAction(LianDanUIMag.Instance.DanLuBag.Open));
	}
}
