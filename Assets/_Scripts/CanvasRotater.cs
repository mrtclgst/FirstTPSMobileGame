using UnityEngine;
using UnityEngine.UI;

public class CanvasRotater : MonoBehaviour
{
    [SerializeField] Transform enemyTransform;
    private void Update()
    {
        transform.position = enemyTransform.position + new Vector3(0, 2, 0);
    }
}
