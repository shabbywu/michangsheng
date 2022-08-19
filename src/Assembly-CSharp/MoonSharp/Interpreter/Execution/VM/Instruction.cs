using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D55 RID: 3413
	internal class Instruction
	{
		// Token: 0x06006033 RID: 24627 RVA: 0x0026DB17 File Offset: 0x0026BD17
		internal Instruction(SourceRef sourceref)
		{
			this.SourceCodeRef = sourceref;
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x0026DB28 File Offset: 0x0026BD28
		public override string ToString()
		{
			string text = this.OpCode.ToString().ToUpperInvariant();
			int fieldUsage = (int)this.OpCode.GetFieldUsage();
			if (fieldUsage != 0)
			{
				text += this.GenSpaces();
			}
			if (this.OpCode == OpCode.Meta || (fieldUsage & 32784) == 32784)
			{
				text = text + " " + this.NumVal.ToString("X8");
			}
			else if ((fieldUsage & 16) != 0)
			{
				text = text + " " + this.NumVal.ToString();
			}
			if ((fieldUsage & 32) != 0)
			{
				text = text + " " + this.NumVal2.ToString();
			}
			if ((fieldUsage & 4) != 0)
			{
				text = text + " " + this.Name;
			}
			if ((fieldUsage & 8) != 0)
			{
				text = text + " " + this.PurifyFromNewLines(this.Value);
			}
			if ((fieldUsage & 1) != 0)
			{
				text = text + " " + this.Symbol;
			}
			if ((fieldUsage & 2) != 0 && this.SymbolList != null)
			{
				text = text + " " + string.Join(",", (from s in this.SymbolList
				select s.ToString()).ToArray<string>());
			}
			return text;
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x0026DC76 File Offset: 0x0026BE76
		private string PurifyFromNewLines(DynValue Value)
		{
			if (Value == null)
			{
				return "";
			}
			return Value.ToString().Replace('\n', ' ').Replace('\r', ' ');
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x0026DC99 File Offset: 0x0026BE99
		private string GenSpaces()
		{
			return new string(' ', 10 - this.OpCode.ToString().Length);
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x0026DCBC File Offset: 0x0026BEBC
		internal void WriteBinary(BinaryWriter wr, int baseAddress, Dictionary<SymbolRef, int> symbolMap)
		{
			wr.Write((byte)this.OpCode);
			int fieldUsage = (int)this.OpCode.GetFieldUsage();
			if ((fieldUsage & 32784) == 32784)
			{
				wr.Write(this.NumVal - baseAddress);
			}
			else if ((fieldUsage & 16) != 0)
			{
				wr.Write(this.NumVal);
			}
			if ((fieldUsage & 32) != 0)
			{
				wr.Write(this.NumVal2);
			}
			if ((fieldUsage & 4) != 0)
			{
				wr.Write(this.Name ?? "");
			}
			if ((fieldUsage & 8) != 0)
			{
				this.DumpValue(wr, this.Value);
			}
			if ((fieldUsage & 1) != 0)
			{
				Instruction.WriteSymbol(wr, this.Symbol, symbolMap);
			}
			if ((fieldUsage & 2) != 0)
			{
				wr.Write(this.SymbolList.Length);
				for (int i = 0; i < this.SymbolList.Length; i++)
				{
					Instruction.WriteSymbol(wr, this.SymbolList[i], symbolMap);
				}
			}
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x0026DD98 File Offset: 0x0026BF98
		private static void WriteSymbol(BinaryWriter wr, SymbolRef symbolRef, Dictionary<SymbolRef, int> symbolMap)
		{
			int value = (symbolRef == null) ? -1 : symbolMap[symbolRef];
			wr.Write(value);
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0026DDBC File Offset: 0x0026BFBC
		private static SymbolRef ReadSymbol(BinaryReader rd, SymbolRef[] deserializedSymbols)
		{
			int num = rd.ReadInt32();
			if (num < 0)
			{
				return null;
			}
			return deserializedSymbols[num];
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x0026DDDC File Offset: 0x0026BFDC
		internal static Instruction ReadBinary(SourceRef chunkRef, BinaryReader rd, int baseAddress, Table envTable, SymbolRef[] deserializedSymbols)
		{
			Instruction instruction = new Instruction(chunkRef);
			instruction.OpCode = (OpCode)rd.ReadByte();
			int fieldUsage = (int)instruction.OpCode.GetFieldUsage();
			if ((fieldUsage & 32784) == 32784)
			{
				instruction.NumVal = rd.ReadInt32() + baseAddress;
			}
			else if ((fieldUsage & 16) != 0)
			{
				instruction.NumVal = rd.ReadInt32();
			}
			if ((fieldUsage & 32) != 0)
			{
				instruction.NumVal2 = rd.ReadInt32();
			}
			if ((fieldUsage & 4) != 0)
			{
				instruction.Name = rd.ReadString();
			}
			if ((fieldUsage & 8) != 0)
			{
				instruction.Value = Instruction.ReadValue(rd, envTable);
			}
			if ((fieldUsage & 1) != 0)
			{
				instruction.Symbol = Instruction.ReadSymbol(rd, deserializedSymbols);
			}
			if ((fieldUsage & 2) != 0)
			{
				int num = rd.ReadInt32();
				instruction.SymbolList = new SymbolRef[num];
				for (int i = 0; i < instruction.SymbolList.Length; i++)
				{
					instruction.SymbolList[i] = Instruction.ReadSymbol(rd, deserializedSymbols);
				}
			}
			return instruction;
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x0026DEBC File Offset: 0x0026C0BC
		private static DynValue ReadValue(BinaryReader rd, Table envTable)
		{
			if (!rd.ReadBoolean())
			{
				return null;
			}
			DataType dataType = (DataType)rd.ReadByte();
			switch (dataType)
			{
			case DataType.Nil:
				return DynValue.NewNil();
			case DataType.Void:
				return DynValue.Void;
			case DataType.Boolean:
				return DynValue.NewBoolean(rd.ReadBoolean());
			case DataType.Number:
				return DynValue.NewNumber(rd.ReadDouble());
			case DataType.String:
				return DynValue.NewString(rd.ReadString());
			case DataType.Table:
				return DynValue.NewTable(envTable);
			}
			throw new NotSupportedException(string.Format("Unsupported type in chunk dump : {0}", dataType));
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x0026DF50 File Offset: 0x0026C150
		private void DumpValue(BinaryWriter wr, DynValue value)
		{
			if (value == null)
			{
				wr.Write(false);
				return;
			}
			wr.Write(true);
			wr.Write((byte)value.Type);
			switch (value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
			case DataType.Table:
				return;
			case DataType.Boolean:
				wr.Write(value.Boolean);
				return;
			case DataType.Number:
				wr.Write(value.Number);
				return;
			case DataType.String:
				wr.Write(value.String);
				return;
			}
			throw new NotSupportedException(string.Format("Unsupported type in chunk dump : {0}", value.Type));
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x0026DFE9 File Offset: 0x0026C1E9
		internal void GetSymbolReferences(out SymbolRef[] symbolList, out SymbolRef symbol)
		{
			InstructionFieldUsage fieldUsage = this.OpCode.GetFieldUsage();
			symbol = null;
			symbolList = null;
			if ((fieldUsage & InstructionFieldUsage.Symbol) != InstructionFieldUsage.None)
			{
				symbol = this.Symbol;
			}
			if ((fieldUsage & InstructionFieldUsage.SymbolList) != InstructionFieldUsage.None)
			{
				symbolList = this.SymbolList;
			}
		}

		// Token: 0x040054DD RID: 21725
		internal OpCode OpCode;

		// Token: 0x040054DE RID: 21726
		internal SymbolRef Symbol;

		// Token: 0x040054DF RID: 21727
		internal SymbolRef[] SymbolList;

		// Token: 0x040054E0 RID: 21728
		internal string Name;

		// Token: 0x040054E1 RID: 21729
		internal DynValue Value;

		// Token: 0x040054E2 RID: 21730
		internal int NumVal;

		// Token: 0x040054E3 RID: 21731
		internal int NumVal2;

		// Token: 0x040054E4 RID: 21732
		internal SourceRef SourceCodeRef;
	}
}
