using System;
using System.Text;
using UnityEngine;

// Token: 0x0200011F RID: 287
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0000D500 File Offset: 0x0000B700
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0000D523 File Offset: 0x0000B723
	// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00091A44 File Offset: 0x0008FC44
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

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0000D52B File Offset: 0x0000B72B
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

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00091A94 File Offset: 0x0008FC94
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

	// Token: 0x06000B4A RID: 2890 RVA: 0x0000D559 File Offset: 0x0000B759
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00091AD4 File Offset: 0x0008FCD4
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

	// Token: 0x06000B4C RID: 2892 RVA: 0x00091B6C File Offset: 0x0008FD6C
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.Rebuild();
		}
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00091BD0 File Offset: 0x0008FDD0
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00091C04 File Offset: 0x0008FE04
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0000D56C File Offset: 0x0000B76C
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x0000D584 File Offset: 0x0000B784
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00091C3C File Offset: 0x0008FE3C
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

	// Token: 0x06000B52 RID: 2898 RVA: 0x00091C98 File Offset: 0x0008FE98
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

	// Token: 0x06000B53 RID: 2899 RVA: 0x00091DC4 File Offset: 0x0008FFC4
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

	// Token: 0x04000809 RID: 2057
	public UILabel textLabel;

	// Token: 0x0400080A RID: 2058
	public UIProgressBar scrollBar;

	// Token: 0x0400080B RID: 2059
	public UITextList.Style style;

	// Token: 0x0400080C RID: 2060
	public int paragraphHistory = 50;

	// Token: 0x0400080D RID: 2061
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x0400080E RID: 2062
	protected BetterList<UITextList.Paragraph> mParagraphs = new BetterList<UITextList.Paragraph>();

	// Token: 0x0400080F RID: 2063
	protected float mScroll;

	// Token: 0x04000810 RID: 2064
	protected int mTotalLines;

	// Token: 0x04000811 RID: 2065
	protected int mLastWidth;

	// Token: 0x04000812 RID: 2066
	protected int mLastHeight;

	// Token: 0x02000120 RID: 288
	public enum Style
	{
		// Token: 0x04000814 RID: 2068
		Text,
		// Token: 0x04000815 RID: 2069
		Chat
	}

	// Token: 0x02000121 RID: 289
	protected class Paragraph
	{
		// Token: 0x04000816 RID: 2070
		public string text;

		// Token: 0x04000817 RID: 2071
		public string[] lines;
	}
}
