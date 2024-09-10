using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EvidenceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;

    private bool _fired;
    private AudioSource _audioSource;
    private Action<AudioClip> _playClip;
    
    public void SetEvidenceData(Article data, Action<AudioClip> playClip)
    {
        _nameText.text = data.Title;
        _descriptionText.text = data.Description;
        _fired = false;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = data.Audio;
        _playClip = playClip;
    }
    
    public void PlayAudio()
    {
        if (_fired) return;
        _fired = true;
        _playClip?.Invoke(_audioSource.clip);
    }
}