using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField]
    Animation opening;
    [SerializeField]
    Animation closing;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.setTextBox(this.gameObject);
        anim = this.gameObject.GetComponent<Animator>();
    }

    public void openBox()
    {
        anim.Play("Opening");
    }
    public void closeBox()
    {
        anim.Play("Closing");
    }
}
