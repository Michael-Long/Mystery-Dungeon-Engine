using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Dialog
{
    public class DialogueManager : MonoBehaviour
    {
        [Tooltip("Parent Portiat of the dialogue UI.")]
        public Canvas dialogUI = null;
        [Tooltip("The GameObject that holds the main Portiat image.")]
        public GameObject PortiatObject = null;
        [Tooltip("The GameObject that represents the border surrounding the Portiat.")]
        public GameObject PortiatBorderObject = null;
        [Tooltip("The GameObject that holds where the text will be inserted into.")]
        public GameObject TextObject = null;
        [Tooltip("The GameObject the holds the sound to play while text is being displayed.")]
        public GameObject TextSoundObject = null;
        [Tooltip("The GameObject that holds the \"Continue Arrow\", to indiciate when the sentence has completed.")]
        public GameObject ContinueArrowObject = null;

        private Dialogue currDialog = null;
        private Queue<Dialogue> dialogQueue = new Queue<Dialogue>();
        // Sentence Processor, for when a character has multiple pages of dialog.
        private Queue<string> sentences = new Queue<string>();

        // Components within the above GameObjects that we need, so we don't have to always search for them.
        private Text textArea = null;
        private Image portiat = null;
        private Image portiatBorder = null;
        private AudioSource textSound = null;
        private Image continueArrowImage = null;
        private Animation continueArrowAnim = null;
        private PortiatAnimator portiatAnim = null;

        // PUBLIC METHODS

        public void Start()
        {
            dialogUI.enabled = false;

            textArea = TextObject.GetComponent<Text>();
            portiat = PortiatObject.GetComponent<Image>();
            portiatBorder = PortiatBorderObject.GetComponent<Image>();
            textSound = TextSoundObject.GetComponent<AudioSource>();
            continueArrowImage = ContinueArrowObject.GetComponent<Image>();
            continueArrowAnim = ContinueArrowObject.GetComponent<Animation>();
            portiatAnim = PortiatObject.GetComponent<PortiatAnimator>();
        }
        public void AddDialog(Dialogue dialog)
        {
            dialogQueue.Enqueue(dialog);
        }

        public void TriggerDialog()
        {
            if (dialogUI.enabled)
            {
                DisplayNextSentence();
            } else
            {
                StartDialog();
            }
        }

        public bool ProcessingDialog()
        {
            return dialogUI.enabled;
        }

        public void EndDialog()
        {
            dialogUI.enabled = false;
            currDialog = null;
            dialogQueue.Clear();
            sentences.Clear();
            portiatAnim.StopAnimation();
            textArea.text = "";
        }

        // PRIVATE METHODS
        private void StartDialog()
        {
            if (dialogQueue.Count > 0)
            {
                currDialog = dialogQueue.Dequeue();
                if (currDialog.TalkerPortiat)
                {
                    portiat.sprite = currDialog.TalkerPortiat;
                    portiat.enabled = true;
                    portiatBorder.enabled = true;
                }
                else
                {
                    portiat.enabled = false;
                    portiatBorder.enabled = false;
                }

                dialogUI.enabled = true;

                sentences.Clear();

                string[] dialogSentences = currDialog.Sentences;
                if (currDialog.ImportSentences)
                    dialogSentences = currDialog.ImportSentences.text.Split('\n');

                foreach (string line in dialogSentences)
                {
                    sentences.Enqueue(line);
                }

                SetupPortiatAnimator(currDialog);

                DisplayNextSentence();
            }
        }

        private void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                if (dialogQueue.Count > 0)
                {
                    StartDialog();
                    return;
                }
                else
                {
                    StopAllCoroutines();
                    EndDialog();
                    return;
                }
            }
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences.Dequeue(), currDialog));
        }

        private void SetupPortiatAnimator(Dialogue currDialog)
        {
            if (currDialog.animateTrigger != PortiatAnimateTrigger.None)
            {
                portiatAnim.SetParams(currDialog.SpriteFrames, currDialog.Duration, currDialog.Delay, currDialog.Loop, currDialog.AnimationSound);
            }
            else
            {
                portiatAnim.StopAnimation();
            }

            if (currDialog.animateTrigger == PortiatAnimateTrigger.OnStart)
            {
                portiatAnim.StartAnimation();
            }
        }

        IEnumerator TypeSentence(string sentence, Dialogue dialog)
        {
            SetContinueArrowState(false);
            textArea.text = "";

            if (!string.IsNullOrEmpty(dialog.SpeakerName))
                textArea.text = "<color=#" + ColorToHex(dialog.TalkerColor) + ">" + dialog.SpeakerName + "</color>: ";

            for (int index = 0; index < sentence.Length; ++index)
            {
                char letter = sentence[index];
                if (letter == '<')
                {
                    index = processTaggedLines(sentence, index);
                }
                else
                {
                    textArea.text += letter;
                    if (!textSound.isPlaying)
                        textSound.Play();
                }
                yield return null;
            }
            SetContinueArrowState(true);
            if (dialog.animateTrigger == PortiatAnimateTrigger.OnCompleteSpeech)
                portiatAnim.StartAnimation();
        }

        private void SetContinueArrowState(bool state)
        {
            continueArrowImage.enabled = state;
            continueArrowAnim.enabled = state;
        }

        private string ColorToHex(Color32 color)
        {
            // Takes a Unity Color32 object and converts it to the hex tag for usage in Rich Text.
            string hex = "";
            hex += BitConverter.ToString(BitConverter.GetBytes(color.r)).Substring(0, 2);
            hex += BitConverter.ToString(BitConverter.GetBytes(color.g)).Substring(0, 2);
            hex += BitConverter.ToString(BitConverter.GetBytes(color.b)).Substring(0, 2);

            return hex;
        }

        private int processTaggedLines(string sentence, int currIndex)
        {
            // Processes tags so that the tags themselves don't auto-fill with the scrolling text.
            int frontTagEndIndex = sentence.IndexOf(">", currIndex) + 1;
            string frontTag = sentence.Substring(currIndex, frontTagEndIndex - currIndex);

            string backTag = frontTag[0] + "/" + frontTag.Substring(1);
            string content = sentence.Substring(frontTagEndIndex, sentence.IndexOf(backTag, frontTagEndIndex) - frontTagEndIndex);

            textArea.text += frontTag + backTag;
            int newIndex = textArea.text.Length - backTag.Length;

            int count = 0;
            for (; count < content.Length; ++newIndex, ++count)
            {
                textArea.text = textArea.text.Insert(newIndex, content[count] + "");
            }

            return currIndex + frontTag.Length + content.Length + backTag.Length - 1;
        }

    }
}
