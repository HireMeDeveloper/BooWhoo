using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UIManager.Instance.StartScene());
    }

}
