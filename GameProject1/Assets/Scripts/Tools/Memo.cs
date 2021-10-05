#if UNITY_EDITOR_WIN

using UnityEngine;

public class Memo : MonoBehaviour
{
    [SerializeField] [TextArea] private string memo;
    [SerializeField] private bool @lock;
}

#endif