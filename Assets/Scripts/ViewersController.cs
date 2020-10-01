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
            Debug.Log("ReactToEvent " + EventAttractiveness);
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