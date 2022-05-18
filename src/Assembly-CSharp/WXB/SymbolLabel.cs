using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009D8 RID: 2520
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/SymbolLabel")]
	public class SymbolLabel : SymbolText
	{
		// Token: 0x06004014 RID: 16404 RVA: 0x0002E003 File Offset: 0x0002C203
		protected override void Awake()
		{
			base.Awake();
			this.m_textDirty = false;
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x000042DD File Offset: 0x000024DD
		protected override void SetTextDirty()
		{
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0002E012 File Offset: 0x0002C212
		public override void SetLayoutDirty()
		{
			base.SetLayoutDirty();
			this.SetMaterialDirty();
			base.SetRenderDirty();
			this.m_layoutDirty = true;
			this.m_textDirty = false;
		}

		// Token: 0x17000728 RID: 1832
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x0002E034 File Offset: 0x0002C234
		public override string text
		{
			set
			{
				throw new NotSupportedException("text");
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x0002E040 File Offset: 0x0002C240
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x0002E048 File Offset: 0x0002C248
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

		// Token: 0x0600401A RID: 16410 RVA: 0x001BB9DC File Offset: 0x001B9BDC
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

		// Token: 0x04003946 RID: 14662
		[SerializeField]
		private int m_MaxElement = 30;
	}
}
