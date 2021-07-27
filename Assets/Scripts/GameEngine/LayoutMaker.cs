using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine
{
    public class LayoutMaker : MonoBehaviour
    {
        // Initalisation
        private double[,] layout = new double[30, 100]; // In this 2D array, the first array is y, the second is x. If the size is too large, the rooms might end up being very far apart. If the size is too low, the loading may never finish, due to being unable to create the last final rooms
        public int maxRoomsToMake; // Defined in Unity. If this number is too low, the rooms might end up being VERY far apart. If this is too high, the loading may never finish, due to being unable to create the last final rooms
        public int maxPathsPerRoom = 3; // Defined in Unity.
        public int margin = 2; // This is to make sure rooms aren't made right next to each other. Defined in Unity.
        private int roomsMade = 0;

        // On start. Basic Unity function. 
        void Start()
        {
            log2DDoubleArray(layout); // Just allows me to log the change.
            int LayoutByte = 0; // Temporary, as I'm working on getting one type to work first.
                                // int LayoutByte = Random.Range (0, 16); // Chooses random floor type. Could make this hex as in https://github.com/Aissurteivos/mdrngzer/blob/master/doc/floorLayouts.md, but I don't really see the point.
            switch (LayoutByte)
            { // There are duplicates here. I believe that's because it's to give them more chance, rather than it being a fair dice.
                case 0:
                    medLarge();
                    break;
                case 1:
                    small();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10: // Equivalent to 0A
                    outerRooms();
                    break;
                case 11:
                    smallMed();
                    break;
                case 12:
                    medLarge();
                    break;
                case 13:
                    medLarge();
                    break;
                case 14:
                    medLarge();
                    break;
                case 15:
                    medLarge();
                    break;
            }
        }

        void medLarge()
        {
            while (roomsMade < maxRoomsToMake)
            {
                int[] xyLengths = new int[] { 0, 0 }; // Initialising the variable which will hold the room lengths.
                int xyDecider = Random.Range(1, 3);
                if (xyDecider == 2)
                { // This just chooses whether y is longer than x or x is longer than y.
                    xyLengths[0] = Random.Range(2, 7);
                    xyLengths[1] = Random.Range(2, 5);
                }
                else
                {
                    xyLengths[0] = Random.Range(2, 5);
                    xyLengths[1] = Random.Range(2, 7);
                }
                int[] roomOrigin = new int[] { Random.Range(0, layout.GetLength(0)), Random.Range(0, layout.GetLength(1)) }; // Finds a random location, and this will be the location from which the checking will stem from. It's the top left.
                checkLocations(roomOrigin, xyLengths);
            }
            log2DDoubleArray(layout);
        }

        void smallMed()
        {

        }

        void outerRooms()
        {

        }

        void small()
        {

        }

        // Basically the following function checks if the room can be set at the roomOrigin. 
        void checkLocations(int[] roomOrigin, int[] xyLengths)
        {
            int[] startSearch = new int[] { roomOrigin[0] - margin, roomOrigin[1] - margin }; // Start search at y, x. We use margin to make sure there's space around the rooms, so two rooms don't spawn right next to each other.
            int[] endSearch = new int[] { roomOrigin[0] + (xyLengths[0] - 1) + margin, roomOrigin[1] + (xyLengths[1] - 1) + margin }; // End search at y, x
            if (endSearch[0] >= layout.GetLength(0) || endSearch[1] >= layout.GetLength(1) || startSearch[0] <= 0 || startSearch[1] <= 0) { return; }
            else
            { // If something is out of bounds for whatever reason. I will use these weird if statements a lot; it's just nicer to have everything on one line.
                for (int y = startSearch[0]; y <= endSearch[0]; y++)
                {
                    for (int x = startSearch[1]; x <= endSearch[1]; x++)
                    {
                        if (layout[y, x] != 0) { return; }
                    }
                }
                // If wer're here, then that means that the room can be made.
                int[] startPlace = new int[] { startSearch[0] + margin, startSearch[1] + margin }; // Removing the margins, because we don't want the room to spawn on that margin.
                int[] endPlace = new int[] { endSearch[0] - margin, endSearch[1] - margin };
                placeLocations(startPlace, endPlace);
            }
        }

        // Now that we've checked that the room can be made, we make the room.
        void placeLocations(int[] startPlace, int[] endPlace)
        {
            for (int y = startPlace[0]; y <= endPlace[0]; y++)
            {
                for (int x = startPlace[1]; x <= endPlace[1]; x++)
                {
                    layout[y, x] = 1;
                }
            }
            // And here the room is made. But of course, we need corridors, so we move on.
            makeCorridorNodes(startPlace, endPlace);
            roomsMade++;
        }

        // This is a longer one. This essentially creates a corridor beginnings which follow certain criteria. 
        // Criteria here are: Nodes may not be created on corners or in the center, and they may not be side by side. 
        void makeCorridorNodes(int[] startPlace, int[] endPlace)
        {
            int[] marginStart = new int[] { startPlace[0] - 1, startPlace[1] - 1 }; // Back to margins. This is fixed at 1, though. Otherwise, we'd have corridors not attached to rooms.
            int[] marginEnd = new int[] { endPlace[0] + 1, endPlace[1] + 1 };

            // -- Following section checks the first two criteria.
            string possibleNodes = ""; // Basically, I don't like using Lists, and yeah they might be more efficient or whatever, but I'd rather just do this. This just creates a string we add to, so we can later split back to an array.
            for (int y = marginStart[0]; y <= marginEnd[0]; y++)
            {
                for (int x = marginStart[1]; x <= marginEnd[1]; x++)
                {
                    if (checkValid(y, x, marginStart, marginEnd))
                    { // I realised that the conditions here were so messy and caused so much indentation I created a new function for it. 
                        possibleNodes += "|" + y.ToString() + "," + x.ToString(); // And wow we add to the string
                    }
                }
            }
            possibleNodes = possibleNodes.Substring(1); // Remove the first "|", as otherwise we'd have an empty element in the array, which causes many issues.
            string[] possibleNodesSplit = possibleNodes.Split("|"[0]); // And back to the array we are.

            // -- Following section chooses nodes at random.
            string decidedNodes = ""; // And we make another string. You see this would be easily solvable if we just had resizeable arrays in C#! Whatever, this works, so I'm keeping it this way.
            for (int attempt = 1; attempt <= maxPathsPerRoom; attempt++)
            {
                int chosen = Random.Range(0, possibleNodesSplit.Length);
                decidedNodes += "|" + possibleNodesSplit[chosen].ToString();
            }
            decidedNodes = decidedNodes.Substring(1);
            string[] decidedNodesSplit = decidedNodes.Split("|"[0]);

            // -- Following section checks for the third criteria, then if that's fine, places down the node.
            foreach (string coords in decidedNodesSplit)
            {
                string[] coordSplit = coords.Split(","[0]);
                int yChosen = System.Convert.ToInt32(coordSplit[0]); // Putting the strings back to numbers so that they are usable for placing the location in a moment.
                int xChosen = System.Convert.ToInt32(coordSplit[1]);
                if (layout[yChosen - 1, xChosen] == 2 || layout[yChosen + 1, xChosen] == 2 || layout[yChosen, xChosen + 1] == 2 || layout[yChosen, xChosen - 1] == 2) { return; }
                else
                { // This specifically checks for the third criteria.
                    layout[yChosen, xChosen] = 2; // And after all of that, we finally place down the node.
                }
            }
        }

        // So this is where the checks happen. 
        bool checkValid(int currY, int currX, int[] marginStart, int[] marginEnd)
        {
            if (currX == marginStart[1] && currY == marginStart[0]) { return false; }
            else
            { // If you're a corner. Here you can see more of my weird ifs.
                if (currX == marginEnd[1] && currY == marginEnd[0]) { return false; }
                else
                { // if youre a corner 
                    if (currX == marginStart[1] && currY == marginEnd[0]) { return false; }
                    else
                    { // If You're A Corner.
                        if (currX == marginEnd[1] && currY == marginStart[0]) { return false; }
                        else
                        { // IF YOU ARE A CORNER! There was some way to simplify this, but it would've looked horrible. This is the only time ever I was against one line.
                            if (currY == marginStart[0] || currY == marginEnd[0] || currX == marginStart[1] || currX == marginEnd[1])
                            { // If you're on the edge
                                return true; // You're cool
                            }
                            else { return false; } // You're not cool.
                        }
                    }
                }
            }
        }

        // Unity doesn't actually come with a way to represent 2D arrays in logs or in the inspector. So I created my own logger.
        void log2DDoubleArray(double[,] arr)
        {
            var msg = "";
            for (int x = 0; x < arr.GetLength(0); x++)
            {
                for (int y = 0; y < arr.GetLength(1); y++)
                {
                    msg += arr[x, y].ToString();
                }
                msg += "\n";
            }
            Debug.Log(msg); // Fun fact, since fonts are taller than wider, rooms will appear taller than they really are. No way of fixing that. 
        }
    }
}