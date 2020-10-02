using UnityEngine;

namespace IStreamYouScream
{
    public class ViewersController : MonoBehaviour
    {
        [SerializeField] int InitialViewers = 500;
        private float numOfViewersf;
        private int numOfViewers;
        [SerializeField] int InitialPopularity = 50;
        [SerializeField] IntEvent OnViewersCountChanged;

        private void Start()
        {
            numOfViewersf = (float)InitialViewers;
            numOfViewers = (int)numOfViewersf;
            OnViewersCountChanged.Invoke(numOfViewers);
        }

        public void ReactToEvent(float EventAttractiveness)
        {
            int prev = numOfViewers;

            numOfViewersf += EventAttractiveness;
            numOfViewers = (int)numOfViewersf;

            if (prev < numOfViewers)
            {
                SoundsController.Instance.findSound("MoreViewers").Play();
            }
            else if (prev > numOfViewers)
            {
                SoundsController.Instance.findSound("LessViewers").Play();
            }

            OnViewersCountChanged.Invoke(numOfViewers);

            if (numOfViewers <= 0)
            {
                StoryEvents.Instance.OnLostAllViewers.Invoke();
            }
        }
    }
}