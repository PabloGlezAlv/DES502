using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;

public class DosIntro : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI TextUI;
    private const int MAX_COLUMNS = 27;//Assuming text is at a size of 36

    int TextIndex = 0;
    private float CurrentTimer;
    private float CurrentTypeTimer;
    bool DoneWriting = true;
    private struct TextLines
    {
        public string Text;
        public float TypeTime;
        public float WaitTime;
    }

    static TextLines[] Lines =
    {
        //Idea is type the text within TypeTime seconds and then wait WaitTime seconds
        new TextLines() { Text = "    C:\\>",                                               TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "keyb uk\n",                                               TypeTime = 0.5f,  WaitTime = 0.5f},
        new TextLines() { Text = "    Keyboard layout uk loaded for codepage 858\n\n",      TypeTime = 0,     WaitTime = 0.21f},
                                      
        new TextLines() { Text = "    C:\\>",                                               TypeTime = 0,     WaitTime = 1},
        new TextLines() { Text = "SET BLASTER=A220 I7 D1 H5 T6\n\n",                        TypeTime = 2,     WaitTime = 0.24f},
                                      
        new TextLines() { Text = "    C:\\>",                                               TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "dir\n",                                                   TypeTime = 1,     WaitTime = 0.25f},
        new TextLines() { Text = "    Directory of C:\\>\n",                                TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    .              <DIR>           12-24-1993 18:53\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    ..             <DIR>           01-01-1980 00:00\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    TCZ            <DIR>           12-08-1981 02:11\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    DOOM           <DIR>           10-12-1993 02:11\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    SIMCITYC       <DIR>           14-06-1990 05:41\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    XENON          <DIR>           14-06-1990 22:45\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    FEELINGS TXT             5,162 14-06-1990 01:12\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    THERAPY7 TXT            16,800 14-06-1990 23:31\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "        2 File(s)           21,962 Bytes.\n"          ,   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "        6 Dir(s)           262,111 Bytes free.\n",        TypeTime = 0,     WaitTime = 0},

        new TextLines() { Text = "    C:\\>",                                               TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "cd TCZ\n",                                                TypeTime = 1,     WaitTime = 0.25f},
                                      
        new TextLines() { Text = "    C:\\TCZ>",                                            TypeTime = 0,     WaitTime = 1.25f},
        new TextLines() { Text = "dir \n",                                                  TypeTime = 0.5f,     WaitTime = 0.25f},
        
        new TextLines() { Text = "    Directory of C:\\TCZ>\n",                             TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    .              <DIR>           12-24-1993 18:53\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    ..             <DIR>           01-01-1980 00:00\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    DAT            <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    GFX            <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    LVL            <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    REGISTR        <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    STTNGS         <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    VGA            <DIR>           12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    README TXT             264,000 12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    SAV1   SAV               2,400 12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    TCZ    COM               1,818 12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "    TCZ    EXE              12,672 12-08-1982 12:21\n",   TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "        4 File(s)           12,940 Bytes.\n",             TypeTime = 0,     WaitTime = 0},
        new TextLines() { Text = "        9 Dir(s)           262,111 Bytes free.\n",        TypeTime = 1,     WaitTime = 0},
        new TextLines() { Text = "    C:\\TCZ>",                                            TypeTime = 0,     WaitTime = 1},
        new TextLines() { Text = "TCZ.EXE",                                                 TypeTime = 1,     WaitTime = 2},
        new TextLines() { Text = "",                                                        TypeTime = 0,     WaitTime = 2},
    };

    private void Start()
    {
        TextUI.alignment = TextAlignmentOptions.TopLeft;
    }

    private void FixedUpdate()
    {
        if (TextIndex < Lines.Length && DoneWriting)
        {
            if (TextUI.textInfo.lineCount + 1 > MAX_COLUMNS)
            {
                TextUI.alignment = TextAlignmentOptions.BottomLeft;
            }
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > Lines[TextIndex].WaitTime)
            {
                StartCoroutine(WriteText());
                CurrentTimer = 0;
            }
        }
        else if (TextIndex >= Lines.Length || Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.active = false;
        }
    }

    IEnumerator WriteText()
    {
        //Writes the text in TypeTime seconds
        DoneWriting = false;
        Debug.Log(TextUI.textInfo.lineCount);
        if (Lines[TextIndex].TypeTime == 0)
        {
            TextUI.text += Lines[TextIndex].Text;
        }
        else
        {
            float TimeDivide = Lines[TextIndex].TypeTime / Lines[TextIndex].Text.Length;
            for (int x = 0; x < Lines[TextIndex].Text.Length; x++)
            {
                TextUI.text += Lines[TextIndex].Text[x];
                yield return new WaitForSeconds(TimeDivide);
            }
        }
        TextIndex++;
        DoneWriting = true;
    }
}
