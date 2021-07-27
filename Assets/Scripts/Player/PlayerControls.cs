using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Player
{
    public class PlayerControls
    {
        private static PlayerControls instance = null;

        public KeyCode UpDirection { get; set; }
        public KeyCode LeftDirection { get; set; }
        public KeyCode DownDirection { get; set; }
        public KeyCode RightDirection { get; set; }

        public KeyCode ActionKey { get; set; }
        public KeyCode BackKey { get; set; }
        public KeyCode MenuKey { get; set; }
        public KeyCode LeftActionKey { get; set; }
        public KeyCode RightActionKey { get; set; }

        public KeyCode MoveHotkey1 { get; set; }
        public KeyCode MoveHotkey2 { get; set; }
        public KeyCode MoveHotkey3 { get; set; }
        public KeyCode MoveHotkey4 { get; set; }

        public static PlayerControls getInstance()
        {
            if (instance == null)
                instance = new PlayerControls();

            return instance;
        }

        private PlayerControls() {
            // Eventually should load controls from some saved file.
            UpDirection = KeyCode.UpArrow;
            LeftDirection = KeyCode.LeftArrow;
            DownDirection = KeyCode.DownArrow;
            RightDirection = KeyCode.RightArrow;

            ActionKey = KeyCode.Z;
            BackKey = KeyCode.X;
            MenuKey = KeyCode.Return;
            LeftActionKey = KeyCode.A;
            RightActionKey = KeyCode.S;

            MoveHotkey1 = KeyCode.Alpha1;
            MoveHotkey2 = KeyCode.Alpha2;
            MoveHotkey3 = KeyCode.Alpha3;
            MoveHotkey4 = KeyCode.Alpha4;
        }

        public List<KeyCode> GetPressedPlayerControls()
        {
            // Return a list of keys, since multiple could be pressed?
            List<KeyCode> PressedKeys = new List<KeyCode>();
            if (Input.GetKey(LeftDirection))
                PressedKeys.Add(LeftDirection);
            if (Input.GetKey(RightDirection))
                PressedKeys.Add(RightDirection);
            if (Input.GetKey(UpDirection))
                PressedKeys.Add(UpDirection);
            if (Input.GetKey(DownDirection))
                PressedKeys.Add(DownDirection);
            if (Input.GetKey(ActionKey))
                PressedKeys.Add(ActionKey);
            if (Input.GetKey(BackKey))
                PressedKeys.Add(BackKey);
            if (Input.GetKey(MenuKey))
                PressedKeys.Add(MenuKey);
            if (Input.GetKey(LeftActionKey))
                PressedKeys.Add(LeftActionKey);
            if (Input.GetKey(RightActionKey))
                PressedKeys.Add(RightActionKey);
            if (Input.GetKey(MoveHotkey1))
                PressedKeys.Add(MoveHotkey1);
            if (Input.GetKey(MoveHotkey2))
                PressedKeys.Add(MoveHotkey2);
            if (Input.GetKey(MoveHotkey3))
                PressedKeys.Add(MoveHotkey3);
            if (Input.GetKey(MoveHotkey4))
                PressedKeys.Add(MoveHotkey4);
            return PressedKeys;
        }

        public bool IsMovementControl(List<KeyCode> PressedKeys)
        {
            if (PressedKeys.Contains(LeftDirection))
                return true;
            if (PressedKeys.Contains(RightDirection))
                return true;
            if (PressedKeys.Contains(UpDirection))
                return true;
            if (PressedKeys.Contains(DownDirection))
                return true;
            return false;
        }
    }
}