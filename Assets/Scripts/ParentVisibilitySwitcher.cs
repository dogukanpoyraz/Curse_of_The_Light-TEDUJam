using UnityEngine;

public class ParentVisibilitySwitcher : MonoBehaviour
{
    public GameObject group2, group3, group4;

    void Start()
    {
        HideGroup(group2);
        HideGroup(group3);
        HideGroup(group4);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowGroup(group2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShowGroup(group3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShowGroup(group4);
        }
    }

    void HideGroup(GameObject group)
    {
        foreach (Renderer r in group.GetComponentsInChildren<Renderer>())
        {
            r.enabled = false; 
        }
    }

    void ShowGroup(GameObject group)
    {

        HideGroup(group2);
        HideGroup(group3);
        HideGroup(group4);


        foreach (Renderer r in group.GetComponentsInChildren<Renderer>())
        {
            r.enabled = true; 
        }
    }
}