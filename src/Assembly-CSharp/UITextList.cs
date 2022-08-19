using System;
using System.Text;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003F371 File Offset: 0x0003D571
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0003F394 File Offset: 0x0003D594
	// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0003F39C File Offset: 0x0003D59C
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (this.isValid && this.mScroll != value)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.value = value;
					return;
				}
				this.mScroll = value;
				this.UpdateVisibleText();
			}
		}
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0003F3EA File Offset: 0x0003D5EA
	protected float lineHeight
	{
		get
		{
			if (!(this.textLabel != null))
			{
				return 20f;
			}
			return (float)(this.textLabel.fontSize + this.textLabel.spacingY);
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0003F418 File Offset: 0x0003D618
	protected int scrollHeight
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			return Mathf.Max(0, this.mTotalLines - num);
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0003F456 File Offset: 0x0003D656
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0003F46C File Offset: 0x0003D66C
	private void Start()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.scrollBar != null)
		{
			EventDelegate.Add(this.scrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
		}
		this.textLabel.overflowMethod = UILabel.Overflow.ClampContent;
		if (this.style == UITextList.Style.Chat)
		{
			this.textLabel.pivot = UIWidget.Pivot.BottomLeft;
			this.scrollValue = 1f;
			return;
		}
		this.textLabel.pivot = UIWidget.Pivot.TopLeft;
		this.scrollValue = 0f;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0003F504 File Offset: 0x0003D704
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.Rebuild();
		}
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0003F568 File Offset: 0x0003D768
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0003F59C File Offset: 0x0003D79C
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0003F5D2 File Offset: 0x0003D7D2
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003F5EA File Offset: 0x0003D7EA
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.size < this.paragraphHistory)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		this.Rebuild();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0003F650 File Offset: 0x0003D850
	protected void Rebuild()
	{
		if (this.isValid)
		{
			this.textLabel.UpdateNGUIText();
			NGUIText.rectHeight = 1000000;
			this.mTotalLines = 0;
			for (int i = 0; i < this.mParagraphs.size; i++)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[i];
				string text;
				NGUIText.WrapText(paragraph.text, out text);
				paragraph.lines = text.Split(new char[]
				{
					'\n'
				});
				this.mTotalLines += paragraph.lines.Length;
			}
			this.mTotalLines = 0;
			int j = 0;
			int size = this.mParagraphs.size;
			while (j < size)
			{
				this.mTotalLines += this.mParagraphs.buffer[j].lines.Length;
				j++;
			}
			if (this.scrollBar != null)
			{
				UIScrollBar uiscrollBar = this.scrollBar as UIScrollBar;
				if (uiscrollBar != null)
				{
					uiscrollBar.barSize = ((this.mTotalLines == 0) ? 1f : (1f - (float)this.scrollHeight / (float)this.mTotalLines));
				}
			}
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0003F77C File Offset: 0x0003D97C
	protected void UpdateVisibleText()
	{
		if (this.isValid)
		{
			if (this.mTotalLines == 0)
			{
				this.textLabel.text = "";
				return;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			int num2 = Mathf.Max(0, this.mTotalLines - num);
			int num3 = Mathf.RoundToInt(this.mScroll * (float)num2);
			if (num3 < 0)
			{
				num3 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			int size = this.mParagraphs.size;
			while (num > 0 && num4 < size)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[num4];
				int num5 = 0;
				int num6 = paragraph.lines.Length;
				while (num > 0 && num5 < num6)
				{
					string value = paragraph.lines[num5];
					if (num3 > 0)
					{
						num3--;
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append("\n");
						}
						stringBuilder.Append(value);
						num--;
					}
					num5++;
				}
				num4++;
			}
			this.textLabel.text = stringBuilder.ToString();
		}
	}

	// Token: 0x0400066A RID: 1642
	public UILabel textLabel;

	// Token: 0x0400066B RID: 1643
	public UIProgressBar scrollBar;

	// Token: 0x0400066C RID: 1644
	public UITextList.Style style;

	// Token: 0x0400066D RID: 1645
	public int paragraphHistory = 50;

	// Token: 0x0400066E RID: 1646
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x0400066F RID: 1647
	protected BetterList<UITextList.Paragraph> mParagraphs = new BetterList<UITextList.Paragraph>();

	// Token: 0x04000670 RID: 1648
	protected float mScroll;

	// Token: 0x04000671 RID: 1649
	protected int mTotalLines;

	// Token: 0x04000672 RID: 1650
	protected int mLastWidth;

	// Token: 0x04000673 RID: 1651
	protected int mLastHeight;

	// Token: 0x02001231 RID: 4657
	public enum Style
	{
		// Token: 0x040064EF RID: 25839
		Text,
		// Token: 0x040064F0 RID: 25840
		Chat
	}

	// Token: 0x02001232 RID: 4658
	protected class Paragraph
	{
		// Token: 0x040064F1 RID: 25841
		public string text;

		// Token: 0x040064F2 RID: 25842
		public string[] lines;
	}
}
