using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B36 RID: 2870
	public abstract class BuildBase : Entity
	{
		// Token: 0x06005076 RID: 20598 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onBuildIdChanged(int oldValue)
		{
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0021FA04 File Offset: 0x0021DC04
		public BuildBase()
		{
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0021FA1F File Offset: 0x0021DC1F
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_BuildBase(this.id, this.className);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0021FA38 File Offset: 0x0021DC38
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_BuildBase(this.id, this.className);
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0021FA51 File Offset: 0x0021DC51
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0021FA5A File Offset: 0x0021DC5A
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0021FA62 File Offset: 0x0021DC62
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x0021FA6C File Offset: 0x0021DC6C
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Build"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x0021FAEC File Offset: 0x0021DCEC
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Build"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 102)
				{
					if (properUtype2 != 97)
					{
						if (properUtype2 == 102)
						{
							uint oldValue = this.dialogID;
							this.dialogID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onDialogIDChanged(oldValue);
								}
							}
							else if (this.inWorld)
							{
								this.onDialogIDChanged(oldValue);
							}
						}
					}
					else
					{
						int buildId = this.BuildId;
						this.BuildId = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onBuildIdChanged(buildId);
							}
						}
						else if (this.inWorld)
						{
							this.onBuildIdChanged(buildId);
						}
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 40000:
					{
						Vector3 position = this.position;
						this.position = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onPositionChanged(position);
							}
						}
						else if (this.inWorld)
						{
							this.onPositionChanged(position);
						}
						break;
					}
					case 40001:
					{
						Vector3 direction = this.direction;
						this.direction = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDirectionChanged(direction);
							}
						}
						else if (this.inWorld)
						{
							this.onDirectionChanged(direction);
						}
						break;
					}
					case 40002:
						stream.readUint32();
						break;
					default:
						switch (properUtype2)
						{
						case 41003:
						{
							string oldValue2 = this.name;
							this.name = stream.readUnicode();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onNameChanged(oldValue2);
								}
							}
							else if (this.inWorld)
							{
								this.onNameChanged(oldValue2);
							}
							break;
						}
						case 41004:
						{
							uint oldValue3 = this.uid;
							this.uid = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUidChanged(oldValue3);
								}
							}
							else if (this.inWorld)
							{
								this.onUidChanged(oldValue3);
							}
							break;
						}
						case 41005:
						{
							uint oldValue4 = this.utype;
							this.utype = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUtypeChanged(oldValue4);
								}
							}
							else if (this.inWorld)
							{
								this.onUtypeChanged(oldValue4);
							}
							break;
						}
						case 41006:
						{
							uint oldValue5 = this.modelID;
							this.modelID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelIDChanged(oldValue5);
								}
							}
							else if (this.inWorld)
							{
								this.onModelIDChanged(oldValue5);
							}
							break;
						}
						case 41007:
						{
							byte oldValue6 = this.modelScale;
							this.modelScale = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelScaleChanged(oldValue6);
								}
							}
							else if (this.inWorld)
							{
								this.onModelScaleChanged(oldValue6);
							}
							break;
						}
						default:
							if (properUtype2 == 51007)
							{
								uint oldValue7 = this.entityNO;
								this.entityNO = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onEntityNOChanged(oldValue7);
									}
								}
								else if (this.inWorld)
								{
									this.onEntityNOChanged(oldValue7);
								}
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x0021FECC File Offset: 0x0021E0CC
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Build"].idpropertys;
			int buildId = this.BuildId;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onBuildIdChanged(buildId);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onBuildIdChanged(buildId);
			}
			uint oldValue = this.dialogID;
			Property property2 = idpropertys[5];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue);
			}
			Vector3 direction = this.direction;
			Property property3 = idpropertys[2];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue2 = this.entityNO;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue2);
			}
			uint oldValue3 = this.modelID;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue3);
			}
			byte oldValue4 = this.modelScale;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue4);
			}
			string oldValue5 = this.name;
			Property property7 = idpropertys[9];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue5);
			}
			Vector3 position = this.position;
			Property property8 = idpropertys[1];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue6 = this.uid;
			Property property9 = idpropertys[10];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue6);
			}
			uint oldValue7 = this.utype;
			Property property10 = idpropertys[11];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue7);
					return;
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue7);
			}
		}

		// Token: 0x04004F67 RID: 20327
		public EntityBaseEntityCall_BuildBase baseEntityCall;

		// Token: 0x04004F68 RID: 20328
		public EntityCellEntityCall_BuildBase cellEntityCall;

		// Token: 0x04004F69 RID: 20329
		public int BuildId;

		// Token: 0x04004F6A RID: 20330
		public uint dialogID;

		// Token: 0x04004F6B RID: 20331
		public uint entityNO;

		// Token: 0x04004F6C RID: 20332
		public uint modelID;

		// Token: 0x04004F6D RID: 20333
		public byte modelScale = 30;

		// Token: 0x04004F6E RID: 20334
		public string name = "";

		// Token: 0x04004F6F RID: 20335
		public uint uid;

		// Token: 0x04004F70 RID: 20336
		public uint utype;
	}
}
