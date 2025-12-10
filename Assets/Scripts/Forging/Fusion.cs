using UnityEngine;
using UnityEngine.UIElements;

public class Fusion : MonoBehaviour
{
    private Transform handleTransform;
    public GameObject sword;
    public AudioSource swordFall;

    private void Start()
    {
        handleTransform = this.transform;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blade"))
        {
            // Get the Blade component from the collided object
            Transform bladeTransform = collision.transform;
            handleTransform = bladeTransform;
            handleTransform.position = handleTransform.position + new Vector3(0, 1.5f, 0);
            Destroy(collision.gameObject);
            Instantiate(sword, handleTransform.position, handleTransform.rotation);
            swordFall.Play();
            Destroy(this.gameObject);
        }
    }
}
