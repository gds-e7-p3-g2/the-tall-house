using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    [System.Serializable] public class ViewerEvent : UnityEvent<Viewer> { };
    public class Viewer
    {
        public bool canInvite = true;
        private int numOfFriends;
        private float __satisfaction;
        public ViewerEvent OnLeave = new ViewerEvent();
        public ViewerEvent OnInvite = new ViewerEvent();
        public float satisfaction
        {
            get { return __satisfaction; }
            set
            {
                __satisfaction = Mathf.Min(100f, Mathf.Max(0f, value));

                // if (__satisfaction <= 0)
                // {
                //     Leave();
                // }
                if (__satisfaction >= 70)
                {
                    InviteFriend();
                }
            }
        }
        public Viewer(int _satisfaction = 50, int _numOfFriends = 2)
        {
            satisfaction = _satisfaction;
            numOfFriends = _numOfFriends;
        }
        public void Leave()
        {
            // OnLeave.Invoke(this);
        }
        public void InviteFriend()
        {
            if (!canInvite || numOfFriends <= 0)
            {
                return;
            }
            numOfFriends--;
            canInvite = false;

            OnInvite.Invoke(this);
        }
        public void ReactToEvent(float EventAttractiveness)
        {
            bool DoIEvenCare = Random.Range(1, 100) < Mathf.Min(satisfaction, 30);

            if (!DoIEvenCare)
            {
                return;
            }

            satisfaction += EventAttractiveness;
        }
    }
    // public class ViewersController : MonoBehaviour
    // {
    //     public List<Viewer> Viewers = new List<Viewer>();
    //     private int viewersToAdd = 0;
    //     public int CurrentNumberOfViewers { get { return Viewers.Count; } }
    //     [SerializeField] int InitialViewers = 5;
    //     [SerializeField] int InitialSatisfaction = 50;
    //     [SerializeField] int NumberOfViewerFriends = 1;
    //     [SerializeField] int InvitationCooldown = 10;
    //     [SerializeField] IntEvent OnViewersCountChanged;

    //     void Start()
    //     {
    //         viewersToAdd = InitialViewers;
    //         AddViewers();
    //     }
    //     public void ReactToEvent(float EventAttractiveness)
    //     {
    //         Viewers.ForEach(viewer => viewer.ReactToEvent(EventAttractiveness));
    //         Viewers.FindAll(viewer => viewer.satisfaction <= 0).ForEach(RemoveViewer);
    //         AddViewers();
    //     }
    //     private void AddViewers()
    //     {
    //         for (int i = 0; i < viewersToAdd; i++)
    //         {
    //             AddViewer();
    //         }

    //         viewersToAdd = 0;
    //     }

    //     private Viewer BuildViewer()
    //     {
    //         Viewer viewer = new Viewer(InitialSatisfaction, NumberOfViewerFriends);

    //         viewer.OnInvite.AddListener(OnInvite);
    //         viewer.OnLeave.AddListener(RemoveViewer);

    //         return viewer;
    //     }

    //     private void OnInvite(Viewer viewer)
    //     {
    //         viewersToAdd++;
    //         StartCoroutine(Cooldown(viewer));
    //     }

    //     private IEnumerator Cooldown(Viewer viewer)
    //     {
    //         yield return new WaitForSeconds(InvitationCooldown);
    //         viewer.canInvite = true;
    //     }

    //     private void AddViewer()
    //     {
    //         Viewers.Add(BuildViewer());
    //         OnViewersCountChanged.Invoke(CurrentNumberOfViewers);
    //     }

    //     private void RemoveViewer(Viewer viewer)
    //     {
    //         viewer.OnInvite.RemoveAllListeners();
    //         viewer.OnLeave.RemoveAllListeners();
    //         Viewers.Remove(viewer);
    //         OnViewersCountChanged.Invoke(CurrentNumberOfViewers);

    //         if (CurrentNumberOfViewers <= 0)
    //         {
    //             StoryEvents.Instance.OnLostAllViewers.Invoke();
    //         }
    //     }
    // }

    public class ViewersController : MonoBehaviour
    {
        [SerializeField] int InitialViewers = 5;
        private int numOfViewers;
        [SerializeField] int InitialPopularity = 50;
        [SerializeField] IntEvent OnViewersCountChanged;

        public void ReactToEvent(float EventAttractiveness)
        {
            numOfViewers += (int)EventAttractiveness;

            Debug.Log("WIZOWIE " + numOfViewers);

            OnViewersCountChanged.Invoke(numOfViewers);
        }


    }
}