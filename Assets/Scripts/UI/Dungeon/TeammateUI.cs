using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Dungeon {
    public class TeammateUI : MonoBehaviour {
        public Image Portriat;
        public Image BackgroundBorder;
        public Image ForegroundBorder;
        public Text Name;
        public Text Level;
        public Text HP;
        public Text Belly;

        private int currHP = 1;
        private int maxHP = 1;
        private int currBelly = 100;
        private int maxBelly = 100;

        public void updatePortiat(Sprite newPort) {
            Portriat.sprite = newPort;
        }

        public void setName(string name) {
            Name.text = " " + name;
        }

        public void setLevel(short currLevel) {
            Level.text = " Lv: " + currLevel;
        }

        public void setHP(short currHP) {
            this.currHP = currHP;
            updateHP();
        }

        public void setMaxHP(short maxHP) {
            this.maxHP = maxHP;
            updateHP();
        }

        private void updateHP() {
            HP.text = " HP: " + currHP + "/" + maxHP;
        }

        public void setBelly(short currBelly) {
            this.currBelly = currBelly;
            updateBelly();
        }

        public void setMaxBelly(short maxBelly) {
            this.maxBelly = maxBelly;
            updateBelly();
        }

        private void updateBelly() {
            Belly.text = " Belly: " + currBelly + "/" + maxBelly;
        }

        public void setVisable(bool isVisable) {
            int opacity = isVisable ? 1 : 0;
            Portriat.color = new Color(Portriat.color.r, Portriat.color.g, Portriat.color.b, opacity);
            ForegroundBorder.color = new Color(ForegroundBorder.color.r, ForegroundBorder.color.g, ForegroundBorder.color.b, opacity);
            BackgroundBorder.color = new Color(BackgroundBorder.color.r, BackgroundBorder.color.g, BackgroundBorder.color.b, opacity);
            Name.color = new Color(Name.color.r, Name.color.g, Name.color.b, opacity);
            Level.color = new Color(Level.color.r, Level.color.g, Level.color.b, opacity);
            HP.color = new Color(HP.color.r, HP.color.g, HP.color.b, opacity);
            Belly.color = new Color(Belly.color.r, Belly.color.g, Belly.color.b, opacity);
        }
    }
}