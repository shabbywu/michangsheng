using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB;

[RequireComponent(typeof(RectTransform))]
[AddComponentMenu("UI/SymbolLabel")]
public class SymbolLabel : SymbolText
{
	[SerializeField]
	private int m_MaxElement = 30;

	public override string text
	{
		set
		{
			throw new NotSupportedException("text");
		}
	}

	public int MaxElement
	{
		get
		{
			return m_MaxElement;
		}
		set
		{
			m_MaxElement = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		m_textDirty = false;
	}

	protected override void SetTextDirty()
	{
	}

	public override void SetLayoutDirty()
	{
		base.SetLayoutDirty();
		((Graphic)this).SetMaterialDirty();
		SetRenderDirty();
		m_layoutDirty = true;
		m_textDirty = false;
	}

	public void Append(string text)
	{
		LinkedListNode<NodeBase> last = mNodeList.Last;
		int endl = 0;
		if (last != null)
		{
			endl = (int)last.Value.userdata;
			last = mNodeList.First;
			while (last != null && endl - (int)last.Value.userdata >= MaxElement)
			{
				last.Value.Release();
				mNodeList.RemoveFirst();
				last = mNodeList.First;
			}
		}
		int num = endl + 1;
		endl = num;
		SymbolText.s_nodebases.Clear();
		base.Parser.parser(this, text, CreateConfig(), SymbolText.s_nodebases);
		SymbolText.s_nodebases.ForEach(delegate(NodeBase nb)
		{
			nb.userdata = endl;
			mNodeList.AddLast(nb);
		});
		SymbolText.s_nodebases.back().setNewLine(line: true);
		SymbolText.s_nodebases.Clear();
		SetRenderDirty();
		((Graphic)this).SetLayoutDirty();
	}
}
