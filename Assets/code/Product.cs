using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public string name;
    public string description;
    public string price;
    public Product(string name,string description, string price)
    {
        this.name = name;
        this.description = description;
        this.price = price;
    }
}
