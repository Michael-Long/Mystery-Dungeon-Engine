using Assets.ObjectTypes;
using Assets.ObjectTypes.Creature;
using Assets.ObjectTypes.MoveData;
using Assets.ObjectTypes.IQGroupData;

using System.ComponentModel;
using System.IO;
using System.Xml;
using System;

namespace Assets.Parsers
{
    public static class CreatureXMLParser
    {
        public static CreatureData ParseCreatureData(string workingFileName, CreatureData workingCreature)
        {

            if (!File.Exists(workingFileName))
            {
                return null;
            }

            bool entityParsed = false;
            var fileStream = new StreamReader(workingFileName, System.Text.Encoding.UTF8);
            var reader = XmlReader.Create(fileStream);

            // Now that I know about subtree parsing, this can be better blocked out into methods.
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Strings":
                            ParseStringsBlock(ref workingCreature, reader.ReadSubtree());
                            break;
                        case "GenderedEntity":
                            if (!entityParsed)
                            {
                                entityParsed = true;
                                ParseGenderedEntityBlock(ref workingCreature, reader.ReadSubtree());
                            }
                            else
                            {
                                ParseSecondaryGenderBlock(ref workingCreature, reader.ReadSubtree());
                            }
                            break;
                        case "Moveset":
                            ParseMovesetBlock(ref workingCreature, reader.ReadSubtree());
                            break;
                        case "StatsGrowth":
                            ParseStatsGrowthBlock(ref workingCreature, reader.ReadSubtree());
                            break;
                        default:
                            break;
                    }
                }
            }

            return workingCreature;
        }

        private static void ParseStringsBlock(ref CreatureData workingCreature, XmlReader stringReader)
        {
            while (stringReader.Read())
            {
                if (stringReader.IsStartElement())
                {
                    switch (stringReader.Name)
                    {
                        case "Name":
                            workingCreature.species = XMLParserHelper.BasicDataGrab<string>(stringReader);
                            break;
                        case "Category":
                            workingCreature.category = XMLParserHelper.BasicDataGrab<string>(stringReader);
                            break;
                    }
                }
            }
        }

        private static void ParseGenderedEntityBlock(ref CreatureData workingCreature, XmlReader entityReader)
        {
            while (entityReader.Read())
            {
                if (entityReader.IsStartElement())
                {
                    switch (entityReader.Name)
                    {
                        case "PokedexNumber":
                            workingCreature.creatureNo = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "Gender":
                            ushort genderID = XMLParserHelper.BasicDataGrab<ushort>(entityReader);
                            if (genderID == 3)
                            {
                                genderID = 0;
                            }
                            workingCreature.possibleGender += genderID;
                            break;
                        case "EvolutionReq":
                            ParseEvolutionBlock(ref workingCreature, entityReader.ReadSubtree());
                            break;
                        case "BodySize":
                            workingCreature.bodySize = (CreatureData.PartySize)XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "PrimaryType":
                            workingCreature.primaryType = XMLParserHelper.BasicDataGrab<ElementType>(entityReader);
                            break;
                        case "SecondaryType":
                            int secondType = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            if (secondType != 0)
                            {
                                workingCreature.secondaryType = (ElementType)secondType;
                            }
                            break;
                        case "MovementType":
                            int movementType = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            switch (movementType)
                            {
                                case 1:
                                    movementType = 5;
                                    break;
                                case 2:
                                    movementType = 3;
                                    break;
                                case 3:
                                    movementType = 4;
                                    break;
                                case 4:
                                    movementType = 2;
                                    break;
                                case 5:
                                    movementType = 1;
                                    break;
                                default:
                                    break;
                            }
                            workingCreature.movementType = (CreatureData.MovementType)movementType;
                            break;
                        case "IQGroup":
                            //IQGroup newIQGroup = ScriptableObject.CreateInstance<IQGroup>();
                            int todo = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            //workingCreature.iQGroup = newIQGroup;
                            break;
                        case "PrimaryAbility":
                            /*workingCreature.primaryAbility = */
                            XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "SecondaryAbility":
                            /*workingCreature.secondaryAbility = */
                            XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "ExpYield":
                            workingCreature.expYield = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "RecruitRate1":
                            workingCreature.recruitRate = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "RecruitRate2":
                            workingCreature.recruitRate2 = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "BaseStats":
                            ParseStatsBlock(ref workingCreature, entityReader.ReadSubtree());
                            break;
                        case "Weight":
                            workingCreature.weight = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "Size":
                            workingCreature.size = XMLParserHelper.BasicDataGrab<int>(entityReader);
                            break;
                        case "ExclusiveItems":
                            ParseExclusiveItemsBlock(ref workingCreature, entityReader.ReadSubtree());
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ParseSecondaryGenderBlock(ref CreatureData workingCreature, XmlReader genderReader)
        {
            bool complete = false;
            while (genderReader.Read() && !complete)
            {
                if (genderReader.IsStartElement() && genderReader.Name == "Gender")
                {
                    ushort genderID = XMLParserHelper.BasicDataGrab<ushort>(genderReader);
                    if (genderID == 3)
                    {
                        genderID = 0;
                    }
                    workingCreature.possibleGender += genderID;
                    complete = true;
                }
            }
        }

        private static void ParseEvolutionBlock(ref CreatureData workingCreature, XmlReader evolutionReader)
        {
            Evolution workingEvo = new Evolution();
            while (evolutionReader.Read())
            {
                if (evolutionReader.IsStartElement())
                {
                    switch (evolutionReader.Name)
                    {
                        case "PreEvoIndex":
                            // NOTE: This is the previous evolution, not the next evolution that we want to store.
                            workingEvo.nextEvo = XMLParserHelper.BasicDataGrab<int>(evolutionReader);
                            break;
                        case "Method":
                            int evoMethod = XMLParserHelper.BasicDataGrab<int>(evolutionReader);
                            // This can prob be improved by combining trade evo into item evolution.
                            if (evoMethod == 4)
                            {
                                evoMethod = 0;
                            }
                            else if (evoMethod == 5)
                            {
                                evoMethod = 4;
                            }
                            workingEvo.evolutionType = (EvolutionType)evoMethod;
                            break;
                        case "Param1":
                            workingEvo.evolutionRequirement = XMLParserHelper.BasicDataGrab<int>(evolutionReader);
                            break;
                        case "Param2":
                            int optionalItem = XMLParserHelper.BasicDataGrab<int>(evolutionReader);
                            if (optionalItem != 0)
                            {
                                workingEvo.optionalEvolutionItem = optionalItem;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            workingCreature.evolution.Add(workingEvo);
        }

        private static void ParseStatsBlock(ref CreatureData workingCreature, XmlReader statReader)
        {
            workingCreature.baseStats = new Stats();
            while (statReader.Read())
            {
                if (statReader.IsStartElement())
                {
                    switch (statReader.Name)
                    {
                        case "HP":
                            workingCreature.baseStats.HP = XMLParserHelper.BasicDataGrab<int>(statReader);
                            break;
                        case "Attack":
                            workingCreature.baseStats.attack = XMLParserHelper.BasicDataGrab<int>(statReader);
                            break;
                        case "Defense":
                            workingCreature.baseStats.defense = XMLParserHelper.BasicDataGrab<int>(statReader);
                            break;
                        case "SpAttack":
                            workingCreature.baseStats.spAttack = XMLParserHelper.BasicDataGrab<int>(statReader);
                            break;
                        case "SpDefense":
                            workingCreature.baseStats.spDefense = XMLParserHelper.BasicDataGrab<int>(statReader);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ParseExclusiveItemsBlock(ref CreatureData workingCreature, XmlReader eItemReader)
        {
            while (eItemReader.Read())
            {
                if (eItemReader.IsStartElement() && eItemReader.Name == "ItemID")
                {
                    // Not Fully Implemented, not sure if it can be
                    //ExclusiveItem newItem = ScriptableObject.CreateInstance<ExclusiveItem>();
                    //newItem.id = XMLParserHelper.BasicDataGrab<int>(eItemReader);
                    int todo = XMLParserHelper.BasicDataGrab<int>(eItemReader);
                    //workingCreature.exclusiveItems.Add(newItem);
                }
            }
        }

        private static void ParseMovesetBlock(ref CreatureData workingCreature, XmlReader movesetReader)
        {
            while (movesetReader.Read())
            {
                if (movesetReader.IsStartElement())
                {
                    switch (movesetReader.Name)
                    {
                        case "LevelUpMoves":
                            ParseLevelUpMovesBlock(ref workingCreature, movesetReader.ReadSubtree());
                            break;
                        case "EggMoves":
                            //ParseEggsMovesBlock(ref workingCreature, movesetReader.ReadSubtree());
                            break;
                        case "HmTmMoves":
                            //ParseTMMovesBlock(ref workingCreature, movesetReader.ReadSubtree());
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ParseLevelUpMovesBlock(ref CreatureData workingCreature, XmlReader levelupMoveReader)
        {
            while (levelupMoveReader.Read())
            {
                if (levelupMoveReader.IsStartElement() && levelupMoveReader.Name == "Learn")
                {
                    workingCreature.levelUpMoves.Add(ParseLevelUpMove(levelupMoveReader.ReadSubtree()));
                }
            }
        }

        private static LevelUpMove ParseLevelUpMove(XmlReader learnReader)
        {
            int level = 0;
            int moveID = 0;
            while (learnReader.Read())
            {
                if (learnReader.IsStartElement())
                {
                    switch (learnReader.Name)
                    {
                        case "Level":
                            level = XMLParserHelper.BasicDataGrab<int>(learnReader);
                            break;
                        case "MoveID":
                            moveID = XMLParserHelper.BasicDataGrab<int>(learnReader);
                            break;
                        default:
                            break;
                    }
                }
            }
            return new LevelUpMove(level);
        }

        private static void ParseEggsMovesBlock(ref CreatureData workingCreature, XmlReader eggMoveReader)
        {
            throw new NotImplementedException("XML Parser Doesn't Full Support this Operation");
            //workingCreature.eggMoves = new List<int>();
            //while (eggMoveReader.Read())
            //{
            //    if (eggMoveReader.IsStartElement() && eggMoveReader.Name == "MoveID")
            //    {
            //        workingCreature.eggMoves.Add(BasicDataGrab<int>(eggMoveReader));
            //    }
            //}
        }

        private static void ParseTMMovesBlock(ref CreatureData workingCreature, XmlReader tmMoveReader)
        {
            throw new NotImplementedException("XML Parser Doesn't Full Support this Operation");
            //workingCreature.tmMoves = new List<int>();
            //while (tmMoveReader.Read())
            //{
            //    if (tmMoveReader.IsStartElement() && tmMoveReader.Name == "MoveID")
            //    {
            //        workingCreature.tmMoves.Add(BasicDataGrab<int>(tmMoveReader));
            //    }
            //}
        }

        private static void ParseStatsGrowthBlock(ref CreatureData workingCreature, XmlReader statGrowthReader)
        {
            workingCreature.totalStats = new Stats();
            while (statGrowthReader.Read())
            {
                if (statGrowthReader.IsStartElement() && statGrowthReader.Name == "Level")
                {
                    XmlReader levelReader = statGrowthReader.ReadSubtree();
                    while (levelReader.Read())
                    {
                        if (levelReader.IsStartElement())
                        {
                            switch (levelReader.Name)
                            {
                                case "RequiredExp":
                                    workingCreature.totalExp = XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                case "HP":
                                    workingCreature.totalStats.HP += XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                case "Attack":
                                    workingCreature.totalStats.attack += XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                case "Defense":
                                    workingCreature.totalStats.defense += XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                case "SpAttack":
                                    workingCreature.totalStats.spAttack += XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                case "SpDefense":
                                    workingCreature.totalStats.spDefense += XMLParserHelper.BasicDataGrab<int>(levelReader);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
