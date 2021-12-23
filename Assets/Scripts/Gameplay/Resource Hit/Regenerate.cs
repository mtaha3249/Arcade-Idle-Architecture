using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Regenerate : MonoBehaviour
{
    public float regenerateTime = 2f;

    public UnityEvent RegenerateSuccessful;

    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(regenerateTime);
    }

    public void StartRegeneration()
    {
        Invoke("WaitInvoke", regenerateTime);
        // StartCoroutine(RegenerateRoutine());
    }

    IEnumerator RegenerateRoutine()
    {
        yield return _waitForSeconds;
        RegenerateSuccessful.Invoke();
    }

    void WaitInvoke()
    {
        RegenerateSuccessful.Invoke();
    }
}