using System.IO;
using System.Xml;
using System;

using Assets.ObjectTypes;
using Assets.ObjectTypes.MoveData;

namespace Assets.Parsers
{
    public class MoveXMLParser
    {
        private class ExplorersOfSkyMoveData
        {
            public BaseMoveText text = new BaseMoveText();
            public ushort basePower;
            public byte type;
            public byte category;
            public ushort unk4;
            public ushort unk5;
            public byte basePP;
            public byte unk6;
            public byte unk7;
            public byte accuracy;
            public byte unk9;
            public byte unk10;
            public byte unk11;
            public byte unk12;
            public byte unk13;
            public byte unk14;
            public byte unk15;
            public byte unk16;
            public byte unk17;
            public byte unk18;
            public ushort moveID;
            public byte unk19;

            public ElementType Type
            {
                get
                {
                    switch (type)
                    {
                        case 1: return ElementType.Normal;
                        case 2: return ElementType.Fire;
                        case 3: return ElementType.Water;
                        case 4: return ElementType.Grass;
                        case 5: return ElementType.Electric;
                        case 6: return ElementType.Ice;
                        case 7: return ElementType.Fighting;
                        case 8: return ElementType.Poison;
                        case 9: return ElementType.Ground;
                        case 10: return ElementType.Flying;
                        case 11: return ElementType.Psychic;
                        case 12: return ElementType.Bug;
                        case 13: return ElementType.Rock;
                        case 14: return ElementType.Ghost;
                        case 15: return ElementType.Dragon;
                        case 16: return ElementType.Dark;
                        case 17: return ElementType.Steel;
                        case 18: return ElementType.Neutral;
                        default: return ElementType.None;
                    };
                }
            }

            public MoveCategory Category
            {
                get
                {
                    switch (category)
                    {
                        case 1: return MoveCategory.Special;
                        case 2: return MoveCategory.Status;
                        default: return MoveCategory.Physical;
                    };
                }
            }

            public MoveTarget Target
            {
                get
                {
                    switch (unk4)
                    {
                        case 0:
                            {
                                if (unk5 == 255)
                                {
                                    return MoveTarget.Attacker; // Snore
                                }
                                return MoveTarget.EnemyInFront;
                            }
                        case 2: return MoveTarget.Facing;
                        case 16: return MoveTarget.ThreeInFront;
                        case 32: return MoveTarget.EnemiesWithin1TileRange;
                        case 48: return MoveTarget.RoomEnemies;
                        case 49: return MoveTarget.RoomAllies;
                        case 50: return MoveTarget.RoomAll;
                        case 53: return MoveTarget.RoomAllExceptUser;
                        case 54: return MoveTarget.RoomAlliesExceptUser;
                        case 64: return MoveTarget.EnemyInFrontUpTo2Away;
                        case 80: return MoveTarget.LineOfSightEnemy;
                        case 82: return MoveTarget.LineOfSight;
                        case 96: return MoveTarget.FloorEnemies;
                        case 97: return MoveTarget.FloorAllies;
                        case 98: return MoveTarget.FloorAll;
                        case 115: return MoveTarget.User;
                        case 116: return MoveTarget.User; // 1-turn delay, like Dig
                        case 128: return MoveTarget.EnemyInFrontCutCorners;
                        case 133: return MoveTarget.FacingCutCorners;
                        case 144: return MoveTarget.EnemyInFrontUpTo2Away; // Ice Shard
                        case 255: return MoveTarget.Facing; // Me First
                        case 561: return MoveTarget.RoomAllies; // Softboiled
                        case 609: return MoveTarget.FloorAllies; // Moonlight, Milk Drink
                        case 627: return MoveTarget.User; // Swallow, Synthesis, Heal Order, Roost
                        case 1334: return MoveTarget.RoomAllies; // Maybe should be RoomAlliesExceptUser? Healing Wish, Lunar Dance
                        default: return MoveTarget.EnemyInFront;
                    };
                }
            }

            public void InitializeMoveData(BaseMove move)
            {
                move.inGameText = text;
                move.basePower = basePower;
                move.primaryType = Type;
                move.category = Category;
                move.basePP = basePP;
                move.accuracy = accuracy;
                move.id = moveID;
                move.target = Target;
            }
        }

        private bool HasReadStrings { get; set; } = false;
        private bool HasReadData { get; set; } = false;

        private MoveXMLParser()
        {
        }

        public bool ReadMove(XmlReader reader, BaseMove move)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Move":
                            string version = reader.GetAttribute("gameVersion");
                            switch (version)
                            {
                                case "EoS":
                                    ReadMoveExplorersOfSky(reader.ReadSubtree())
                                        .InitializeMoveData(move);
                                    return true;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        private ExplorersOfSkyMoveData ReadMoveExplorersOfSky(XmlReader reader)
        {
            var data = new ExplorersOfSkyMoveData();
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Strings":
                            if (!HasReadStrings)
                            {
                                ReadStrings(reader.ReadSubtree(), data);
                            }
                            break;
                        case "Data":
                            if (!HasReadData)
                            {
                                ReadData(reader.ReadSubtree(), data);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return data;
        }

        private void ReadStrings(XmlReader reader, ExplorersOfSkyMoveData data)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            data.text.moveName = reader.ReadElementContentAsString();
                            break;
                        case "Category":
                            data.text.moveCategory = reader.ReadElementContentAsString();
                            break;
                        default:
                            break;
                    }
                }
            }
            HasReadStrings = true;
        }

        private void ReadData(XmlReader reader, ExplorersOfSkyMoveData data)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "BasePower":
                            data.basePower = (ushort)reader.ReadElementContentAsInt();
                            break;
                        case "Type":
                            data.type = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Category":
                            data.category = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk4":
                            data.unk4 = (ushort)reader.ReadElementContentAsInt();
                            break;
                        case "Unk5":
                            data.unk5 = (ushort)reader.ReadElementContentAsInt();
                            break;
                        case "BasePP":
                            data.basePP = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk6":
                            data.unk6 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk7":
                            data.unk7 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Accuracy":
                            data.accuracy = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk9":
                            data.unk9 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk10":
                            data.unk10 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk11":
                            data.unk11 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk12":
                            data.unk12 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk13":
                            data.unk13 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk14":
                            data.unk14 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk15":
                            data.unk15 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk16":
                            data.unk16 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk17":
                            data.unk17 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "Unk18":
                            data.unk18 = (byte)reader.ReadElementContentAsInt();
                            break;
                        case "MoveID":
                            data.moveID = (ushort)reader.ReadElementContentAsInt();
                            break;
                        case "Unk19":
                            data.unk19 = (byte)reader.ReadElementContentAsInt();
                            break;
                        default:
                            break;
                    }
                }
            }
            HasReadData = true;
        }

        public static bool ParseMoveData(string inputUri, BaseMove move)
        {
            try
            {
                var parser = new MoveXMLParser();
                using (var fileStream = new StreamReader(inputUri, System.Text.Encoding.UTF8))
                {
                    using (var reader = XmlReader.Create(fileStream))
                    {
                        return parser.ReadMove(reader, move);
                    }
                }
            }
            catch (XmlException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            catch (UriFormatException)
            {
            }
            catch (FileNotFoundException)
            {
            }
            return false;
        }
    }
}