using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    public class PaintingsRiddle : MonoBehaviour
    {
        public FamilyPainting[] paintings = new FamilyPainting[5];
        public UnityEvent OnSolved;

        private int numberOfLitPaintings = 0;

        void Start()
        {
            // foreach (FamilyPainting painting in paintings)
            // {
            //     painting.OnDeactivated.AddListener(DeactivateAll);
            // }
        }

        public void OnPaintingLit(FamilyPainting painting)
        {
            if (painting != paintings[numberOfLitPaintings])
            {
                WrongNumber();
                return;
            }
            numberOfLitPaintings++;
            if (numberOfLitPaintings == paintings.Length)
            {
                CorrectNumber();
            }
        }

        public void OnPaintingDeactivated()
        {
            TimesOut();
        }

        private void CorrectNumber()
        {
            OnSolved.Invoke();
        }

        private void TimesOut()
        {
            DeactivateAll();
        }

        private void WrongNumber()
        {
            DeactivateAll();
        }

        private void DeactivateAll()
        {
            numberOfLitPaintings = 0;
            foreach (FamilyPainting painting in paintings)
            {
                painting.ForceDeactivate();
            }
        }

        private void MarkAllAsSolved()
        {
            numberOfLitPaintings = 0;
            foreach (FamilyPainting painting in paintings)
            {
                painting.MarkSolved();
            }
        }

    }
}