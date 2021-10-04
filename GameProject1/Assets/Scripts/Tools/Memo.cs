#if UNITY_EDITOR_WIN

using UnityEngine;

public class Memo : MonoBehaviour
{
    [SerializeField] [TextArea] private string _memo;
    [SerializeField] private bool _lock;
}

#endif