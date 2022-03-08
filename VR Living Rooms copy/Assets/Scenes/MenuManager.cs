using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text PINText;
    public Text errorText;
    public Text joinTestText;
    public Text createTestText;
    public GameObject PINInput;
    public GameObject avitarSelectionJoin;
    public string correctPin;
    private string PIN = "";
    private string roomID;
    public string defaltRoomID;
    public string defaltServerID;
    public string defaltCreatingAvitar;
    private string joiningAvitar;
    private string creatingAvitar;

    public void setRoomCafe(){
        roomID = "Cafe";
    }

    public void setRoomPark(){
        roomID = "Park";
    }

    public void setRoomHome(){
        roomID = "Home";
    }

    public void setCreatingAvitar1(){
        creatingAvitar = "Avitar 1";
        string test = "Room: " + roomID + " Avitar : " + creatingAvitar;
        createTestText.text = test;
    }

    public void setCreatingAvitar2(){
        creatingAvitar = "Avitar 2";
        string test = "Room: " + roomID + " Avitar : " + creatingAvitar;
        createTestText.text = test;
    }

    public void setCreatingAvitar3(){
        creatingAvitar = "Avitar 3";
        string test = "Room: " + roomID + " Avitar : " + creatingAvitar;
        createTestText.text = test;
    }
        public void setJoiningAvitar1(){
        joiningAvitar = "Avitar 1";
        string test = "Room: " + roomID + " Avitar in room : " + creatingAvitar + " Avitar joining : " + joiningAvitar;
        joinTestText.text = test;
    }

    public void setJoiningAvitar2(){
        joiningAvitar = "Avitar 2";
        string test = "Room: " + roomID + " Avitar in room : " + creatingAvitar + " Avitar joining : " + joiningAvitar;
        joinTestText.text = test;
    }

    public void setJoiningAvitar3(){
        joiningAvitar = "Avitar 3";
        string test = "Room: " + roomID + " Avitar in room : " + creatingAvitar + " Avitar joining : " + joiningAvitar;
        joinTestText.text = test;
    }

    public void doExitGame() {
        Application.Quit();
    }

    public void SubmitPIN(){
        if (PIN.Length != 4){
            errorText.text = "PIN should be 4 characters";
        }
        else if (PIN == correctPin){
            PINInput.SetActive(false);
            avitarSelectionJoin.SetActive(true);
        }
        else{
            errorText.text = "Invalid PIN please retry";
        }
    }
    public void Add0(){
        if (PIN.Length < 4){
            PIN += "0";
        }
        
    }
    public void Add1(){
        if (PIN.Length < 4){
            PIN += "1";
        }
        
    }
    public void Add2(){
        if (PIN.Length < 4){
            PIN += "2";
        }
    }
    public void Add3(){
        if (PIN.Length < 4){
            PIN += "3";
        }
        
    }
    public void Add4(){
        if (PIN.Length < 4){
            PIN += "4";
        }
        
    }
    public void Add5(){
        if (PIN.Length < 4){
            PIN += "5";
        }
        
    }
    public void Add6(){
        if (PIN.Length < 4){
            PIN += "6";
        }
        
    }
    public void Add7(){
        if (PIN.Length < 4){
            PIN += "7";
        }
        
    }
    public void Add8(){
        if (PIN.Length < 4){
            PIN += "8";
        }
        
    }
    public void Add9(){
        if (PIN.Length < 4){
            PIN += "8";
        }
    }
    public void Del(){
        if (PIN != ""){
            PIN = PIN.Remove(PIN.Length - 1);
        }
    }

    public void Clear(){
        PIN = "";
        errorText.text = "";
        joiningAvitar = "";
        creatingAvitar = "";
        roomID = "";
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

