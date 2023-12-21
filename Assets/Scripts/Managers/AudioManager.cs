using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public static AudioManager Instance {
        get
        {
            return _instance;
        }
    }
    private static AudioManager _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy( this.gameObject);
        }
    }

    public void PlayDialogue(AudioClip dialogueClip)
    {
        _audioSource.PlayOneShot(dialogueClip);
    }
}
