using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001165 RID: 4453
	internal class Instruction
	{
		// Token: 0x06006C1C RID: 27676 RVA: 0x00049D29 File Offset: 0x00047F29
		internal Instruction(SourceRef sourceref)
		{
			this.SourceCodeRef = sourceref;
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x00294F40 File Offset: 0x00293140
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

		// Token: 0x06006C1E RID: 27678 RVA: 0x00049D38 File Offset: 0x00047F38
		private string PurifyFromNewLines(DynValue Value)
		{
			if (Value == null)
			{
				return "";
			}
			return Value.ToString().Replace('\n', ' ').Replace('\r', ' ');
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x00049D5B File Offset: 0x00047F5B
		private string GenSpaces()
		{
			return new string(' ', 10 - this.OpCode.ToString().Length);
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x00295090 File Offset: 0x00293290
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

		// Token: 0x06006C21 RID: 27681 RVA: 0x0029516C File Offset: 0x0029336C
		private static void WriteSymbol(BinaryWriter wr, SymbolRef symbolRef, Dictionary<SymbolRef, int> symbolMap)
		{
			int value = (symbolRef == null) ? -1 : symbolMap[symbolRef];
			wr.Write(value);
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x00295190 File Offset: 0x00293390
		private static SymbolRef ReadSymbol(BinaryReader rd, SymbolRef[] deserializedSymbols)
		{
			int num = rd.ReadInt32();
			if (num < 0)
			{
				return null;
			}
			return deserializedSymbols[num];
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x002951B0 File Offset: 0x002933B0
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

		// Token: 0x06006C24 RID: 27684 RVA: 0x00295290 File Offset: 0x00293490
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

		// Token: 0x06006C25 RID: 27685 RVA: 0x00295324 File Offset: 0x00293524
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

		// Token: 0x06006C26 RID: 27686 RVA: 0x00049D7D File Offset: 0x00047F7D
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

		// Token: 0x04006159 RID: 24921
		internal OpCode OpCode;

		// Token: 0x0400615A RID: 24922
		internal SymbolRef Symbol;

		// Token: 0x0400615B RID: 24923
		internal SymbolRef[] SymbolList;

		// Token: 0x0400615C RID: 24924
		internal string Name;

		// Token: 0x0400615D RID: 24925
		internal DynValue Value;

		// Token: 0x0400615E RID: 24926
		internal int NumVal;

		// Token: 0x0400615F RID: 24927
		internal int NumVal2;

		// Token: 0x04006160 RID: 24928
		internal SourceRef SourceCodeRef;
	}
}
