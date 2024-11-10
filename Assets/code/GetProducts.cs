using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
public class GetProducts : MonoBehaviour
{
    // Start is called before the first frame update
    private string URL = "https://homework.mocart.io/api/products";
    public RectTransform contentPanel;
    public GameObject prefabPro;
    void Start()
    {
        StartCoroutine(Getproducts());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Getproducts()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(json);
                Globals.product_count = node["products"].Count;
                Globals.products = new Product[node["products"].Count];
                for (int i = 0; i < node["products"].Count; i++)
                {
                    JSONNode pro = node["products"][i];
                    Globals.products[i] = new Product(pro["name"], pro["description"], pro["price"]);
                    GameObject product = Instantiate(prefabPro, Vector3.zero, Quaternion.identity);
                    product.transform.parent = contentPanel;
                    product.transform.localScale = Vector3.one;
                    product.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = Globals.products[i].name;
                    product.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = Globals.products[i].description;
                    product.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = Globals.products[i].price;
                }
            }
        }
    }
}
