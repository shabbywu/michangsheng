using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using WXB;

namespace YSGame.TuJian;

public class TuJianHyperlinkEvent : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__0_0;

		internal void _003COnHyperlinkClick_003Eb__0_0()
		{
			TuJianManager.Inst.OpenTuJian();
			UIMapPanel.Inst.CloseAction = null;
		}
	}

	public void OnHyperlinkClick(NodeBase node)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		if (!(node is HyperlinkNode))
		{
			return;
		}
		HyperlinkNode hyperlinkNode = node as HyperlinkNode;
		Debug.Log((object)("点击了超链接，文本:" + hyperlinkNode.d_text + "，链接信息:" + hyperlinkNode.d_link));
		if (string.IsNullOrWhiteSpace(hyperlinkNode.d_link))
		{
			return;
		}
		if (hyperlinkNode.d_link.StartsWith("Map"))
		{
			int num = int.Parse(hyperlinkNode.d_link.Replace("Map", ""));
			if (num <= 0)
			{
				return;
			}
			UIMapPanel inst = UIMapPanel.Inst;
			object obj = _003C_003Ec._003C_003E9__0_0;
			if (obj == null)
			{
				UnityAction val = delegate
				{
					TuJianManager.Inst.OpenTuJian();
					UIMapPanel.Inst.CloseAction = null;
				};
				_003C_003Ec._003C_003E9__0_0 = val;
				obj = (object)val;
			}
			inst.CloseAction = (UnityAction)obj;
			UIMapPanel.Inst.OpenHighlight(num);
		}
		else
		{
			TuJianManager.Inst.OnHyperlink(hyperlinkNode.d_link);
		}
	}
}
