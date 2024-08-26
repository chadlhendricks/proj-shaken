using UnityEngine;

public class BulletDestroySelf : MonoBehaviour
{
    void Start()
    {
        Destroy(this, 5f);
    }    
}
