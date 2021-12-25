using UnityEngine;

public class Selectable : MonoBehaviour
{
    public GameObject ObjectToShowWhenSelected;
    public bool toggleSelectWithClick;
    private bool isSelected;

    public void ToggleSelected()
    {
        SetIsSelected(!isSelected);
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void SetIsSelected(bool selected)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        isSelected = selected;
        if( ObjectToShowWhenSelected != null)
        {
            ObjectToShowWhenSelected.SetActive(isSelected);
        }
    }
    public void OnDisable()
    {
        SetIsSelected(false);
    }
    // Use this for initialization
    void Start()
    {
        SetIsSelected(isSelected);
        if (toggleSelectWithClick)
        {
            gameObject.AddComponent<Clickable>();
            Clickable clickable = GetComponent<Clickable>();
            //clickable.OnClicked.AddListener(ToggleSelected);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
