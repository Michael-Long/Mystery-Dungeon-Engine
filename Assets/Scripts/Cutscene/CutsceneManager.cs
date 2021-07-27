using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class CutsceneManager : MonoBehaviour
    {
        [Tooltip("List of actions to perform in order once this cutscene is activated.")]
        public List<CutsceneAction> Actions;
        [Tooltip("The Trigger required for this cutscene to be activated.")]
        public CutsceneTrigger Trigger;


        private bool IsCutsceneActive = false;
        private bool IsCutsceneFinished = false;
        private Dictionary<int, CutsceneAction> ActiveActions = new Dictionary<int, CutsceneAction>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!IsCutsceneActive && Trigger.CheckTrigger())
            {
                IsCutsceneActive = true;
                StartCoroutine(DoCutscene());
            }
            else if(IsCutsceneFinished)
            {

            }
        }

        private IEnumerator DoCutscene()
        {
            for (int index = 0; index < Actions.Count; ++index)
            {
                ActiveActions.Add(index, Actions[index]);
                if (Actions[index] is WaitAction)
                {
                    do
                    {
                        bool anyActionsActive = false;
                        foreach (var action in ActiveActions)
                        {
                            if (action.Value.IsActionActive())
                            {
                                anyActionsActive = true;
                                break;
                            }
                            yield return null;
                        }

                        if (!anyActionsActive)
                        {
                            break;
                        }
                        yield return null;
                    } while (true);
                    yield return null;
                } 
                else
                {
                    StartCoroutine(Actions[index].DoAction());
                    yield return null;
                }
            }
        }
    }
}
