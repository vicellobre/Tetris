using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, ConfigurationUtils.TimeExplosion);
    }
}