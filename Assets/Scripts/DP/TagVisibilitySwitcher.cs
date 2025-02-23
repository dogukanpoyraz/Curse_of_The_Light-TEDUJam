using System.Collections;
using UnityEngine;

public class TagVisibilitySwitcher : MonoBehaviour
{
    public string group2Tag = "EditorOnly";
    public string group3Tag = "Respawn";
    public string group4Tag = "Enemy";

    private Coroutine activeCoroutine = null;
    private string activeTag = "";

    void Start()
    {
        HideGroup(group2Tag);
        HideGroup(group3Tag);
        HideGroup(group4Tag);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopCurrentVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartNewVisibility(group2Tag);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartNewVisibility(group3Tag);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartNewVisibility(group4Tag);
        }
    }

    void HideGroup(string tag)
    {
        GameObject[] groupObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in groupObjects)
        {
            foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
        }
    }

    void StartNewVisibility(string tag)
    {
        StopCurrentVisibility();

        activeTag = tag;
        activeCoroutine = StartCoroutine(FadeInGroup(tag));
    }

    void StopCurrentVisibility()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        if (!string.IsNullOrEmpty(activeTag))
        {
            HideGroup(activeTag);
            activeTag = "";
        }
    }

    IEnumerator FadeInGroup(string tag)
    {
        HideGroup(group2Tag);
        HideGroup(group3Tag);
        HideGroup(group4Tag);

        GameObject[] groupObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in groupObjects)
        {
            foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
            {
                r.enabled = true;
                Color color = r.material.color;
                color.a = 0f;
                r.material.color = color;
            }
        }

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);

            foreach (GameObject obj in groupObjects)
            {
                foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
                {
                    Color color = r.material.color;
                    color.a = alpha;
                    r.material.color = color;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(4.5f);

        if (activeTag == tag)
        {
            HideGroup(tag);
            activeTag = "";
            activeCoroutine = null;
        }
    }
}