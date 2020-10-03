using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Ticker : MonoBehaviour
{
    public UnityEvent OnTick;
    public float interval;
    void Start()
    {
        Schedule();
    }

    void Schedule()
    {
        StartCoroutine(WaitAndTick());
    }

    private IEnumerator WaitAndTick()
    {
        yield return new WaitForSeconds(interval);

        OnTick.Invoke();

        Schedule();
    }
}
