using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //0= not met yet. 1, first level done. 2, waiting for second. 3, second level done. 4, waiting for third, 5 third done, 6 all done.
    int ClothosLevel = 0;
    int LachesisLevel = 0;
    int AtroposLevel = 0;

    //the first dialogues of the gals (for each state)

    [SerializeField] List<dialogueScriptableObjects> Clothos = new List<dialogueScriptableObjects>();
    [SerializeField] List<dialogueScriptableObjects> Lachesis = new List<dialogueScriptableObjects>();
    [SerializeField] List<dialogueScriptableObjects> Atropos = new List<dialogueScriptableObjects>();


    public static GameManager Instance;
    [SerializeField] Image sceneTransicioner;
    [SerializeField] MapManager mapManager;
    [SerializeField] dialogueScriptableObjects LachesisNotReady;
    [SerializeField] dialogueScriptableObjects AtroposNotReady;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        DontDestroyOnLoad(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeScene(string scene, bool makeTrans = true)
    {
        if (makeTrans)
        {
            sceneTransicioner.gameObject.SetActive(true);
            LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) =>
            {
                Color newColor = sceneTransicioner.color;
                newColor.a = val;
                sceneTransicioner.color = newColor;
            }).setOnComplete(()=> { changeSceneAfterTween(scene); });
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void requestMinigame(string game)
    {
        //dos opciones, o lachesis o atropos
        if(SceneManager.GetActiveScene().name == "Lachesis")
        {
            if (ClothosLevel > LachesisLevel)
                changeScene(game);
            else
                DialogueManager.instance.startDialogue(LachesisNotReady);

        }
        else if(SceneManager.GetActiveScene().name == "Atropos")
        {
            if (LachesisLevel > AtroposLevel)
                changeScene(game);
            else
                DialogueManager.instance.startDialogue(AtroposNotReady);
        }
    }
    void changeSceneAfterTween(string scene)
    {
        SceneManager.LoadScene(scene);
        LeanTween.value(gameObject, 1f, 0f, 0.5f).setOnUpdate((float val) =>
        {
            Color newColor = sceneTransicioner.color;
            newColor.a = val;
            sceneTransicioner.color = newColor;
        }).setOnComplete(turnOfftransitioner );
    }

    void turnOfftransitioner()
    {
        sceneTransicioner.gameObject.SetActive(false);
    }

    public void addDialogueAsDone(string moira)
    {
        if (moira == "Clothos")
        {
            ClothosLevel++;
        }
        else if (moira == "Lachesis")
        {
            LachesisLevel++;
        }
        else if (moira == "Atropos")
        {
            AtroposLevel++;
        }

    }

    //for the map (idk if there will be one
    dialogueScriptableObjects getRoomDialogue(string moira)
    {
        if (moira == "Clothos")
        {
            return Clothos[ClothosLevel];
        }
        else if (moira == "Lachesis")
        {
            return Lachesis[LachesisLevel];
        }
        else if (moira == "Atropos")
        {
            return Atropos[AtroposLevel];
        }

        return null;
    }

    public dialogueScriptableObjects requestDialogue(string room)
    {
        //if its one of the moiras room, it gets a line
        //if not, it doesnt
        return getRoomDialogue(room);
    }

    public void openMap()
    {
        mapManager.openMap();
    }

    public void reset()
    {
        ClothosLevel = 0;
        LachesisLevel = 0;
        AtroposLevel = 0;
    }
}
