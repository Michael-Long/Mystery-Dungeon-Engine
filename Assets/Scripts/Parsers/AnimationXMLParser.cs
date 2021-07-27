using System.Collections.Generic;
using System.IO;
using System.Xml;

using Assets.ObjectTypes.AnimationData;
using Assets.Parsers;

public static class AnimationXMLParser
{
    public static LegacyAnimation ParseAnimationData(string workingFileName, LegacyAnimation workingAnimation)
    {
        if (!File.Exists(workingFileName))
        {
            return null;
        }

        var fileStream = new StreamReader(workingFileName, System.Text.Encoding.UTF8);
        var reader = XmlReader.Create(fileStream);

        List<AnimationSequence> collectedSequences = new List<AnimationSequence>();

        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                switch (reader.Name)
                {
                    case "FrameWidth":
                        workingAnimation.FrameWidth = XMLParserHelper.BasicDataGrab<int>(reader);
                        break;
                    case "FrameHeight":
                        workingAnimation.FrameHeight = XMLParserHelper.BasicDataGrab<int>(reader);
                        break;
                    case "AnimGroupTable":
                        ParseGroupTableBlock(ref workingAnimation, reader.ReadSubtree());
                        break;
                    case "AnimSequenceTable":
                        ParseSequenceTableBlock(ref collectedSequences, reader.ReadSubtree());
                        break;
                    default:
                        break;
                }
            }
        }
        connectSequences(ref workingAnimation, collectedSequences);
        return workingAnimation;
    }

    private static void ParseGroupTableBlock(ref LegacyAnimation workingAnimation, XmlReader reader)
    {
        AnimationType currType = AnimationType.None;
        while (reader.Read())
        {
            if (reader.IsStartElement() && reader.Name == "AnimGroup")
            {
                AnimationGroup group = new AnimationGroup();
                group.Type = currType;
                XmlReader groupReader = reader.ReadSubtree();
                while(groupReader.Read())
                {
                    if (groupReader.IsStartElement() && groupReader.Name == "AnimSequenceIndex")
                    {
                        group.AnimationSequenceIndexList.Add(XMLParserHelper.BasicDataGrab<int>(groupReader));
                    }
                }
                TypedAnimationGroup newGroup = new TypedAnimationGroup(currType, group);
                workingAnimation.AnimationGroups.Add(newGroup);
                currType += 1;
            }
        }
    }

    private static void ParseSequenceTableBlock(ref List<AnimationSequence> collectedSequences, XmlReader reader)
    {
        int index = 0;
        while (reader.Read())
        {
            if (reader.IsStartElement() && reader.Name == "AnimSequence")
            {
                AnimationSequence sequence = new AnimationSequence();
                sequence.Index = index;
                XmlReader sequenceReader = reader.ReadSubtree();
                while(sequenceReader.Read())
                {
                    if (sequenceReader.IsStartElement())
                    {
                        switch (sequenceReader.Name)
                        {
                            case "RushPoint":
                                sequence.RushPoint = XMLParserHelper.BasicDataGrab<int>(sequenceReader);
                                break;
                            case "HitPoint":
                                sequence.HitPoint = XMLParserHelper.BasicDataGrab<int>(sequenceReader);
                                break;
                            case "ReturnPoint":
                                sequence.ReturnPoint = XMLParserHelper.BasicDataGrab<int>(sequenceReader);
                                break;
                            case "AnimFrame":
                                AnimationFrame frame = ParseFrameBlock(sequenceReader.ReadSubtree());
                                sequence.Frames.Add(frame);
                                break;
                            default:
                                break;
                        }
                    }
                }
                collectedSequences.Add(sequence);
                ++index;
            }
        }
    }

    private static AnimationFrame ParseFrameBlock(XmlReader frameReader)
    {
        AnimationFrame frame = new AnimationFrame();
        while(frameReader.Read())
        {
            if (frameReader.IsStartElement())
            {
                switch (frameReader.Name)
                {
                    case "Duration":
                        frame.Duration = XMLParserHelper.BasicDataGrab<int>(frameReader);
                        break;
                    case "MetaFrameGroupIndex":
                        frame.SpriteIndex = XMLParserHelper.BasicDataGrab<int>(frameReader);
                        break;
                    case "Sprite":
                        XmlReader spriteReader = frameReader.ReadSubtree();
                        while(spriteReader.Read())
                        {
                            if (spriteReader.IsStartElement())
                            {
                                switch(spriteReader.Name)
                                {
                                    case "XOffset":
                                        frame.spriteXPos = XMLParserHelper.BasicDataGrab<int>(spriteReader);
                                        break;
                                    case "YOffset":
                                        frame.spriteYPos = XMLParserHelper.BasicDataGrab<int>(spriteReader) * -1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                    case "HFlip":
                        int isFlip = XMLParserHelper.BasicDataGrab<int>(frameReader);
                        frame.HorizFlip = isFlip > 0 ? true : false;
                        break;
                    case "Shadow":
                        XmlReader shadowReader = frameReader.ReadSubtree();
                        while (shadowReader.Read())
                        {
                            if (shadowReader.IsStartElement())
                            {
                                switch (shadowReader.Name)
                                {
                                    case "XOffset":
                                        frame.shadowXOffset = XMLParserHelper.BasicDataGrab<int>(shadowReader);
                                        break;
                                    case "YOffset":
                                        frame.shadowYOffset = XMLParserHelper.BasicDataGrab<int>(shadowReader);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        return frame;
    }

    private static void connectSequences(ref LegacyAnimation workingAnimation, List<AnimationSequence> animationSequences)
    {
        foreach (var typedGroup in workingAnimation.AnimationGroups)
        {
            foreach(int index in typedGroup.group.AnimationSequenceIndexList)
            {
                typedGroup.group.AnimationSequenceList.Add(animationSequences[index]);
                int seqIndex = typedGroup.group.AnimationSequenceList.Count - 1;
                typedGroup.group.AnimationSequenceList[seqIndex].Direction = (AnimationDirection)seqIndex;
            }
        }
    }
}
