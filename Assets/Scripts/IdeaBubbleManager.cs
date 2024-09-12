using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class IdeaBubbleManager : MonoBehaviour
{
    [SerializeField] private Idea[] _ideas;
    [SerializeField] private UnityEvent _onIdeasCompleted;
    
    private IdeaBubble[] _ideaBubbles;
    
    private void Start()
    {
        _ideaBubbles = GetComponentsInChildren<IdeaBubble>();
        Debug.Assert(_ideaBubbles.Length == _ideas.Length, "Idea bubbles and ideas length mismatch");
        
        for (int i = 0; i < _ideaBubbles.Length; i++)
        {
            _ideaBubbles[i].SetIdea(_ideas[i]);
            _ideaBubbles[i].gameObject.SetActive(false);
        }
    }

    public void DisplayIdeas()
    {
        foreach (var ideaBubble in _ideaBubbles)
        {
            ideaBubble.gameObject.SetActive(true);
        }
        
        StartCoroutine(AwaitIdeasCompleted());
    }

    private IEnumerator AwaitIdeasCompleted()
    {
        yield return new WaitUntil(() => _ideaBubbles.All(x => x.IsCompleted()));
        _onIdeasCompleted.Invoke();
    }
}