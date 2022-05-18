using System;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020011A9 RID: 4521
	internal class BinaryEncoding : Encoding
	{
		// Token: 0x06006EA6 RID: 28326 RVA: 0x0004B51E File Offset: 0x0004971E
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x0029F3F4 File Offset: 0x0029D5F4
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				bytes[byteIndex + i] = (byte)chars[charIndex + i];
			}
			return charCount;
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x0004B51E File Offset: 0x0004971E
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x0029F41C File Offset: 0x0029D61C
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				chars[charIndex + i] = (char)bytes[byteIndex + i];
			}
			return byteCount;
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
