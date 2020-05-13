using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    public bool Cinematic = false;
    public List<Camera> cameras = new List<Camera>();

    private int passage = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && passage == 0)
        {
            Cinematic = true;

            passage += 1;
        }
    }

    void Update()
    {
        if (Cinematic)
        {
            StartCoroutine(Intro());
            Cinematic = false;
        }
    }

    IEnumerator Intro()
    {
        Linker.instance.characterManager.isInactive = true;

        for (int i = 0; i <= cameras.Count - 1; i++)
        {
            if (i > 0)
                cameras[i - 1].gameObject.SetActive(false);

            if (i < cameras.Count)
                cameras[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(8f);
        }
        Linker.instance.characterManager.isInactive = false;

        Destroy(gameObject);
    }
}
