using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnapToItem : MonoBehaviour
{
    // A class that is responsible for scrolling between product list
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public HorizontalLayoutGroup HLG;
    public TMP_Text NameLabel;
    public TMP_InputField NAME_IF;
    public TMP_InputField DESCRIPTION_IF;
    public TMP_InputField PRICE_IF;
    public Button Edit_b;
    TouchScreenKeyboard keyboard;
    public string[] ItemNames;
    bool isSnapped = false;
    bool EditMode = false;
    public float snapForce;
    float snapSpeed;
    public int typeKeyboard;
    public bool selectedIF = false;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));
        Debug.Log(currentItem);
        if (scrollRect.velocity.magnitude < 200 && !isSnapped)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed), contentPanel.localPosition.y, contentPanel.localPosition.z);
            if (contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
            {
                isSnapped = true;
            }

        }
        if (scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }
        if (Application.isMobilePlatform)
        { // check if it is mobile, editor,desktop behaves differently when keyboard used
            if (selectedIF && keyboard != null)
            {
                GameObject SelectedProduct = contentPanel.GetChild(currentItem).gameObject;
                SelectedProduct.transform.GetChild(typeKeyboard).GetComponent<TMPro.TMP_Text>().text = keyboard.text;
                if (typeKeyboard == 0)
                {
                    NAME_IF.text = keyboard.text;
                }
                else if (typeKeyboard == 1)
                {
                    DESCRIPTION_IF.text = keyboard.text;
                }
                else if (typeKeyboard == 2)
                {
                    PRICE_IF.text = keyboard.text;
                }
                else
                {
                    NAME_IF.text = keyboard.text;
                }
            }
        }
    }
    public void openKeyboard(int t)
    {
        //open touch keyboard when mobile platform available
        typeKeyboard = t;
        string textToEdit = "";
        selectedIF = true;
        if (t == 0)
        {
            textToEdit = NAME_IF.text;
        }
        else if (typeKeyboard == 1)
        {
            textToEdit = DESCRIPTION_IF.text;
        }
        else if (typeKeyboard == 2)
        {
            textToEdit = PRICE_IF.text;
        }
        else if(typeKeyboard == 3)
        {
            typeKeyboard = 0;
        }
        if (TouchScreenKeyboard.isSupported)
        {
            keyboard = TouchScreenKeyboard.Open(textToEdit, TouchScreenKeyboardType.Default);
        }
    }
    public void EditAndConfirmProduct()
    {
        // Edit and Confirm the changes made for product's name,description,price
        EditMode = !EditMode;
        if (EditMode)
        {
            //change mode to edit mode
            scrollRect.horizontal = false;
            Edit_b.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "CONFIRM";
            int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));
            GameObject SelectedProduct = contentPanel.GetChild(currentItem).gameObject;
            NAME_IF.gameObject.SetActive(true);
            DESCRIPTION_IF.gameObject.gameObject.SetActive(true);
            PRICE_IF.gameObject.gameObject.SetActive(true);
            string name = SelectedProduct.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text;
            string description = SelectedProduct.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text;
            string price = SelectedProduct.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text;

            NAME_IF.text = name;
            DESCRIPTION_IF.text = description;
            PRICE_IF.text = price;

            SelectedProduct.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(false);
            SelectedProduct.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(false); 
            SelectedProduct.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(false);

        }
        else
        {
            //change mode to confirm mode
            selectedIF = false;
            scrollRect.horizontal = true;
            Edit_b.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "EDIT";
            int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)));
            GameObject SelectedProduct = contentPanel.GetChild(currentItem).gameObject;


            string name = NAME_IF.text;
            string description = DESCRIPTION_IF.text;
            string price = PRICE_IF.text;

            NAME_IF.gameObject.SetActive(false);
            DESCRIPTION_IF.gameObject.SetActive(false);
            PRICE_IF.gameObject.SetActive(false);

            SelectedProduct.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(true);
            SelectedProduct.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(true);
            SelectedProduct.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().gameObject.SetActive(true);

            SelectedProduct.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = name;
            SelectedProduct.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = description;
            SelectedProduct.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = price;

        }
    }
}
