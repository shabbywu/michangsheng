using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006B4 RID: 1716
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/SymbolLabel")]
	public class SymbolLabel : SymbolText
	{
		// Token: 0x06003612 RID: 13842 RVA: 0x00172DA9 File Offset: 0x00170FA9
		protected override void Awake()
		{
			base.Awake();
			this.m_textDirty = false;
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x00004095 File Offset: 0x00002295
		protected override void SetTextDirty()
		{
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x00172DB8 File Offset: 0x00170FB8
		public override void SetLayoutDirty()
		{
			base.SetLayoutDirty();
			this.SetMaterialDirty();
			base.SetRenderDirty();
			this.m_layoutDirty = true;
			this.m_textDirty = false;
		}

		// Token: 0x170004FD RID: 1277
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x00172DDA File Offset: 0x00170FDA
		public override string text
		{
			set
			{
				throw new NotSupportedException("text");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x00172DE6 File Offset: 0x00170FE6
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x00172DEE File Offset: 0x00170FEE
		public int MaxElement
		{
			get
			{
				return this.m_MaxElement;
			}
			set
			{
				this.m_MaxElement = value;
			}
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x00172DF8 File Offset: 0x00170FF8
		public void Append(string text)
		{
			LinkedListNode<NodeBase> linkedListNode = this.mNodeList.Last;
			int endl = 0;
			if (linkedListNode != null)
			{
				endl = (int)linkedListNode.Value.userdata;
				linkedListNode = this.mNodeList.First;
				while (linkedListNode != null && endl - (int)linkedListNode.Value.userdata >= this.MaxElement)
				{
					linkedListNode.Value.Release();
					this.mNodeList.RemoveFirst();
					linkedListNode = this.mNodeList.First;
				}
			}
			int endl2 = endl + 1;
			endl = endl2;
			SymbolText.s_nodebases.Clear();
			base.Parser.parser(this, text, base.CreateConfig(), SymbolText.s_nodebases);
			SymbolText.s_nodebases.ForEach(delegate(NodeBase nb)
			{
				nb.userdata = endl;
				this.mNodeList.AddLast(nb);
			});
			SymbolText.s_nodebases.back<NodeBase>().setNewLine(true);
			SymbolText.s_nodebases.Clear();
			base.SetRenderDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x04002F58 RID: 12120
		[SerializeField]
		private int m_MaxElement = 30;
	}
}
