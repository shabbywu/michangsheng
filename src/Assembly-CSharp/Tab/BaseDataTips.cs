using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab;

public class BaseDataTips : ITabTips
{
	public BaseDataTips(GameObject go)
	{
		_go = go;
		_rect = Get<RectTransform>("Bg");
		_sizeFitter = Get<ContentSizeFitter>("Bg");
		_childSizeFitter = Get<ContentSizeFitter>("Bg/Content");
		_text = Get<Text>("Bg/Content");
	}

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
