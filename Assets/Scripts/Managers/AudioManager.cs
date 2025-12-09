using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    private AudioSource audioSource;
    private Sounds sounds;

    void Awake()
    {
        Instance = this;
        sounds = Resources.Load<Sounds>("Sounds");
        audioSource = GetComponent<AudioSource>();

    }

    void OnValidate()
    {
        bool audioSourceExists = TryGetComponent<AudioSource>(out audioSource);
        if (!audioSourceExists) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio, float volume = 1)
    {
        audioSource.PlayOneShot(audio, volume);
    }

    private int perfectBlockIndex = 0;
    private int regularBlockIndex = 0;
    private int meleeSoundIndex = 0;


    public IEnumerator PlayDelayedSound(AudioClip audio, float delay, float volume = 1)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(audio, volume);
    }



    public AudioClip GetPerfectBlockClip()
    {
        AudioClip audioClip = perfectBlockIndex switch
        {
            0 => sounds.perfectBlock1,
            1 => sounds.perfectBlock2,
            _ => sounds.perfectBlock2
        };
        perfectBlockIndex = (perfectBlockIndex + 1) % 2;
        return audioClip;
    }

    public AudioClip GetRegularBlockClip()
    {
        AudioClip audioClip = regularBlockIndex switch
        {
            0 => sounds.regularBlock1,
            1 => sounds.regularBlock2,
            _ => sounds.regularBlock2
        };
        regularBlockIndex = (regularBlockIndex + 1) % 2;
        return audioClip;
    }

    public AudioClip GetMeleeSoundClip()
    {
        AudioClip audioClip = meleeSoundIndex switch
        {
            0 => sounds.meleeSound1,
            1 => sounds.meleeSound2,
            2 => sounds.meleeSound3,
            _ => sounds.meleeSound3
        };
        meleeSoundIndex = (meleeSoundIndex + 1) % 3;
        return audioClip;
    }
}
