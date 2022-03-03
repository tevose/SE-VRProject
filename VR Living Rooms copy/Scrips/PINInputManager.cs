using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PINInputManager : MonoBehaviour
{
    public Text PINText;
    public string PIN = "";

    public void SubmitPIN(){
        Debug.Log(PIN);
    }
    public void Add0(){
        PIN += "0";
        Debug.Log(PIN);
        
    }
    public void Add1(){
        PIN += "1";
        Debug.Log(PIN);
        
    }
    public void Add2(){
        PIN += "2";
        Debug.Log(PIN);
        
    }
    public void Add3(){
        PIN += "3";
        Debug.Log(PIN);
        
    }
    public void Add4(){
        PIN += "4";
        Debug.Log(PIN);
        
    }
    public void Add5(){
        PIN += "5";
        Debug.Log(PIN);
        
    }
    public void Add6(){
        PIN += "6";
        Debug.Log(PIN);
        
    }
    public void Add7(){
        PIN += "7";
        Debug.Log(PIN);
        
    }
    public void Add8(){
        PIN += "8";
        
    }
    public void Add9(){
        PIN += "9";
    }
    public void Del(){
        if (PIN != ""){
            PIN = PIN.Remove(PIN.Length - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PIN == ""){
            PINText.text = "Enter PIN -";
        }else{
            PINText.text = PIN;
        }
    }
}
