#if UNITY_EDITOR_WIN
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    [SerializeField] [TextArea] private string _memo;
    [SerializeField] private bool _lock;
}

