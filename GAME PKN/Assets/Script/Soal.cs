using TMPro;
using UnityEngine;
using System.Collections.Generic;


public class Soal : MonoBehaviour
{
    private Dictionary<int, string> sceneQuestions = new Dictionary<int, string>()
    {
        {2, "Berikut ini merupakan contoh pelanggaran HAM, kecuali... \n A. Penyiksaan terhadap tahanan \n B. Penahanan tanpa proses hukum yang jelas \n C. Memberi kesempatan berbicara kepada semua warga"},
        {4, "Dalam sistem demokrasi konstitusional, \n pengakuan terhadap HAM diwujudkan melalui... \n A. Pembatasan kebebasan demi stabilitas negara \n B. Jaminan hukum yang tertuang dalam Undang-Undang Dasar \n C. Pelimpahan kekuasaan legislatif kepada eksekutif"},
        {6, "Dalam konteks hubungan antara negara dan warganya, pelaksanaan demokrasi sering kali berbenturan dengan kepentingan stabilitas nasional. Bagaimana seharusnya negara menyikapi hal ini? \n A. Mengutamakan stabilitas meski harus mengurangi kebebasan sipil \n B. Menghapus hak-hak individu demi kepentingan mayoritas \n C. Menyelaraskan kebebasan individu dengan prinsip hukum dan keamanan"}
    };

    void Start()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        if (sceneQuestions.ContainsKey(currentScene))
        {
            CreateWorldText(
                sceneQuestions[currentScene],
                transform.position,
                Color.black,
                5
            );
        }
    }

    public void CreateWorldText(string text, Vector3 position, Color color, int fontSize)
    {
        GameObject textObj = new GameObject("Soal", typeof(TextMeshPro));
        TextMeshPro tmp = textObj.GetComponent<TextMeshPro>();
        
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.color = color;
        tmp.alignment = TextAlignmentOptions.Center;
        
        tmp.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        tmp.GetComponent<MeshRenderer>().sortingOrder = 100;
        
        textObj.transform.position = position;
    }
}