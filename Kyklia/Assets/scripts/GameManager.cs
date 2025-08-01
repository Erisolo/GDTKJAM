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
    int LechesisLevel = 0;
    int AtroposLevel = 0;

    //the first dialogues of the gals (for each state)

    [SerializeField] List<dialogueScriptableObjects> Clothos = new List<dialogueScriptableObjects>();
    [SerializeField] List<dialogueScriptableObjects> Lachesis = new List<dialogueScriptableObjects>();
    [SerializeField] List<dialogueScriptableObjects> Atropos = new List<dialogueScriptableObjects>();


    public static GameManager Instance;
    [SerializeField] Image sceneTransicioner;

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

    void completeMinigame(string moira)
    {
        if (moira == "Clothos")
        {
            ClothosLevel++;
        }
        else if (moira == "Lachesis")
        {
            LechesisLevel++;
        }
        else if (moira == "Atropos")
        {
            AtroposLevel++;
        }

        //check if you've finished all of them to unlock the final area

        if(ClothosLevel == 6 && LechesisLevel == 6 && AtroposLevel == 6)
        {
            //TODO:: DESBLOQUEAR LO DEL MAPA
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
            return Lachesis[LechesisLevel];
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
}
