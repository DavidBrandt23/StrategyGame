using UnityEngine;
using UnityEngine.Events;

public class OnClickEvent : UnityEvent<GameObject> { };

public class Clickable : MonoBehaviour
{
    public OnClickEvent OnClicked = new OnClickEvent();
    public UnityEvent OnEnableEvent = new UnityEvent();
    public UnityEvent OnDisableEvent = new UnityEvent();
    public BoxCollider2D myBoxCollider2D;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnMouseUp()
    {
        if (isActiveAndEnabled)
        {
            OnClicked.Invoke(gameObject);
        }
    }

    void OnEnable()
    {
        if (myBoxCollider2D == null)
        {
            myBoxCollider2D = GetComponent<BoxCollider2D>();
        }
        OnEnableEvent.Invoke();
        myBoxCollider2D.enabled = true;
    }

    void OnDisable()
    {
        OnDisableEvent.Invoke();
        myBoxCollider2D.enabled = false;
    }
}
