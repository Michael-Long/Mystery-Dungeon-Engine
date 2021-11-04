using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.Dungeon {
    public class PartyUI : MonoBehaviour {

        public TeammateUI Mate1;
        public TeammateUI Mate2;
        public TeammateUI Mate3;
        public TeammateUI Mate4;

        public void Update() {
            // Yay big test!
            Mate2.setVisable(false);
            Mate3.setVisable(false);
            Mate4.setVisable(false);
        }
    }
}