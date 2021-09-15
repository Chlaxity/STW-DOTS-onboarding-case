using UnityEngine;

public class CameraMovement : MonoBehaviour
{
#region singleton
    public static CameraMovement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(instance);
        }
    }
    #endregion

    private Vector3 target = new Vector3(0,0,0);
    public Vector3 offset;
    [SerializeField]
    private float speed = 2;

    private void Update()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, target + offset, speed * Time.deltaTime);
    }

    /// <summary>
    /// Updates the target position
    /// </summary>
    /// <param name="targetPos"></param>
    public void SetTarget(Unity.Mathematics.float3 targetPos)
    {
        target = targetPos;
    }
}
