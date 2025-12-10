using UnityEngine;

public class BladeTransform : MonoBehaviour
{
    private Transform metalTransform;
    public GameObject blade;
    public AudioSource bladeFall;

    private void Start()
    {
        metalTransform = this.transform;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
            // Get the Blade component from the collided object
            Metal metal = other.GetComponent<Metal>();
            if (metal != null && metal.hitsToMend == 0)
            {
                metalTransform = metal.transform;
                Destroy(other.gameObject);
                Instantiate(blade, metalTransform.position, metalTransform.rotation);
                bladeFall.Play();
            }
        }
    }
}
