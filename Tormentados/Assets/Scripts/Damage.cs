using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public float lifetime;
    public TextMeshPro textMesh;
    public GameObject effect;

    public void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Start()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Invoke("Destruction", lifetime);
    }
    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
    }

    public void Destruction()
    {
        
        Destroy(gameObject);
    }
}
