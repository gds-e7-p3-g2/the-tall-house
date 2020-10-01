using UnityEngine;

namespace IStreamYouScream
{
    public class ViewersController : MonoBehaviour
    {
        [SerializeField] int InitialViewers = 5;
        private float numOfViewersf;
        private int numOfViewers;
        [SerializeField] int InitialPopularity = 50;
        [SerializeField] IntEvent OnViewersCountChanged;

        public void ReactToEvent(float EventAttractiveness)
        {
            numOfViewersf += EventAttractiveness;
            numOfViewers = (int)numOfViewersf;

            OnViewersCountChanged.Invoke(numOfViewers);

            if (numOfViewers <= 0)
            {
                StoryEvents.Instance.OnLostAllViewers.Invoke();
            }
        }
    }
}