using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace IStreamYouScream
{
    public class LettersRiddle : MonoBehaviour
    {
        public Letter[] letters = new Letter[5];
        public Letter[] wrongLetters = new Letter[15];
        private int numberOfLettersLit = 0;
        private int numberOfCorrectLettersLit = 0;
        private Letter[] allLetters;
        public UnityEvent OnSolved;
        void Start()
        {
            allLetters = letters.Concat(wrongLetters).ToArray();
            foreach (Letter letter in allLetters)
            {
                letter.OnLitUp.AddListener(OnLit);
                letter.OnDeactivated.AddListener(OnLetterDeactivated);
            }

            foreach (Letter letter in letters)
            {
                OnSolved.AddListener(letter.MarkSolved);
            }
        }

        void OnLit(Letter letter)
        {
            if (letters.Contains(letter))
            {
                numberOfCorrectLettersLit++;
            }
            numberOfLettersLit++;
            if (numberOfLettersLit == letters.Length)
            {
                CheckWord();
            }
        }

        private void CheckWord()
        {
            if (numberOfCorrectLettersLit == numberOfLettersLit && numberOfLettersLit == letters.Length)
            {
                CorrectWord();
                return;
            }
            WrongWord();
        }

        public void OnLetterDeactivated(Letter letter)
        {
            if (letters.Contains(letter))
            {
                numberOfCorrectLettersLit = letters.Sum((Letter _letter) => _letter.IsLitUp ? 1 : 0);
            }
            numberOfLettersLit = allLetters.Sum((Letter _letter) => _letter.IsLitUp ? 1 : 0); ;
        }

        private void CorrectWord()
        {
            OnSolved.Invoke();
        }

        private void WrongWord()
        {
            DeactivateAll();
        }

        private void DeactivateAll()
        {
            numberOfLettersLit = 0;
            numberOfCorrectLettersLit = 0;
            foreach (Letter letter in allLetters)
            {
                letter.ForceDeactivate();
            }
        }

        private void MarkAllAsSolved()
        {
            numberOfLettersLit = 0;
            numberOfCorrectLettersLit = 0;
            foreach (Letter letter in letters)
            {
                letter.MarkSolved();
            }
            foreach (Letter letter in wrongLetters)
            {
                letter.gameObject.SetActive(false);
            }
        }

    }
}
