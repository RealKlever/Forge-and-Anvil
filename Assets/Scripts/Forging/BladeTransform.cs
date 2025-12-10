using UnityEngine;

public class BladeTransform : MonoBehaviour
{
    private Transform handleTransform;
    public GameObject blade;
    public AudioSource bladeFall;

    private void Start()
    {
        handleTransform = this.transform;
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
                handleTransform = metal.transform;
                Destroy(other.gameObject);
                Instantiate(blade, handleTransform.position, handleTransform.rotation);
                bladeFall.Play();
            }
        }
    }
}
