using UnityEngine;
using System.Collections;

public class Model:MonoBehaviour {
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        FreeView.Target = transform;
    }
}