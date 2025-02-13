using UnityEngine;

public enum TypeOfSound
{
    Music,
    UIButton,
    Shoot,
    Shoot_Hit,
    Button,
    Light,
    Teleport,
}

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundsList;
    public static AudioManager instance;
    private AudioSource _source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        //https://www.youtube.com/watch?v=xswEpNpucZQ
    }



    private void Start()
    {
        _source = GetComponent<AudioSource>();
        PlaySound(TypeOfSound.Music, 1f);
    }

    public void PlaySound(TypeOfSound sound, float volume = 1)
    {
        instance._source.PlayOneShot(instance._soundsList[(int)sound], volume);
    }
}