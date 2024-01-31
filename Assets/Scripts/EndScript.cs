using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    [SerializeField] GameObject Illu;
    [SerializeField] GameObject Certificat;
    [SerializeField] GameObject tampon;
    [SerializeField] TextMeshProUGUI name;
    [Space]
    [SerializeField] Image photo;

    private void Awake()
    {
        Load();
    }

    public void Load()
    {

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
