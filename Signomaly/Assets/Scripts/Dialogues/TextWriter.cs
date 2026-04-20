using System;
using System.Collections.Generic;
using CSTGames.SharedResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AforgeStudios.Signomaly
{
    public class TextWriter : Singleton<TextWriter>
    {
        List<EffectTextWriterSingle> textWriterList;

        protected void Awake()
        {
            base.Awake();
            textWriterList = new List<EffectTextWriterSingle>();
        }


        public EffectTextWriterSingle AddWrite(TextMeshProUGUI writeTextMesh, string textToWrite, float timePerCharacter, Action OnComplete, bool isInvisibleCharacter = true, bool removeBeforAdd = true)
        {
            if (removeBeforAdd)
                RemoveWrite(writeTextMesh);

            EffectTextWriterSingle writerSingle = new EffectTextWriterSingle(writeTextMesh, textToWrite, timePerCharacter, isInvisibleCharacter, OnComplete);
            textWriterList.Add(writerSingle);

            return writerSingle;
        }

        public void RemoveWrite(TextMeshProUGUI UIText)
        {
            for (int i = 0; i < textWriterList.Count; i++)
            {
                if (textWriterList[i].GetUIText() == UIText)
                {
                    textWriterList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < textWriterList.Count; i++)
            {
                if (textWriterList[i].UpdateWritingText())
                {
                    textWriterList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public class EffectTextWriterSingle
    {
        private TextMeshProUGUI uIText;
        private string textToWrite;
        private float timePerCharacter;
        private int characterIndex = 0;
        private float timer;
        private bool isInvisibleCharacter;
        private Action OnComplete;

        public EffectTextWriterSingle(TextMeshProUGUI uIText, string textToWrite, float timePerCharacter, bool isInvisibleCharacter, Action OnComplete)
        {
            this.uIText = uIText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.isInvisibleCharacter = isInvisibleCharacter;
            this.OnComplete = OnComplete;
            characterIndex = 0;
        }

        public bool UpdateWritingText()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;

                string text = textToWrite.Substring(0, characterIndex);

                if (isInvisibleCharacter)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }

                uIText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    if (OnComplete != null)
                        OnComplete();
                    return true;
                }
            }

            return false;
        }

        public void WriteAllAndDestroy()
        {
            uIText.text = textToWrite;
            characterIndex = textToWrite.Length;
            if (OnComplete != null)
                OnComplete();
            TextWriter.Instance.RemoveWrite(uIText);
        }

        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }

        public TextMeshProUGUI GetUIText()
        {
            return uIText;
        }
    }
}