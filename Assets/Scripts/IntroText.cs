using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour
{
    [SerializeField]
    TextMesh[] texts;
    [SerializeField]
    TextMesh skipText;

    [SerializeField]
    Transform toSkip;
    [SerializeField]
    Transform skipLocation;

    KeyCode skipKey = KeyCode.Space;

    void Start()
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].gameObject.SetActive(false);
        skipText.gameObject.SetActive(true);

        StartCoroutine(DisplayTexts());
    }

    void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            StopAllCoroutines();

            toSkip.position = skipLocation.position;

            Destroy(gameObject);
        }
    }

    IEnumerator DisplayTexts()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        Destroy(gameObject);
    }
}
