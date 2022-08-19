using System;
using UnityEngine;
using WXB;

namespace YSGame.TuJian
{
	// Token: 0x02000A9E RID: 2718
	public class TuJianHyperlinkEvent : MonoBehaviour
	{
		// Token: 0x06004C21 RID: 19489 RVA: 0x00208324 File Offset: 0x00206524
		public void OnHyperlinkClick(NodeBase node)
		{
			if (node is HyperlinkNode)
			{
				HyperlinkNode hyperlinkNode = node as HyperlinkNode;
				Debug.Log("点击了超链接，文本:" + hyperlinkNode.d_text + "，链接信息:" + hyperlinkNode.d_link);
				if (!string.IsNullOrWhiteSpace(hyperlinkNode.d_link))
				{
					if (hyperlinkNode.d_link.StartsWith("Map"))
					{
						int num = int.Parse(hyperlinkNode.d_link.Replace("Map", ""));
						if (num > 0)
						{
							UIMapPanel.Inst.CloseAction = delegate()
							{
								TuJianManager.Inst.OpenTuJian();
								UIMapPanel.Inst.CloseAction = null;
							};
							UIMapPanel.Inst.OpenHighlight(num);
							return;
						}
					}
					else
					{
						TuJianManager.Inst.OnHyperlink(hyperlinkNode.d_link);
					}
				}
			}
		}
	}
}
