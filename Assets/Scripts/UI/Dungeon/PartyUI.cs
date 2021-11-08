using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.Dungeon {
    public class PartyUI : MonoBehaviour {

        public TeammateUI Mate1;
        public TeammateUI Mate2;
        public TeammateUI Mate3;
        public TeammateUI Mate4;

        private TeammateUI getTeammate(int teammateIndex) {
            switch (teammateIndex) {
                case 0:
                    return Mate1;
                case 1:
                    return Mate2;
                case 2:
                    return Mate3;
                case 3:
                    return Mate4;
                default:
                    Debug.LogWarning("Invalid Teammate Index given: " + teammateIndex);
                    return null;
            }
        }

        public void SetTeammateName(int teammateIndex, string name) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setName(name);
        }

        public void SetTeammatePortriat(int teammateIndex, Sprite portrait) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.updatePortiat(portrait);
        }

        public void SetTeammateLevel(int teammateIndex, int level) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setLevel(level);
        }

        public void SetTeammateVisible(int teammateIndex, bool isVisable) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setVisable(isVisable);
        }

        public void SetTeammateHealth(int teammateIndex, int currHealth) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setHP(currHealth);
        }

        public void SetTeammateMaxHealth(int teammateIndex, int maxHealth) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setMaxHP(maxHealth);
        }

        public void SetTeammateBelly(int teammateIndex, int currBelly) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setBelly(currBelly);
        }

        public void SetTeammateMaxBelly(int teammateIndex, int maxBelly) {
            TeammateUI ui = getTeammate(teammateIndex);
            if (ui)
                ui.setMaxBelly(maxBelly);
        }
    }
}