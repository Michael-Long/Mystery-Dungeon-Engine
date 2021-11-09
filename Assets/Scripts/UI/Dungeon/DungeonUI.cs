using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonUI : MonoBehaviour
{

    public Text dungeonName;
    public Text dungeonFloor;

    public void setName(string newName) {
        dungeonName.text = newName;
    }

    public void setFloor(int floor, bool isBasement) {
        string floorText = floor + "F";
        if (isBasement)
            floorText = "B" + floorText;
        dungeonFloor.text = floorText;
    }
}
