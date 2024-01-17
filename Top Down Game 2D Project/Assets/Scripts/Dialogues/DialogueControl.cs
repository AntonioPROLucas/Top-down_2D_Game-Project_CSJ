using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }
    public idiom language;


    [Header("Components")]
    public GameObject dialogueObj;               //janela de diálogo
    public Image profileSprite;                  //sprite do perfil
    public TextMeshProUGUI speechText;           //texto da fala
    public TextMeshProUGUI actorNameText;        //nome do npc

    [Header("Settings")]
    public float typingSpeed;  //velocidade da fala
    public Animator anim;

    //Variaveis de controle
    [HideInInspector]public bool isShowing; //se a janela está visivel 
    public int index;     //index das sentenças
    private string[] sentences;

    public static DialogueControl instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray() )
        {
            speechText.text += letter;
            yield return new  WaitForSeconds(typingSpeed);
        }
        OnSentenceComplete();
    }
    

    //pular pra próxima frase/fala
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                anim.SetInteger("transition", 1);
                StartCoroutine(TypeSentence());
            }
            else//quando terminam o texto
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
            }
        }

    }

    //chamar a fala do npc
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
     // Método chamado quando a sentença é concluída
    private void OnSentenceComplete()
    {
        // Verifica se o texto digitado é igual à sentença atual
        if (speechText.text == sentences[index])
        {
            // Atualiza o animator
            anim.SetInteger("transition", 0);
        }
    }
}
