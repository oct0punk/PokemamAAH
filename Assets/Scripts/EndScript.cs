using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField] GameObject Illu;
    [SerializeField] GameObject Certificat;
    [SerializeField] GameObject tampon;
    [SerializeField] new TextMeshProUGUI name;
    [Space]
    [SerializeField] Image photo;
    [SerializeField] Sprite rabbitAltern;
    [Header("Button")]
    [SerializeField] Button Cat;
    [SerializeField] Button Dog;
    [SerializeField] Button Pigeon;
    [SerializeField] Button Poule;
    [SerializeField] Button Rabbit;
    [SerializeField] Button Rat;
    [SerializeField] Button Cow;
    [SerializeField] Button Licorne;


    private void Awake()
    {
        Load();
        AudioManager.instance.Stop("villageThem");
        AudioManager.instance.Stop("fightThem");
        AudioManager.instance.Play("end");
    }

    public void Load()
    {
        StockManager stock = StockManager.instance;
        if (stock != null)
        {
            if (!stock.stock.ContainsValue(true)) 
            {
                NoAnimals(); 
                return;
            }
            Cat.interactable = stock.GetAnimalValue("Chat");
            Dog.interactable = stock.GetAnimalValue("Chien");
            Pigeon.interactable = stock.GetAnimalValue("Pigeon");
            Poule.interactable = stock.GetAnimalValue("Poule");
            Rabbit.interactable = stock.GetAnimalValue("Lapin");
            Rat.interactable = stock.GetAnimalValue("Rat");
            Cow.interactable = stock.GetAnimalValue("Vache");
            Licorne.interactable = stock.GetAnimalValue("Licorne");
        }

        if (Cow.interactable)
            Rabbit.GetComponent<Image>().sprite = rabbitAltern;
    }

    public void NoAnimals()
    {
        GoToMenu();
    }

    public void GoToMenu()
    {
        GameManager.instance.Menu();
    }

    public void Adopt(AnimalAsset animal)
    {
        Illu.SetActive(false);
        Certificat.SetActive(true);
        photo.sprite = animal.neutral;
        AudioManager.instance.Stop("end");
        AudioManager.instance.Play("adoption");
    }
    
    public void DownloadCertificat()
    {
        StartCoroutine(TakeSnapshot());
        AudioManager.instance.Play("tampon");
    }

    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    public IEnumerator TakeSnapshot()
    {
        tampon.SetActive(true);
        yield return frameEnd;
        DownloadImageFromTexture(CaptureScreen(), "Certificat d'adoption de " + name.text);
        yield return new WaitForSeconds(2);
        GoToMenu();
    }

    public Texture2D CaptureScreen()
    {
        // Capture d'écran des pixels de la vue de la caméra
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenTexture = new Texture2D(width, height);
        screenTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenTexture.Apply();

        return screenTexture;
    }

    public void DownloadImageFromTexture(Texture2D image, string fileName)
    {
        byte[] imageData = image.EncodeToJPG(); // Convertir la texture en tableau d'octets JPG

        string download = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads";

        string fullPath = Path.Combine(download, fileName + ".jpg");
        File.WriteAllBytes(fullPath, imageData);
        Debug.Log("Image téléchargée avec succès à l'emplacement : " + fullPath);
    }

}
