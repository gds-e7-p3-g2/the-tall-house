using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundEffect : MonoBehaviour
{
    private int currentIndex = 0;
    [SerializeField] bool playsRandom = true;
    public float cooldown = 0f;
    public float Volume = 1f;
    public List<AudioClip> clips = new List<AudioClip>();
    private bool isCollingDown = false;
    private AudioSource audio;

    private void Start()
    {
        if (audio == null)
        {
            AudioSource own = GetComponent<AudioSource>();
            AudioSource parents = gameObject.transform.parent.GetComponent<AudioSource>();
            audio = own != null ? own : (parents != null ? parents : gameObject.AddComponent<AudioSource>());
        }
    }
    public AudioClip GetRandom()
    {
        return clips[Random.Range(0, (clips.Count - 1))];
    }

    public AudioClip GetNext()
    {
        AudioClip current = clips[currentIndex];
        currentIndex = (currentIndex + 1) % clips.Count;
        return current;
    }

    public void PlayNext()
    {
        if (isCollingDown)
        {
            return;
        }
        _Play(GetNext());
    }
    public void PlayRnd()
    {
        if (isCollingDown)
        {
            return;
        }
        _Play(GetRandom());
    }

    public void Play()
    {

        if (playsRandom)
        {
            PlayRnd();
        }
        else
        {
            PlayNext();
        }
    }

    private void _Play(AudioClip clip)
    {
        audio.PlayOneShot(clip, Volume);
        StartCoroutine(WaitCooldown());
    }


    public IEnumerator WaitCooldown()
    {
        isCollingDown = true;
        yield return new WaitForSeconds(cooldown);
        isCollingDown = false;
    }
}
