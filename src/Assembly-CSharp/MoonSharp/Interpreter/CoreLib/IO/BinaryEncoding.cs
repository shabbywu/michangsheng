using System;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x02000D87 RID: 3463
	internal class BinaryEncoding : Encoding
	{
		// Token: 0x06006278 RID: 25208 RVA: 0x00279422 File Offset: 0x00277622
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x00279428 File Offset: 0x00277628
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				bytes[byteIndex + i] = (byte)chars[charIndex + i];
			}
			return charCount;
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x00279422 File Offset: 0x00277622
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x00279450 File Offset: 0x00277650
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				chars[charIndex + i] = (char)bytes[byteIndex + i];
			}
			return byteCount;
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x001086F1 File Offset: 0x001068F1
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x001086F1 File Offset: 0x001068F1
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
