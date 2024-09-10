using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class EvidenceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;

    private bool _fired;
    private bool _onBoard;
    private AudioSource _audioSource;
    private Action<AudioClip> _playClip;
    private XRGrabInteractable _grabInteractable;
    
    public static int TotalEvidencePlaced = 0;
    
    public void SetEvidenceData(Article data, Action<AudioClip> playClip)
    {
        _nameText.text = data.Title;
        _descriptionText.text = data.Description;
        _fired = false;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = data.Audio;
        _playClip = playClip;
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }
    
    public void PlayAudio()
    {
        if (_fired) return;
        _fired = true;
        _playClip?.Invoke(_audioSource.clip);
    }

    public void DisableMoving()
    {
        _grabInteractable.trackPosition = false;
        _grabInteractable.trackRotation = false;
        _grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
    }

    public void AddToBoard()
    {
        if (_onBoard) return;
        _onBoard = true;
        TotalEvidencePlaced++;
    }
    
    public void RemoveFromBoard()
    {
        if (!_onBoard) return;
        _onBoard = false;
        TotalEvidencePlaced--;
    }
}