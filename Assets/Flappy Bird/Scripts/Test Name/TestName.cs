using UnityEngine;
using UnityEngine.UI;

public class TestName : MonoBehaviour
{
    [SerializeField] private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        string name1 = PlayerPrefs.GetString("p1");
        string name2 = PlayerPrefs.GetString("p2");

        myText.text = name1 + " Vs " + name2;
    }
}
