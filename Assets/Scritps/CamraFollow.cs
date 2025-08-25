using UnityEngine;
using Unity.Netcode;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}