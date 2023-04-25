using UnityEngine;

public class MatchTrigger : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.StartMatch();
    }
}