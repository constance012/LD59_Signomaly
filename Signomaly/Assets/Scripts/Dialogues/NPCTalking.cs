using System;
using System.Collections.Generic;
using System.Linq;
using CSTGames.SharedResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AforgeStudios.Signomaly
{
    public class NPCTalking : MonoBehaviour
    {
        // [Header("References"), Space]
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI writeTextMesh;
        [SerializeField] private Image iconSkip;
        [SerializeField] private float timePerCharacter;
        [SerializeField] private Button skipButton;
        [SerializeField] private string name;

        private EffectTextWriterSingle writerSingle;
        private int meetTimes;
        private bool canSkip;
        private int indexScript;
        List<string> script;

        private void Awake()
        {
            meetTimes = 0;
            indexScript = 0;
            script = new List<string>();
        }

        private void Start()
        {
            StartConversation();
            
            skipButton.onClick.AddListener(() =>
            {
                
                    if(canSkip && writerSingle != null && writerSingle.IsActive())
                    {
                        writerSingle.WriteAllAndDestroy();
                    }
                    else if(script != null && writerSingle != null && !writerSingle.IsActive())
                    {
                        writerSingle = TextWriter.Instance.AddWrite(writeTextMesh, script[indexScript], timePerCharacter, StopTalking);
                        
                        indexScript++;
                            
                        if(indexScript >= script.Count)
                        {
                            Debug.Log("End Conversation");
                            script = null;
                        }
                    }
            });
        }

        private void Update()
        {
            if(NewInputManager.Instance.GetKeyDown(UnityEngine.InputSystem.Key.R))
            {
                Debug.Log("talk again");
                meetTimes++;
                StartConversation();
            }

            if(writerSingle != null)
            {
                if(meetTimes == 0)
                {
                    if(writerSingle.IsActive())
                    {
                        iconSkip.enabled = false;
                    }
                    else
                    {
                        iconSkip.enabled = true;
                    }
                }
                else
                {
                    iconSkip.enabled = true;
                }
            }
        }

        private void StartConversation()
        {
            if(meetTimes == 0)
            {
                //First Conversation
                script = DialogueScript.firstDialogue.ToList();
                indexScript = 0;
                canSkip = false;
            }
            else
            {
                script = DialogueScript.necessaryInstructionsDialogue.ToList();
                indexScript = 0;
                canSkip = true;
            }

            nameTextMesh.text = name;
            writerSingle = TextWriter.Instance.AddWrite(writeTextMesh, script[indexScript++], timePerCharacter, null);
        }

        private void StopTalking()
        {
            return;
        }

        public void ResetMeetTimes()
        {
            meetTimes = 0;
        }
    }
    
    public class DialogueScript
    {
        public static readonly string[] firstDialogue =
        {
            "Heh… another one, huh? Are you really that greedy for money?",
            "Well, everything comes at a price.",
            "If you want the treasure, you'll have to play the game just like the contract says.",
            "Win, and you might walk away with that massive fortune. But lose… and we take no responsibility.",
            "You'll become nothing more than entertainment for Them. Hahahaha…",
            "So… ready to step into hell?",
            "Good. Here are the rules of our little game. See that door beside you, ahh the one with the… unusual requirement? That's your way out. But to unlock it, you'll need to fulfill two conditions.",
            "First, look at the door in front of you.",
            "See the timer? Fifteen minutes.",
            "That's all the time you have left to finish the game.",
            "Meet the other condition before it hits zero, and the door will open. If not… hahaha.",
            "Second, beyond this room lies a space filled with all sorts of objects. Your task is to find the anomalies among them.",
            "If you suspect something's off, stand your ground and face it directly, don't look away. It will present you with a puzzle. Solve it, and you'll set it free.",
            "But don't get too corky. These anomalies aren't as forgiving as I am.",
            "Each of them has its own timer hovering overhead… and if their time runs out, it's game over for you too. So keep your eyes sharp.",
            "Free them all within the fifteen minutes I've given you, and the door will open. Walk through it, and that million-dollar prize is yours.",
            "…assuming you can pull it off. Plenty have tried. Plenty have died. Turns out, if you want something valuable, you have to pay a matching price.",
            "Good luck? I guess… and perhaps we'll meet again or not, hahaha…",
        };

        public static readonly string[] necessaryInstructionsDialogue =
        {
            "See that door beside you, ahh the one with the… unusual requirement? That's your way out. But to unlock it, you'll need to fulfill two conditions.",
            "First, look at the door in front of you.",
            "See the timer? Fifteen minutes.",
            "That's all the time you have left to finish the game.",
            "Meet the other condition before it hits zero, and the door will open. If not… hahaha.",
            "Second, beyond this room lies a space filled with all sorts of objects. Your task is to find the anomalies among them.",
            "If you suspect something's off, stand your ground and face it directly, don't look away. It will present you with a puzzle. Solve it, and you'll set it free.",
            "But don't get too corky. These anomalies aren't as forgiving as I am.",
            "Each of them has its own timer hovering overhead… and if their time runs out, it's game over for you too. So keep your eyes sharp.",
            "Free them all within the fifteen minutes I've given you, and the door will open. Walk through it, and that million-dollar prize is yours.",
        };
    }
}