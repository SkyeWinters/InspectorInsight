using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class EvidenceObject : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private SnapOnRelease _snapOnRelease;
    [SerializeField] private Transform _linkPoint;

    private bool _onBoard;
    private AudioSource _audioSource;
    private Action<AudioClip> _playClip;
    private XRGrabInteractable _grabInteractable;
    private bool _isGrabbed;
    private Article _data;
    
    public static int TotalEvidencePlaced = 0;

    public void SetEvidenceData(Article data, Action<AudioClip> playClip, SnapPoint snapCollider)
    {
        _nameText.text = data.Title;
        _data = data;
        _descriptionText.text = data.Description;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = data.Audio;
        _playClip = playClip;
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _snapOnRelease.SetSnapCollider(snapCollider);
    }
    
    public void OnGrab()
    {
        _isGrabbed = true;
        if (IdeaBubble.SelectedIdea != null)
        {
            IdeaBubble.SelectedIdea.LinkTo(_data, _linkPoint);
            IdeaBubble.SelectedIdea.StopSelecting();
        }
    }

    public void OnKeyPress(InputAction.CallbackContext context)
    {
        if (!_isGrabbed) return;
        if (context.started)
        {
            _playClip?.Invoke(_audioSource.clip);
        }
    }

    public void OnRelease()
    {
        _isGrabbed = false;
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