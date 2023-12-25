using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectiveComplete : MonoBehaviour
{
    public bool Complete;
    public string TextComplete;
    public TextMeshProUGUI Text;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Complete = true;
            Text.text = TextComplete.ToString();
            StartCoroutine("WaitForSec");
        }
    }
    void Start()
    {
        Text = Text.GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        DestroyImmediate(Text);
    }
}
