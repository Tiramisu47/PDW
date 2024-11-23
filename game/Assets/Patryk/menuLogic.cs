using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuLogic : MonoBehaviour
{
    [SerializeField] private GameObject m1;
    [SerializeField] private GameObject m2;
    [SerializeField] private GameObject s1;
    [SerializeField] private GameObject s2;
    [SerializeField] private GameObject s3;
    private void Start()
    {
        m1.SetActive(true);
        m2.SetActive(false);
        s1.SetActive(false);
        s2.SetActive(false);
        s3.SetActive(false);

    }
    public void SettingsSelect()
    {
        m1.SetActive(false);
        m2.SetActive(true);
        s1.SetActive(true);
        s2.SetActive(false);
        s3.SetActive(false);
    }
    public void BackSelect()
    {
        m1.SetActive(true);
        m2.SetActive(false);
    }
    public void DisplaySelect()
    {
        s2.SetActive(true);
        s3.SetActive(false);
        s1.SetActive(false);
    }
    public void GraphicsSelect()
    {
        s1.SetActive(true);
        s2.SetActive(false);
        s3.SetActive(false);
    }
    public void ControlSelect()
    {
        s3.SetActive(true);
        s1.SetActive(false);
        s2.SetActive(false);
    }

}
