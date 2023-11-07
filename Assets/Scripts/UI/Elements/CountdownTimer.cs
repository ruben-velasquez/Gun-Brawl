using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    private Animator countdownAnimator;

    private void Start()
    {
        countdownAnimator = GetComponent<Animator>();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        countdownAnimator.Play("Count");

        yield return new WaitForSeconds(4f);

        countdownAnimator.gameObject.SetActive(false);
    }
}