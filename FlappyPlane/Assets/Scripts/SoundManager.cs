using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerDieSound;
    static AudioSource audioSrc;

    void Start()
    {
        playerDieSound = Resources.Load<AudioClip>("PlayerDie");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "die":
                audioSrc.PlayOneShot(playerDieSound);
                break;
        }
    }
}
