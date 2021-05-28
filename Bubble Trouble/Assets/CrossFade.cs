using UnityEngine;

public class CrossFade : MonoBehaviour
{
    public void StartLoad()
    {
        Launcher.instance.completeLoad = true;
    }
}
