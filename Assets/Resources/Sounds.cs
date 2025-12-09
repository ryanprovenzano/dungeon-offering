using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "Scriptable Objects/Sounds")]
public class Sounds : ScriptableObject
{
    [field: SerializeField] public AudioClip death { get; private set; }
    [field: SerializeField] public AudioClip magicBeam { get; private set; }
    [field: SerializeField] public AudioClip meleeSound1 { get; private set; }
    [field: SerializeField] public AudioClip meleeSound2 { get; private set; }
    [field: SerializeField] public AudioClip meleeSound3 { get; private set; }
    [field: SerializeField] public AudioClip perfectBlock1 { get; private set; }
    [field: SerializeField] public AudioClip perfectBlock2 { get; private set; }
    [field: SerializeField] public AudioClip regularBlock1 { get; private set; }
    [field: SerializeField] public AudioClip regularBlock2 { get; private set; }


    private static AudioClip GetAudioClip(string audioName)
    {
        return Resources.Load<AudioClip>("Sound/" + "MeleeSFX/" + audioName);
    }

    void OnEnable()
    {
        death = GetAudioClip("death");
        magicBeam = GetAudioClip("magicBeam");
        meleeSound1 = GetAudioClip("meleeSound1");
        meleeSound2 = GetAudioClip("meleeSound2");
        meleeSound3 = GetAudioClip("meleeSound3");
        perfectBlock1 = GetAudioClip("perfectBlock1");
        perfectBlock2 = GetAudioClip("perfectBlock2");
        regularBlock1 = GetAudioClip("regularBlock1");
        regularBlock2 = GetAudioClip("regularBlock2");

    }
}
