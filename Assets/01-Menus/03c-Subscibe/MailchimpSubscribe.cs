using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MailchimpSubscribe : MonoBehaviour {

  [System.Serializable]
  public class SubscribeRequest {
    public string email_address;
    public string status;
  }

  public GameObject emailField;
  private InputField inputField;

  public GameObject form;
  //public GameObject spinner;

  private static string mailchimpDataCenter = "us17";
  private static string mailchimpApiKey = "59c856d81b40663eb06029b7262e5858-us17";
  private static string mailchimpListId = "8c4b472a92";

  void Start() {
    inputField = emailField.GetComponent<InputField>();
  }

  void Update() {

  }

  public void Subscribe() {
    var email = inputField.text.Trim();
    if (email.Length == 0) {
      Debug.Log("no text");
      return;
    }

    StartCoroutine(SendToMailChimp(inputField.text));
    form.SetActive(false);
    //spinner.SetActive(true);
  }

  public IEnumerator SendToMailChimp(string email) {
    var url = GetUrl(string.Format("/lists/{0}/members/", mailchimpListId));	
    var www = CreateJsonWebRequest(url, new SubscribeRequest {
        email_address = email,
        status = "subscribed"
        });	
	yield return www.SendWebRequest();

    // Debug.Log(www.responseCode); 
    // Debug.Log(www.downloadHandler.text); 

    if (www.isNetworkError || www.isHttpError) {
      Debug.Log(www.error);
    }
    else {
      Debug.Log("Form upload complete!");
    }

    SceneManager.LoadScene("02a Hub");
  }

  private string GetUrl(string path) {
    return string.Format("https://{0}.api.mailchimp.com/3.0{1}", mailchimpDataCenter, path);
  }

  private static UnityWebRequest CreateJsonWebRequest(string url, object jsonObject) {
    var www = new UnityWebRequest(url);
    www.method = UnityWebRequest.kHttpVerbPOST;
    www.chunkedTransfer = false;
    www.SetRequestHeader("Authorization", GetAuthHeader());

    var postData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(jsonObject));
    var uh = new UploadHandlerRaw(postData);
    uh.contentType = "application/json";
    www.uploadHandler = uh;
    www.downloadHandler = new DownloadHandlerBuffer();

    return www;
  }

  private static string GetAuthHeader() {
    return GetBasicAuthHeader(string.Format("{0}:{1}", "anystring", mailchimpApiKey));
  }

  private static string GetBasicAuthHeader(string password) {
    return string.Format("Basic {0}", Base64Encode(password));
  }

  private static string Base64Encode(string plainText) {
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return System.Convert.ToBase64String(plainTextBytes);
  }
}
