using UnityEngine;

public class Item : MonoBehaviour
{

    public void Pickup()
    {
        Destroy(gameObject);
    }
}
