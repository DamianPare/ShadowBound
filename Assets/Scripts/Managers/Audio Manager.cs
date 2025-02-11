using UnityEngine;

public enum TypeOfSound
{
    Music,
    Teleport,
    UIButton,
    Button,
    Light,
    Shoot,
}

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundsList;
    public static AudioManager instance;
    private AudioSource _source;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(TypeOfSound sound, float volume = 1)
    {
        instance._source.PlayOneShot(instance._soundsList[(int)sound], volume);
    }
}