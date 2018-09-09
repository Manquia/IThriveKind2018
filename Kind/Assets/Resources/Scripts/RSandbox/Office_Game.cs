using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office_Game : MonoBehaviour {
    public int game_end_string_length = 50;
    public Button end_game_button;

    public GameObject screen_transition_object;
    public AudioSource audio_player;
    [Space(5)]
    public Font office_Font;
    public string ui_instruction;
    private IEnumerator enumerator;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    private int stringTracker = 0;
    public InputField inputTextField;
    private bool inputText = false;
    public void OnValueChange(string value)
    {
        if(inputText)
        {
            inputText = false;
            return;
        }

        inputText = true;

        //Display the string.
        stringTracker++;
        string display = workString.Substring(0, Mathf.Min(stringTracker * 4, workString.Length - 1));
        inputTextField.text = display;
        inputTextField.caretPosition = stringTracker * 4;

        //Handle player sound
        if (enumerator != null)
        {
            StopAllCoroutines();
        }
        StartCoroutine(PlayerKeyDown());

        //update progress bar
        UI_Text tx = new UI_Text { text = "WORK HARDER", font = office_Font, progress = (stringTracker / 50f )};
        FFMessage<UI_Text>.SendToLocal(tx);

        //Check for button interactable condition
        if (stringTracker > game_end_string_length)
        {
            end_game_button.interactable = true;
        }
        else
        {
            end_game_button.interactable = false;
        }
    }

    public void OnButtonPressed()
    {
        Static_Var.Money += 50;
        FFMessage<TriggerFade>.SendToLocal(new TriggerFade());
    }

    public void PlaySound()
    {
        if (!audio_player.isPlaying)
        {
            audio_player.Play();
        }
    }

    public void StopSound()
    {
        audio_player.Pause();
    }

    IEnumerator PlayerKeyDown()
    {
        enumerator = PlayerKeyDown();
        PlaySound();
        yield return wait;
        StopSound();
        enumerator = null;
    }

    private void Start()
    {
        UI_Text tx;
        tx.text = ui_instruction;
        tx.font = office_Font;
        tx.progress = 0f;
        FFMessage<UI_Text>.SendToLocal(tx);

        Static_Var.RefreshUI();
    }

    public const string workString = "using UnityEngine; \n" +
    #region WORK_STRING
"using System.Collections;\n" +
"public class Perlin_depression : MonoBehaviour\n" +
"{\n" +
"    public int pixWidth;\n" +
"    public int pixHeight;\n" +
"\n" +
"    public float xOrg;\n" +
"    public float yOrg;\n" +
"\n" +
"    public float scale = 1.0F;\n" +
"\n" +
"    private Texture2D noiseTex;\n" +
"    private Color[] pix;\n" +
"    private Renderer rend;\n" +
"\n" +
"\n" +
"    void Start()\n" +
"    {\n" +
"        rend = GetComponent<Renderer>();\n" +
"\n" +
"        // Set up the texture and a Color array to hold pixels during processing.\n" +
"        noiseTex = new Texture2D(pixWidth, pixHeight);\n" +
"        pix = new Color[noiseTex.width * noiseTex.height];\n" +
"        rend.material.mainTexture = noiseTex;\n" +
"    }\n" +
"\n" +
"    void CalcNoise()\n" +
"    {\n" +
"        // For each pixel in the texture...\n" +
"        float y = 0.0F;\n" +
"\n" +
"        while (y < noiseTex.height)\n" +
"        {\n" +
"            float x = 0.0F;\n" +
"            while (x < noiseTex.width)\n" +
"            {\n" +
"                float xCoord = xOrg + x / noiseTex.width * scale;\n" +
"                float yCoord = yOrg + y / noiseTex.height * scale;\n" +
"                float sample = Mathf.PerlinNoise(xCoord, yCoord);\n" +
"                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);\n" +
"                x++;\n" +
"            }\n" +
"            y++;\n" +
"        }\n" +
"\n" +
"        // Copy the pixel data to the texture and load it into the GPU.\n" +
"        noiseTex.SetPixels(pix);\n" +
"        noiseTex.Apply();\n" +
"    }\n" +
"\n" +
"    void Update()\n" +
"    {\n" +
"        CalcNoise();\n" +
"    }\n" +
"}\n" +
"\n" +
"\n" +
"public class Office_Game : MonoBehaviour {\n" +
"    public int game_end_string_length = 50;\n" +
"    public Button end_game_button;\n" +
"\n" +
"    public GameObject screen_transition_object;\n" +
"    public AudioSource audio_player;\n" +
"    [Space(5)]\n" +
"    public Font office_Font;\n" +
"    public string ui_instruction;\n" +
"    private IEnumerator enumerator;\n" +
"\n" +
"    private WaitForSeconds wait = new WaitForSeconds(0.5f);\n" +
"\n" +
"\n" +
"    public void OnValueChange(string value)\n" +
"    {\n" +
"        \n" +
"\n" +
"        //Handle player sound\n" +
"        if(enumerator != null)\n" +
"        {\n" +
"            StopAllCoroutines();\n" +
"        }\n" +
"        StartCoroutine(PlayerKeyDown());\n" +
"\n" +
"        //update progress bar\n" +
"        UI_Text tx = new UI_Text { text = 'WORK HARDER', font = office_Font, progress = (value.Length / 50f )};\n"+
"        FFMessage<UI_Text>.SendToLocal(tx);\n"+
"\n"+
"        //Check for button interactable condition\n"+
"        if (value.Length > game_end_string_length)\n"+
"        {\n"+
"            end_game_button.interactable = true;\n"+
"        }\n"+
"        else\n"+
"        {\n"+
"            end_game_button.interactable = false;\n"+
"        }\n"+
"    }\n"+
"\n"+
"    public void OnButtonPressed()\n"+
"    {\n"+
"        Static_Var.Money += 50;\n"+
"        FFMessage<TriggerFade>.SendToLocal(new TriggerFade());\n"+
"    }\n"+
"\n"+
"    public void PlaySound()\n"+
"    {\n"+
"        if (!audio_player.isPlaying)\n"+
"        {\n"+
"            audio_player.Play();\n"+
"        }\n"+
"    }\n"+
"\n"+
"    public void StopSound()\n"+
"    {\n"+
"        audio_player.Pause();\n"+
"    }\n"+
"\n"+
"    IEnumerator PlayerKeyDown()\n"+
"    {\n"+
"        enumerator = PlayerKeyDown();\n"+
"        PlaySound();\n"+
"        yield return wait;\n"+
"        StopSound();\n"+
"        enumerator = null;\n"+
"    }\n"+
"\n"+
"    private void Start()\n"+
"    {\n"+
"        UI_Text tx;\n"+
"        tx.text = ui_instruction;\n"+
"        tx.font = office_Font;\n"+
"        tx.progress = 0f;\n"+
"        FFMessage<UI_Text>.SendToLocal(tx);\n"+
"\n"+
"        Static_Var.RefreshUI();\n"+
"    }\n"+
"\n"+
"    string workString =  ;\n";

#endregion WORK_STRIN
}
